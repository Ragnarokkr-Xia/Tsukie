namespace Tsukie.Backend.Models.Exceptions
{
    public class PluginInstanceNotFoundException:ExceptionBase
    {
        public PluginInstanceNotFoundException()
        {

        }
        public PluginInstanceNotFoundException(string message, Exception ex) : base(message, ex)
        {

        }

        public PluginInstanceNotFoundException(string message) : base(message)
        {

        }
        public override string ErrorMessage => "Plugin instance with specified criteria cannot be found";
        public override string Code  => "PLUGIN_INSTANCE_NOT_FOUND";
    }
}
