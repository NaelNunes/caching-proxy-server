using Microsoft.Extensions.Options;
using serverproxy.Configurations;
using serverproxy.Services;

var builder = WebApplication.CreateBuilder(args);

var port = builder.Configuration["port"] ?? "5000";
var origin = builder.Configuration["origin"];

if (string.IsNullOrEmpty(origin))
{
    throw new Exception("Informe --origin=http://url");
}

// Configura a porta do servidor
builder.WebHost.UseUrls($"http://localhost:{port}");

builder.Services.Configure<ProxySettings>(options =>
{
    options.Origin = origin;
});

// Serviços da aplicação
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Exemplo: registre seu service depois
builder.Services.AddSingleton<ProxyService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"Proxy rodando em http://localhost:{port}");
Console.WriteLine($"Origin: {origin}");

app.Run();