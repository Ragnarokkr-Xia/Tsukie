
using Microsoft.Extensions.Logging;
using Sora.Interfaces;
using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Sample.Plugin
{
    public class MyPlugin:Integration.Models.Plugin
    {
        public MyPlugin(ISoraService service,PluginConfiguration configuration,ILogger<MyPlugin> logger) : base(service,configuration,logger)
        {
        }


        public new static string PluginName => "插件示例";
        public new static string PluginDescription => "此为示例插件";
        public new static string PluginVersion => "0.0.0.0";
        public new static string PluginAuthorName => "Tsukie";
        public new static string PluginWebPageUrl => @"about://blank";
        public new static bool PluginNewVersionPublished => false;
        public new static string PluginNewVersionWebPageUrl => @"about://blank";
    }
}