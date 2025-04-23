using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationSource(string connectionString, string key) : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new RedisConfigurationProvider(connectionString, key);
    }
}
