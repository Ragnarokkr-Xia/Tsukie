namespace Tsukie.Integration.Models.Access
{
    internal class AccessRecord
    {
        internal AccessTarget Target { get; set; } = new AccessTarget();
        internal AccessTargetType TargetType { get; set; }
        internal AccessType Type { get; set; } = AccessType.Denied;
    }
}
