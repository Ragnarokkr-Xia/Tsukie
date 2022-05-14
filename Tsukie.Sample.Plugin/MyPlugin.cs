
using Microsoft.Extensions.Logging;
using Sora.Interfaces;
using Tsukie.Integration.Interfaces;
using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Sample.Plugin
{
    public class MyPlugin:Integration.Models.Plugin,IStartStop,IDisposable
    {
        private bool _disposed;

        public MyPlugin(ISoraService service,PluginConfiguration configuration, ILogger logger) : base(service,configuration,logger)
        {
            service.Event.OnPrivateMessage += (type, args) =>
            {
                return ValueTask.CompletedTask;
            };
            service.Event.OnGroupMessage +=(type, args) =>
            {
                return ValueTask.CompletedTask;
            };
        }

        public new static string PluginId => "f2cb8a08-9e97-4622-98e1-4c64613f9acf";
        public new static string PluginName => "插件示例";
        public new static string PluginDescription => "此为示例插件";
        public new static string PluginVersion => "0.0.0.0";
        public new static string PluginAuthorName => "Tsukie";
        public new static string PluginWebPageUrl => @"about://blank";
        public new static bool PluginNewVersionPublished => false;
        public new static string PluginNewVersionWebPageUrl => @"about://blank";
        void IStartStop.Start()
        {
            
        }

        void IStartStop.Stop()
        {
            
        }


        void IDisposable.Dispose()
        {
            
        }
    }
}