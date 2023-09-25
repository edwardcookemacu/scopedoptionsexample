using Microsoft.Extensions.Options;
using WebApplication1.OptionsStuff;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOptions();
builder.Services.AddScoped<GuidGenerator>();
builder.Services.Configure<StandardOptions>((options) => options.Guid = new GuidGenerator().Guid);
builder.Services.AddScoped<IConfigureOptions<SnapshotOptions>, SnapshotOptionsConfigurator>();
builder.Services.AddScoped<ScopedOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
