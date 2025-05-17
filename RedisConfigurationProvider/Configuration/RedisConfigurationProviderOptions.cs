namespace RedisConfigurationProvider.Configuration
{
    public class RedisConfigurationProviderOptions
    {

        public const string Name = "RedisConfigurationProvider";

        public string Url { get; set; } = "localhost";
        public int Port { get; set; } = 6379;
        public string Username { get; set; } = "default";
        public string Password { get; set; } = "";
        public string Key { get; set; } = "";
        public string KeyLevelSeparator { get; set; } = "_";

    }
}
