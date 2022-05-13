namespace Tsukie.Backend.Models.Plugin
{
    public class PluginInstanceInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string TypeId { get; set; } = string.Empty;
        public string ConfigurationFilePath { get; set; } = string.Empty;
    }
}
