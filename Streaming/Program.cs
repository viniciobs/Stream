using Streaming.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddScoped<IStreamService, StreamService>();
builder.Services.AddScoped<IItemGenerator, VehicleGenerator>();

var app = builder.Build();

app.MapControllers();

app.Run();