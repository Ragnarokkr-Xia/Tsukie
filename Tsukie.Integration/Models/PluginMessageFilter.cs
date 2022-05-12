using Tsukie.Integration.Models.Configuration;

namespace Tsukie.Integration.Models
{
    public class PluginMessageFilter
    {
        private PluginMessageFilter(PluginConfiguration configuration)
        {
            PluginConfiguration = configuration;
        }

        public static PluginMessageFilter CreateInstance(PluginConfiguration configuration)
        {
            return new PluginMessageFilter(configuration);
        }

        private PluginConfiguration PluginConfiguration { get; }

        private bool FilterMessageByGroupId(string groupId) => true;
        private bool FilterMessageByAccountId(string accountId) => true;

        private bool FilterMessageByAccountIdAndGroupId(string accountId) => true;
    }
}
