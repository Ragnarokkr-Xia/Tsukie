using Microsoft.Extensions.Logging;
using Sora.Interfaces;
using Sora.OnebotAdapter;
using Tsukie.Integration.Interfaces;
using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Integration.Models
{
    public abstract class Plugin:PluginInformation
    {
        protected Plugin(ISoraService service,PluginConfiguration configuration,ILogger logger)
        {
            SoraService = service;
            PluginConfiguration = configuration;
            PluginMessageFilter = PluginMessageFilter.CreateInstance(configuration);
        }
        protected ISoraService SoraService { get; }
        protected PluginConfiguration PluginConfiguration { get; }
        protected PluginMessageFilter PluginMessageFilter { get; }
    }
}
