using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using RedisConfigurationProvider.Configuration;
using Microsoft.Extensions.Logging;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationProvider : ConfigurationProvider
    {

        private string _keyLevelSeparator;
        private IDatabase _db;
        private string _key;
        private ILogger<RedisConfigurationProvider> _logger;

        public RedisConfigurationProvider(RedisConfigurationProviderOptions options, ILogger<RedisConfigurationProvider> logger)
        {
            try
            {
                ConfigurationOptions configOptions = new ConfigurationOptions()
                {
                    EndPoints = { { options.Url, options.Port } },
                    User = options.Username,
                    Password = options.Password
                };
                logger.LogDebug("Connecting to Redis at {Endpoint}", configOptions);
                var mux = ConnectionMultiplexer.Connect(configOptions.ToString());
                _db = mux.GetDatabase();
                logger.LogDebug("Connected to Redis at {Endpoint}", configOptions);
                _key = options.Key;
                _keyLevelSeparator = options.KeyLevelSeparator;
                _logger = logger;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while connecting to Redis");
                throw;
            }
        }

        public override void Load()
        {
            try
            {
                _logger.LogInformation("Started loading configuration from Redis for key {Key}", _key);
                var nestedKeys = GetNestedKeys(_key, _keyLevelSeparator);
                var foundKeys = new List<string>();
                foreach (var key in nestedKeys)
                {
                    _logger.LogDebug("Checking the key {Key}", key);
                    var keyFound = _db.KeyExists(key);
                    _logger.LogDebug("Key {Key} {Status}", key, keyFound ? "exists" : "does not exist");
                    if (keyFound)
                    {
                        foundKeys.Add(key);
                        var redisResult = _db.StringGet(key).ToString();
                        _logger.LogDebug("Value for key {Key} fetched. Length: {Length}", key, redisResult.Length);

                        if (string.IsNullOrWhiteSpace(redisResult))
                        {
                            _logger.LogWarning("Key {Key} exists but the value is empty or null. Skipping.", key);
                            continue;
                        }
                        Dictionary<string, string> dataset = GetKVPFromJson(redisResult);
                        int overloads = 0;
                        foreach (var item in dataset)
                        {
                            if (Data.ContainsKey(item.Key)) overloads++;
                            Data[item.Key] = item.Value;
                        }
                        _logger.LogDebug("Key {Key} processed. Total pairs: {KvpCount}. Overloads: {Overloads}", key, dataset.Count, overloads);
                    }
                }
                if (foundKeys.Count == 0)
                    _logger.LogWarning("No keys found in Redis for the provided key {Key} and its nested keys. Nested keys checked: {CheckedCount}", _key, nestedKeys.Count);
                else
                    _logger.LogInformation("Finished loading configuration from Redis for key {Key}. Nested keys checked: {CheckedCount}. Nested keys found: {FoundCount}", _key, nestedKeys.Count, foundKeys.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading configuration from Redis for key {Key}", _key);
                throw;
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

        private Dictionary<string, string> GetKVPFromJson(string json)
        {
            _logger.LogTrace("Starting JSON parsing. Raw JSON length: {Length}", json.Length);
            Dictionary<string, string> result = [];
            Queue<(string,JsonElement)> elements = new Queue<(string, JsonElement)>();
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            elements.Enqueue(("",root));
            while (elements.Any())
            {
                var (key, elem) = elements.Dequeue();
                string prefix = (key.Length == 0) ? key : $"{key}:";
                _logger.LogTrace("Processing JSON element node. Key: {Key}, Kind: {ValueKind}", key, elem.ValueKind);
                switch (elem.ValueKind)
                {
                    case JsonValueKind.String:
                        {
                            var val = elem.GetString();
                            _logger.LogTrace("Extracted String KVP: {PropertyKey} = {PropertyValue}", key, val);
                            result.Add($"{key}", val);
                            break;
                        }
                    case JsonValueKind.Number:
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        {
                            var val = elem.GetRawText();
                            var kind = (elem.ValueKind == JsonValueKind.Number) ? "Number" : "Boolean";
                            _logger.LogTrace("Extracted {Kind} KVP: {PropertyKey} = {PropertyValue}", kind, key, val);
                            result.Add($"{key}", val);
                            break;
                        }
                    case JsonValueKind.Object:
                        {
                            var properties = elem.EnumerateObject().ToList();
                            _logger.LogTrace("Element {Key} is an Object with {Count} properties. Enqueueing children.", key, properties.Count);
                            foreach (var property in properties)
                            {
                                _logger.LogTrace("Enqueueing property {PropertyName} for key {Key}", property.Name, key);
                                elements.Enqueue(($"{prefix}{property.Name}", property.Value));
                            }
                            break;
                        }
                    case JsonValueKind.Array:
                        {
                            var arrayElements = elem.EnumerateArray().ToList();
                            _logger.LogTrace("Element {Key} is an Array with {Count} elements. Enqueueing elements.", key, arrayElements.Count);
                            for (int i= 0; i<arrayElements.Count; i++)
                            {
                                _logger.LogTrace("Enqueueing array element at index {Index} for key {Key}", i, key);
                                elements.Enqueue(($"{prefix}{i}", arrayElements[i]));
                            }
                            break;
                        }
                    default:
                        {
                            throw new JsonException($"Null or undefined value found at '{prefix}{key}'. This cannot be processed.");
                        }
                }
            }
            _logger.LogTrace("Finished JSON parsing. Extracted {Count} key-value pairs.", result.Count);
            return result;
        }

    }
}
