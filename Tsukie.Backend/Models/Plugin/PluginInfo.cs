namespace Tsukie.Backend.Models.Plugin
{
    public class PluginInfo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Path { get; set; }
        public Type? Type { get; set; }
    }
}
