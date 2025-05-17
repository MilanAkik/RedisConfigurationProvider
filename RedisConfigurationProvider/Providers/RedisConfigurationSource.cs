using Microsoft.Extensions.Configuration;
using RedisConfigurationProvider.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationSource(RedisConfigurationProviderOptions options) : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new RedisConfigurationProvider(options);
    }
}
