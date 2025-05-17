using Microsoft.Extensions.Configuration;
using RedisConfigurationProvider.Configuration;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationSource(RedisConfigurationProviderOptions options) : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new RedisConfigurationProvider(options);
    }
}
