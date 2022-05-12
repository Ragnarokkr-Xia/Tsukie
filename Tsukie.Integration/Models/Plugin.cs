using Microsoft.Extensions.Logging;
using Sora.Interfaces;
using Sora.OnebotAdapter;
using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Integration.Models
{
    public abstract class Plugin
    {
        #region Plugin information

        public static string PluginName => string.Empty;
        public static string PluginDescription => string.Empty;
        public static string PluginVersion => string.Empty;
        public static string PluginAuthorName => string.Empty;
        public static string PluginWebPageUrl => string.Empty;
        public static bool PluginNewVersionPublished => false;
        public static string PluginNewVersionWebPageUrl => string.Empty;
        #endregion

        protected Plugin(ISoraService service,PluginConfiguration configuration, ILogger logger)
        {
            SoraService = service;
            PluginConfiguration = configuration;
            PluginMessageFilter = Models.PluginMessageFilter.CreateInstance(configuration);
            Logger = logger;
        }
        private ISoraService SoraService { get; }
        protected PluginConfiguration PluginConfiguration { get; }
        protected EventAdapter SoraServiceEventAdapter => SoraService.Event;
        protected PluginMessageFilter PluginMessageFilter { get; }
        protected ILogger Logger { get; }

    }
}
