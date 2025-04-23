using LibraryTestProject.Services.Contracts;
using LibraryTestProject.Services.Models;
using Microsoft.Extensions.Options;

namespace LibraryTestProject.Services.Implementations
{
    public class ConfigurationTestService : IConfigurationTestService
    {

        private string Data1 { get; set; }
        private string Data2 { get; set; }

        public ConfigurationTestService(IOptions<ConfigurationData> data)
        {
            Data1 = data.Value.Data1;
            Data2 = data.Value.Data2;
        }

        public string GetResult(string key) => key switch
        {
            "Data1" => Data1,
            "Data2" => Data2,
            _ => "Unknown"
        };
    }
}
