using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RedisConfigurationProvider.Providers
{
    public sealed class RedisConfigurationProvider : ConfigurationProvider
    {

        private IDatabase _db;
        private string _key;

        public RedisConfigurationProvider(string? connectionString, string key)
        {
            var mux = ConnectionMultiplexer.Connect(connectionString);
            _db = mux.GetDatabase();
            _key = key;
        }

        public override void Load()
        {
            if (!_db.KeyExists(_key)) return;
            var redisResult = _db.StringGet(_key).ToString();
            var dataArray = redisResult.Split('|').Select(x=>x.Split("="));
            Dictionary<string, string> dataset = dataArray.ToDictionary(x => x[0], x=>x[1]);
            foreach (var item in dataset) Data.Add(item);
        }
    }
}
