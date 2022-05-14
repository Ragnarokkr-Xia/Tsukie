namespace Tsukie.Backend.Models.Exceptions
{
    public class InstanceStartFailedException:ExceptionBase
    {
        public InstanceStartFailedException()
        {

        }
        public InstanceStartFailedException(string message, Exception ex) : base(message, ex)
        {

        }

        public InstanceStartFailedException(string message) : base(message)
        {

        }
        public override string ErrorMessage => "Plugin instance failed to start";
        public override string Code => "PLUGIN_INSTANCE_START_FAILED";
    }
}
