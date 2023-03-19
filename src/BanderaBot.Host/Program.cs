using BanderaBot.Host.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services
    .AddLogging(x => x.AddConsole())
    .AddBotServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();