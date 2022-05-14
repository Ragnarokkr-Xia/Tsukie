using Tsukie.Integration.Models.Access;
using Tsukie.Integration.Models.Configuration;
using Tsukie.Integration.Utilities;

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

        public List<AccessRecord> GetAccessRecordList()
        {
            IEnumerable<string> descriptors = PluginConfiguration.AccessDescriptors;
            if (descriptors == null)
            {
                return null;
            }

            List<AccessRecord> records = new List<AccessRecord>();
            foreach (var descriptor in descriptors)
            {
                if (AccessDescriptorAnalyzer.TryAnalyze(descriptor, out AccessRecord record))
                {
                    records.Add(record);
                }
            }

            return records;
        }

        public bool FilterMessageByGroupId(string groupId)
        {
            IEnumerable<AccessRecord> allRecords = GetAccessRecordList();
            if (allRecords == null)
            {
                return true;
            }
            IEnumerable<AccessRecord> targetRecords = allRecords.Where(t=>
                t.TargetType == AccessTargetType.Group &&
                t.Target.GroupId.Equals(groupId, StringComparison.InvariantCultureIgnoreCase)).ToList();
            bool allowed = targetRecords.Any(t =>
                t.Type == AccessType.Allowed);
            bool denied = targetRecords.Any(t =>
                t.Type != AccessType.Allowed);
            bool result = allowed && !denied;
            return result;
        }
        public bool FilterMessageByAccountId(string accountId)
        {
            IEnumerable<AccessRecord> allRecords = GetAccessRecordList();
            if (allRecords == null)
            {
                return true;
            }
            IEnumerable<AccessRecord> targetRecords = allRecords.Where(t =>
                t.TargetType == AccessTargetType.Account &&
                t.Target.AccountId.Equals(accountId, StringComparison.InvariantCultureIgnoreCase)).ToList();
            bool allowed = targetRecords.Any(t =>
                t.Type == AccessType.Allowed);
            bool denied = targetRecords.Any(t =>
                t.Type != AccessType.Allowed);
            bool result = allowed && !denied;
            return result;
        }

        public bool FilterMessageByAccountIdAndGroupId(string accountId, string groupId)
        {
            return true;
        }
    }
}
