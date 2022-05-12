namespace Tsukie.Integration.Models.Access
{
    internal class AccessRecord
    {
        internal AccessTarget Target { get; set; } = new AccessTarget();
        internal AccessTargetType TargetType { get; set; }
        internal Access Access { get; set; } = Access.Denied;
    }
}
