using LibraryTestProject.Services.Contracts;
using LibraryTestProject.Services.Implementations;
using LibraryTestProject.Services.Models;
using RedisConfigurationProvider.Extensions;

var builder = WebApplication.CreateBuilder(args);
using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.SetMinimumLevel(LogLevel.Warning);
    loggingBuilder.AddFilter("RedisConfigurationProvider", LogLevel.Trace);
    loggingBuilder.AddConsole();
});
builder.Configuration.AddRedisConfiguration(loggerFactory);
builder.Services.Configure<ConfigurationData>(builder.Configuration.GetSection(nameof(ConfigurationData)));
builder.Services.Configure<OverrideTestData>(builder.Configuration.GetSection(nameof(OverrideTestData)));
builder.Services.AddSingleton<IConfigurationTestService, ConfigurationTestService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/config", (string key, IConfigurationTestService service) => service.GetResult(key));

app.Run();
