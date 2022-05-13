using Microsoft.Extensions.Logging;
using Sora.Interfaces;
using Sora.OnebotAdapter;
using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Integration.Models
{
    public abstract class Plugin:IDisposable
    {
        private bool _disposed;
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    SoraService?.Dispose();
                }

                // 释放未托管的资源(未托管的对象)并重写终结器
                // 将大型字段设置为 null
                _disposed = true;
            }
        }

        // // 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~Plugin()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
