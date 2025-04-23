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
            var dataArray = _db.StringGet(_key).ToString().Split('|').Select(x=>x.Split("="));
            Dictionary<string, string> dataset = dataArray.ToDictionary(x=>x.First(), x=>x.Skip(1).First());
            foreach (var item in dataset) Data.Add(item);
        }
    }
}
