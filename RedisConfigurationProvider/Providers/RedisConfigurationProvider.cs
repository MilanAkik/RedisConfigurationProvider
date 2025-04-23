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

        public static Dictionary<string, Dictionary<string, string>> DataSets = new Dictionary<string, Dictionary<string, string>> {
            { "TestKey" , new Dictionary<string, string>{ { "ConfigurationData:Data1", "Value1" },  { "ConfigurationData:Data2", "Value2" } } },
            { "ProdKey" , new Dictionary<string, string>{ { "ConfigurationData:Data1", "ProdValue1" },  { "ConfigurationData:Data2", "ProdValue2" } } }
        };

        public override void Load()
        {
            Dictionary<string, string> dataset = DataSets[key];
            foreach (var item in dataset) Data.Add(item);
        }
    }
}
