using LibraryTestProject.Services.Contracts;
using LibraryTestProject.Services.Models;
using Microsoft.Extensions.Options;

namespace LibraryTestProject.Services.Implementations
{
    public class ConfigurationTestService : IConfigurationTestService
    {

        private string Data1 { get; set; }
        private string Data2 { get; set; }
        private int X { get; set; }
        private int Y { get; set; }
        private int Z { get; set; }

        public ConfigurationTestService(IOptions<ConfigurationData> data, IOptions<OverrideTestData> odata)
        {
            Data1 = data.Value.Data1;
            Data2 = data.Value.Data2;
            X = odata.Value.X;
            Y = odata.Value.Y;
            Z = odata.Value.Z;
        }

        public string GetResult(string key) => key switch
        {
            "Data1" => Data1,
            "Data2" => Data2,
            "X" => $"{X}",
            "Y" => $"{Y}",
            "Z" => $"{Z}",
            _ => "Unknown"
        };
    }
}
