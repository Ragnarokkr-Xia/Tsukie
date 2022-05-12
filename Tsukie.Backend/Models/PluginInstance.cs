using System.Diagnostics;
using Sora.Interfaces;
using Tsukie.Integration.Models;

namespace Tsukie.Backend.Models
{
    public class PluginInstance
    {
        public PluginInstanceInfo Info { get; set; } = new PluginInstanceInfo();
        public Thread? HostThread { get; set; }

        public Plugin? HostPlugin { get; set; }

        public ISoraService? SoraService { get; set; }

        public void Stop()
        {
        }

        public void Start()
        {

        }

    }
}
