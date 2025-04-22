using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationProvider(string? key) : ConfigurationProvider
    {
        public override void Load()
        {
            base.Load();
        }
    }
}
