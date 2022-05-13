using System.Reflection;
using Sora;
using Sora.Interfaces;
using Sora.Net.Config;
using Sora.OnebotAdapter;
using Tsukie.Integration.Models;
using Tsukie.Sample.Plugin;


//Console.WriteLine(MyPlugin.PluginName);
//var field = typeof(MyPlugin).GetProperty(nameof(Plugin.PluginName), BindingFlags.Public | BindingFlags.Static);
var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
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

app.Run();

