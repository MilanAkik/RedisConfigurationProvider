using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RedisConfigurationProvider.Configuration;
using RedisConfigurationProvider.Providers;

namespace RedisConfigurationProvider.Extensions
{
    public static class ConfigurationManagerExtensions
    {

        private static readonly ILoggerFactory _dummyLoggerFactory = NullLoggerFactory.Instance;

        public static ConfigurationManager AddRedisConfiguration(this ConfigurationManager configurationManager, ILoggerFactory? loggerFactory = null)
        {
            loggerFactory ??= _dummyLoggerFactory;
            var options = configurationManager.GetSection(RedisConfigurationProviderOptions.Name).Get<RedisConfigurationProviderOptions>();
            IConfigurationBuilder builder = configurationManager;
            builder.Add(new RedisConfigurationSource(options, loggerFactory));
            return configurationManager;
        }

        public static ConfigurationManager AddRedisConfiguration(this ConfigurationManager configurationManager, Action<RedisConfigurationProviderOptions> setupOptions, ILoggerFactory? loggerFactory = null)
        {
            loggerFactory ??= _dummyLoggerFactory;
            IConfigurationBuilder builder = configurationManager;
            var options = configurationManager.GetSection(RedisConfigurationProviderOptions.Name).Get<RedisConfigurationProviderOptions>();
            setupOptions(options);
            builder.Add(new RedisConfigurationSource(options, loggerFactory));
            return configurationManager;
        }

    }
}
