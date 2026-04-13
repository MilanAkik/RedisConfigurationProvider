using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RedisConfigurationProvider.Configuration;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationSource(RedisConfigurationProviderOptions options, ILoggerFactory loggerFactory) : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new RedisConfigurationProvider(options, loggerFactory.CreateLogger<RedisConfigurationProvider>());
    }
}