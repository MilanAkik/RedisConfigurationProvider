using RedisConfigurationProvider;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/{n}", (int n) => $"Hello World!\n[{string.Join(',',Class1.GetSquares(n))}]");

app.Run();
