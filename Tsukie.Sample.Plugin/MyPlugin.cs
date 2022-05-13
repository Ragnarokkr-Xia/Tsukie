
using Microsoft.Extensions.Logging;
using Sora.Interfaces;
using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Sample.Plugin
{
    public class MyPlugin:Integration.Models.Plugin
    {
        private bool _disposed;

        public MyPlugin(ISoraService service,PluginConfiguration configuration,ILogger<MyPlugin> logger) : base(service,configuration,logger)
        {
        }
        public new static string PluginId => "f2cb8a08-9e97-4622-98e1-4c64613f9acf";
        public new static string PluginName => "插件示例";
        public new static string PluginDescription => "此为示例插件";
        public new static string PluginVersion => "0.0.0.0";
        public new static string PluginAuthorName => "Tsukie";
        public new static string PluginWebPageUrl => @"about://blank";
        public new static bool PluginNewVersionPublished => false;
        public new static string PluginNewVersionWebPageUrl => @"about://blank";

        protected override void Dispose(bool disposing)
        {
            // Don't dispose more than once.
            if (_disposed)
                return;
            if (disposing)
            {
                // Free managed resources here.
            }
            // Free unmanaged resources here.

            // Dispose resources in base class
            // The base class will call GC.SuppressFinalize()
            base.Dispose(disposing);

            // Set derived class disposed flag:
            _disposed = true;
        }
    }
}