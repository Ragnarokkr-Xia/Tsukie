using System.Text.Json.Serialization;
using Sora.Net.Config;

namespace Tsukie.Backend.Models.Plugin
{
    public class PluginInstanceInfo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public PluginInstanceStatus Status { get; internal set; } = PluginInstanceStatus.Stopped;
        [JsonIgnore]
        public Type Type { get; set; }
        public string TypeId { get; set; } = string.Empty;
        public string ConfigurationFilePath { get; set; } = string.Empty;

        public string CqServerAddress { get; set; } = "localhost";
        public ushort CqServerPort { get; set; } = 6700;
    }
}
