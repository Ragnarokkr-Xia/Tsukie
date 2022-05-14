using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Sora;
using Sora.Interfaces;
using Sora.Net.Config;
using Tsukie.Backend.Global;
using Tsukie.Integration.Interfaces;
using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Backend.Models.Plugin
{
    public class PluginInstance:PluginInstanceInfo,IDisposable
    {
        private bool _disposed;
        [JsonIgnore]
        public object HostPlugin { get; set; }
        [JsonIgnore]
        public ISoraService SoraService { get; set; }
        

        public static PluginInstance Create(PluginInstanceInfo info,ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger(info.Type);
            PluginInstance result = new PluginInstance()
            {
                Id = info.Id,
                TypeId = info.TypeId,
                Type = info.Type,
                Name = info.Name,
                CqServerAddress = info.CqServerAddress,
                CqServerPort = info.CqServerPort,
                ConfigurationFilePath = Path.GetFullPath($"{Constants.CONFIG_FOLDER_NAME}{Path.DirectorySeparatorChar}{info.Id}.json")
            };
            ClientConfig serviceConfig = new ClientConfig()
            {
                Host = info.CqServerAddress,
                Port = info.CqServerPort
            };
            ISoraService service = SoraServiceFactory.CreateService(serviceConfig);
            if (!File.Exists(result.ConfigurationFilePath))
            {
                using (Stream fs = File.Create(result.ConfigurationFilePath))
                {
                    fs.Write(Encoding.UTF8.GetBytes("{}"));
                }
            }
            PluginConfiguration pluginConfiguration = new PluginConfiguration(result.ConfigurationFilePath);
            result.SoraService = service;
            result.HostPlugin = Activator.CreateInstance(info.Type, service, pluginConfiguration, logger);
            return result;
        }
        public async Task StopAsync()
        {
            if (Status == PluginInstanceStatus.Running && SoraService != null)
            {
                (HostPlugin as IStartStop)?.Stop();
                await SoraService.StopService();
                
                Status = PluginInstanceStatus.Stopped;
            }
            
        }

        public async Task StartAsync()
        {
            if (Status == PluginInstanceStatus.Stopped && SoraService != null)
            {
                (HostPlugin as IStartStop)?.Start();
                await SoraService.StartService();
                Status = PluginInstanceStatus.Running;
            }
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    SoraService?.Dispose();
                    (HostPlugin as IDisposable)?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
