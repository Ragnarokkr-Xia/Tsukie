namespace Tsukie.Backend.Models.Requests
{
    public class PluginInstanceCreateRequest
    {
        public string CqServerAddress { get; set; } = "localhost";
        public ushort CqServerPort { get; set; } = 6700;
        public string TypeId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
