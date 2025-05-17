using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using RedisConfigurationProvider.Configuration;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationProvider : ConfigurationProvider
    {

        private string _keyLevelSeparator;
        private IDatabase _db;
        private string _key;

        public RedisConfigurationProvider(RedisConfigurationProviderOptions options)
        {
            ConfigurationOptions configOptions = new ConfigurationOptions()
            {
                EndPoints = { { options.Url, options.Port } },
                User = options.Username,
                Password = options.Password
            };
            var mux = ConnectionMultiplexer.Connect(configOptions.ToString());
            _db = mux.GetDatabase();
            _key = options.Key;
            _keyLevelSeparator = options.KeyLevelSeparator;
        }

        public override void Load()
        {
            foreach(var key in GetNestedKeys(_key, _keyLevelSeparator))
            {
                if (!_db.KeyExists(key)) continue;
                var redisResult = _db.StringGet(key).ToString();
                Dictionary<string, string> dataset = GetKVPFromJson(redisResult);
                foreach (var item in dataset)
                {
                    Data[item.Key]=item.Value;
                }
            }
        }

        private static List<string> GetNestedKeys(string key, string keyLevelSeparator)
        {
            var result = new List<string>();
            var segments = key.Split(keyLevelSeparator);
            for (var i = 1; i <= segments.Length; i++)
            {
                result.Add(string.Join(keyLevelSeparator, segments[0..i]));
            }
            return result;
        }

        private static Dictionary<string, string> GetKVPFromJson(string json)
        {
            Dictionary<string, string> result = [];
            Queue<(string,JsonElement)> elements = new Queue<(string, JsonElement)>();
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            elements.Enqueue(("",root));
            while (elements.Any())
            {
                var (key, elem) = elements.Dequeue();
                string prefix = (key.Length == 0) ? key : $"{key}:";
                switch (elem.ValueKind)
                {
                    case JsonValueKind.String:
                        {
                            result.Add($"{key}", elem.GetString());
                            break;
                        }
                    case JsonValueKind.Number:
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        {
                            result.Add($"{key}", elem.GetRawText());
                            break;
                        }
                    case JsonValueKind.Object:
                        {
                            var properties = elem.EnumerateObject().ToList();
                            foreach (var property in properties)
                            {
                                elements.Enqueue(($"{prefix}{property.Name}", property.Value));
                            }
                            break;
                        }
                    case JsonValueKind.Array:
                        {
                            var arrayElements = elem.EnumerateArray().ToList();
                            for (int i= 0; i<arrayElements.Count; i++)
                            {
                                elements.Enqueue(($"{prefix}{i}", arrayElements[i]));
                            }
                            break;
                        }
                    default:
                        {
                            throw new JsonException("Null or undefined value found. This cannot be processed");
                        }

                }
            }
            return result;
        }

    }
}
