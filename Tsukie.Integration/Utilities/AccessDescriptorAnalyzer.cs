using Tsukie.Integration.Models.Access;

namespace Tsukie.Integration.Utilities
{
    internal class AccessDescriptorAnalyzer
    {
        public static bool TryAnalyze(string descriptor,out AccessRecord record)
        {
            descriptor = descriptor.Trim();
            record = new AccessRecord();
            string[] split = descriptor.Split(AccessDescriptorConstants.HEADER_SPLITTER);
            if (split.Length != 2)
            {
                return false;
            }

            if (TryAnalyzeAccess(split[0],out Access access))
            {
                record.Access = access;
            }
            else
            {
                return false;
            }

            if (TryAnalyzeAccessTargetAndType(split[1], out AccessTarget target, out AccessTargetType type))
            {
                record.Target = target;
                record.TargetType = type;
            }
            else
            {
                return false;
            }

            return true;
        }

        private static bool TryAnalyzeAccessTargetAndType(string content, out AccessTarget target, out AccessTargetType type)
        {
            content = content.Trim();
            target = new AccessTarget();
            type = AccessTargetType.Unknown;
            if (content.IndexOf(AccessDescriptorConstants.DEPENDENCE_SPLITTER) < -1)
            {
                type = AccessTargetType.Account;
                if (TryAnalyzeAccountTypedAccessTarget(content, out AccessTarget targetResult))
                {
                    target = targetResult;
                    return true;
                }
            }
            else
            {
                if(TryAnalyzeGroupOrAccountInGroupTypedAccessTarget(content,out AccessTarget targetResult, out AccessTargetType typeResult))
                {
                    target = targetResult;
                    type = typeResult;
                    return true;
                }
            }
            
            return false;
        }

        private static bool TryAnalyzeGroupOrAccountInGroupTypedAccessTarget(string content, out AccessTarget target,
            out AccessTargetType type)
        {
            target = new AccessTarget();
            type = AccessTargetType.Unknown;
            string[] split = content.Split(AccessDescriptorConstants.DEPENDENCE_SPLITTER);
            if (split.Length == 2)
            {
                string group = split[0].Trim();
                string account = split[1].Trim();
                if (string.IsNullOrWhiteSpace(group) || string.IsNullOrWhiteSpace(account))
                {
                    return false;
                }
                type = account.Equals(AccessDescriptorConstants.WILDCARD_ALL.ToString(),
                    StringComparison.InvariantCultureIgnoreCase) ? 
                    AccessTargetType.Group : 
                    AccessTargetType.AccountInGroup;

                target.GroupId = group;
                target.AccountId = account;
            }

            return false;
        }
        private static bool TryAnalyzeAccountTypedAccessTarget(string content, out AccessTarget target)
        {
            target = new AccessTarget();
            content = content.Trim();
            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }
            target.AccountId = content;
            return true;
        }

        private static bool TryAnalyzeAccess(string content,out Access access)
        {
            content = content.Trim();
            if (content.Equals(AccessDescriptorConstants.ALLOW_PREFIX, StringComparison.InvariantCultureIgnoreCase))
            {
                access = Access.Allowed;
                return true;
            }
            if (content.Equals(AccessDescriptorConstants.DENY_PREFIX, StringComparison.InvariantCultureIgnoreCase))
            {
                access = Access.Denied;
                return true;
            }

            access = Access.Unknown;
            return false;
        }
    }
}
