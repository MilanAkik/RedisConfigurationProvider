using Microsoft.Extensions.Configuration;
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
            var redisUrl = configurationManager.GetSection("RedisConfigurationProvider:Url").Value ?? "localhost";
            var redisPort = configurationManager.GetSection("RedisConfigurationProvider:Port").Value ?? "6379";
            var redisUsername = configurationManager.GetSection("RedisConfigurationProvider:Username").Value ?? "default";
            var redisPassword = configurationManager.GetSection("RedisConfigurationProvider:Password").Value ?? "";
            var key = configurationManager.GetSection("RedisConfigurationProvider:Key").Value;
            ConfigurationOptions opts = new ConfigurationOptions()
            {
                EndPoints = { { redisUrl, int.Parse(redisPort) } },
                User = redisUsername,
                Password = redisPassword
            };
            IConfigurationBuilder builder = configurationManager;
            builder.Add(new RedisConfigurationSource(opts.ToString(),key));
            return configurationManager;
        }

    }
}
