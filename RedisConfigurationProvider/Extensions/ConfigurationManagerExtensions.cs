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
            var config = configurationManager.GetSection(RedisConfigurationProviderOptions.Name).Get<RedisConfigurationProviderOptions>();
            ConfigurationOptions opts = new ConfigurationOptions()
            {
                EndPoints = { { config.Url, config.Port } },
                User = config.Username,
                Password = config.Password
            };
            IConfigurationBuilder builder = configurationManager;
            builder.Add(new RedisConfigurationSource(opts.ToString(),config.Key));
            return configurationManager;
        }

    }
}
