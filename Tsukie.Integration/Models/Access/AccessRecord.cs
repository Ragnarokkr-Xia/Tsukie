namespace Tsukie.Integration.Models.Access
{
    public class AccessRecord
    {
        public AccessTarget Target { get; set; } = new AccessTarget();
        public AccessTargetType TargetType { get; set; }
        public AccessType Type { get; set; } = AccessType.Denied;
    }
}
