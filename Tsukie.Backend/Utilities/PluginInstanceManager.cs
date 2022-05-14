using Tsukie.Backend.Models.Exceptions;
using Tsukie.Backend.Models.Plugin;

namespace Tsukie.Backend.Utilities
{
    public class PluginInstanceManager
    {
        private ILogger<PluginInstanceManager> Logger { get; set; }
        private ILoggerFactory LoggerFactory { get; set; }
        public PluginInstanceManager(ILogger<PluginInstanceManager> logger,ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            Logger = logger;
        }
        public List<PluginInstance> PluginInstanceList { get; set; } = new List<PluginInstance>();

        public PluginInstance Create(PluginInstanceInfo instanceInfo)
        {
            PluginInstance instance = PluginInstance.Create(instanceInfo,LoggerFactory);
            PluginInstanceList.Add(instance);
            return instance;
        }

        public async Task StartAsync(string instanceId)
        {
            PluginInstance instance = Find(instanceId);
            await instance.StartAsync();
        }

        public async Task StopAsync(string instanceId)
        {
            PluginInstance instance = Find(instanceId);
            await instance.StopAsync();
        }

        public async Task DeleteAsync(string instanceId)
        {
            PluginInstance instance = Find(instanceId);
            PluginInstanceList.Remove(instance);
            await instance.StopAsync();
            instance.Dispose();
        }

        public PluginInstance Find(string instanceId)
        {
            PluginInstance instance = PluginInstanceList.FirstOrDefault(t => t.Id.Equals(instanceId));
            if (instance == null)
            {
                throw new PluginInstanceNotFoundException();
            }

            return instance;
        }
    }
}
