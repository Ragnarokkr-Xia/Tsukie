using Tsukie.Backend.Models.Exceptions;

namespace Tsukie.Backend.Models.Plugin
{
    public class PluginInstanceManager
    {
        private ILogger<PluginInstanceManager>? Logger { get; set; }
        public PluginInstanceManager(ILogger<PluginInstanceManager>? logger)
        {
            Logger = logger;
        }
        public List<PluginInstance> PluginInstanceList { get; set; } = new List<PluginInstance>();

        public void Create(PluginInstanceInfo instanceInfo)
        {
            PluginInstance instance = new PluginInstance()
            {
                TypeId = instanceInfo.TypeId,
                Name = instanceInfo.Name,
            };
            PluginInstanceList.Add(instance);
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
            PluginInstance? instance = PluginInstanceList.FirstOrDefault(t => t.Id.Equals(instanceId));
            if (instance == null)
            {
                throw new PluginInstanceNotFoundException();
            }

            return instance;
        }
    }
}
