using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RedisConfigurationProvider.Configuration;
using RedisConfigurationProvider.Providers;

namespace RedisConfigurationProvider.Extensions
{
    public static class ConfigurationManagerExtensions
    {

        public static ConfigurationManager AddRedisConfiguration(this ConfigurationManager configurationManager, ILoggerFactory loggerFactory)
        {
            var options = configurationManager.GetSection(RedisConfigurationProviderOptions.Name).Get<RedisConfigurationProviderOptions>();
            IConfigurationBuilder builder = configurationManager;
            builder.Add(new RedisConfigurationSource(options, loggerFactory));
            return configurationManager;
        }
        public static ConfigurationManager AddRedisConfiguration(this ConfigurationManager configurationManager, Action<RedisConfigurationProviderOptions> setupOptions, ILoggerFactory loggerFactory)
        {
            IConfigurationBuilder builder = configurationManager;
            var options = configurationManager.GetSection(RedisConfigurationProviderOptions.Name).Get<RedisConfigurationProviderOptions>();
            setupOptions(options);
            builder.Add(new RedisConfigurationSource(options, loggerFactory));
            return configurationManager;
        }

    }
}
