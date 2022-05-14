using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sora;
using Sora.EventArgs.SoraEvent;
using Sora.Interfaces;
using Sora.Net.Config;
using Sora.OnebotAdapter;
using Tsukie.Backend.Global;
using Tsukie.Backend.Models.Plugin;
using Tsukie.Backend.Utilities;
using Tsukie.Integration.Models;
using Tsukie.Integration.Models.Configuration;
using Tsukie.Sample.Plugin;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net();

// Add services to the container.
builder.Services.AddSingleton(t =>
{
    ILogger<PluginUtility> logger = t.GetService<ILogger<PluginUtility>>();
    PluginUtility singleton = new PluginUtility(logger)
    {
        BaseFolder = Constants.PLUGIN_FOLDER_NAME
    };
    return singleton;
});

builder.Services.AddSingleton(t =>
{
    ILogger<PluginInstanceManager> logger = t.GetService<ILogger<PluginInstanceManager>>();
    ILoggerFactory loggerFactory = t.GetService<ILoggerFactory>();
    PluginInstanceManager singleton = new PluginInstanceManager(logger, loggerFactory);
    return singleton;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

PluginUtility pluginUtility = (PluginUtility)app.Services.GetService(typeof(PluginUtility));
PluginInstanceManager pluginInstanceManager = (PluginInstanceManager)app.Services.GetService(typeof(PluginInstanceManager));
await Initializer.RestorePluginInstancesAsync(pluginUtility, pluginInstanceManager);

app.Run();

