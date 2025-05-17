using Microsoft.Extensions.Configuration;
using RedisConfigurationProvider.Configuration;
using RedisConfigurationProvider.Providers;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisConfigurationProvider.Extensions
{
    public static class ConfigurationManagerExtensions
    {

        public static ConfigurationManager AddRedisConfiguration(this ConfigurationManager configurationManager)
        {
            var options = configurationManager.GetSection(RedisConfigurationProviderOptions.Name).Get<RedisConfigurationProviderOptions>();
            IConfigurationBuilder builder = configurationManager;
            builder.Add(new RedisConfigurationSource(options));
            return configurationManager;
        }
        public static ConfigurationManager AddRedisConfiguration(this ConfigurationManager configurationManager, Action<RedisConfigurationProviderOptions> setupOptions)
        {
            IConfigurationBuilder builder = configurationManager;
            var options = configurationManager.GetSection(RedisConfigurationProviderOptions.Name).Get<RedisConfigurationProviderOptions>();
            setupOptions(options);
            builder.Add(new RedisConfigurationSource(options));
            return configurationManager;
        }

    }
}
