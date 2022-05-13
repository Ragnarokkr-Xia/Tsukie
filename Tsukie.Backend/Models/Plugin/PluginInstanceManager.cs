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
    }
}
