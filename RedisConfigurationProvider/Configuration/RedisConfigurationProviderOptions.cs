using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisConfigurationProvider.Configuration
{
    public class RedisConfigurationProviderOptions
    {

        public const string Name = "RedisConfigurationProvider";

        public string Url { get; set; } = "localhost";
        public int Port { get; set; } = 9379;
        public string Username { get; set; } = "default";
        public string Password { get; set; } = "";
        public string Key { get; set; } = "";

    }
}
