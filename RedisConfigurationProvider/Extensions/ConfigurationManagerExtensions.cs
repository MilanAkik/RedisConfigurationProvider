using Microsoft.Extensions.Configuration;
using RedisConfigurationProvider.Providers;
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
            var key = configurationManager.GetSection("RedisConfigurationProvider:Key").Value;
            Console.WriteLine(key);
            IConfigurationBuilder builder = configurationManager;
            builder.Add(new RedisConfigurationSource(key));
            return configurationManager;
        }

    }
}
