using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using RedisConfigurationProvider.Configuration;
using Microsoft.Extensions.Logging;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationProvider : ConfigurationProvider, IDisposable
    {

        private readonly ILogger<RedisConfigurationProvider> _logger;
        private readonly RedisConfigurationProviderOptions _options;
        private ConnectionMultiplexer? _multiplexer;

        public RedisConfigurationProvider(RedisConfigurationProviderOptions options, ILogger<RedisConfigurationProvider> logger)
        {
            _options = options;
            _logger = logger;
        }

        public override void Load()
        {
            try
            {
                if (_multiplexer == null)
                {
                    ConfigurationOptions configOptions = new ConfigurationOptions()
                    {
                        EndPoints = { { _options.Url, _options.Port } },
                        User = _options.Username,
                        Password = _options.Password
                    };
                    
                    _logger.LogDebug("Connecting to Redis at {Endpoint}", configOptions);
                    _multiplexer = ConnectionMultiplexer.Connect(configOptions.ToString());
                    _logger.LogDebug("Connected to Redis at {Endpoint}", configOptions);
                }
                
                IDatabase db = _multiplexer.GetDatabase();
                _logger.LogInformation("Started loading configuration from Redis for key {Key}", _options.Key);
                var nestedKeys = GetNestedKeys(_options.Key, _options.KeyLevelSeparator);
                var foundKeys = new List<string>();
                foreach (var key in nestedKeys)
                {
                    _logger.LogDebug("Checking the key {Key}", key);
                    var keyFound = db.KeyExists(key);
                    _logger.LogDebug("Key {Key} {Status}", key, keyFound ? "exists" : "does not exist");
                    if (keyFound)
                    {
                        foundKeys.Add(key);
                        var redisResult = db.StringGet(key).ToString();
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
                    _logger.LogWarning("No keys found in Redis for the provided key {Key} and its nested keys. Nested keys checked: {CheckedCount}", _options.Key, nestedKeys.Count);
                else
                    _logger.LogInformation("Finished loading configuration from Redis for key {Key}. Nested keys checked: {CheckedCount}. Nested keys found: {FoundCount}", _options.Key, nestedKeys.Count, foundKeys.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading configuration from Redis for key {Key}", _options.Key);
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

        public void Dispose()
        {
            if (_multiplexer != null)
            {
                _logger.LogTrace("Disposing Redis ConnectionMultiplexer.");
                _multiplexer.Dispose();
                _multiplexer = null;
            }
        }
    }
}
