namespace Tsukie.Backend.Models.Exceptions
{
    public class PluginTypeNotFoundException:ExceptionBase
    {
        public PluginTypeNotFoundException()
        {

        }
        public PluginTypeNotFoundException(string message, Exception ex) : base(message, ex)
        {

        }

        public PluginTypeNotFoundException(string message) : base(message)
        {

        }
        public override string ErrorMessage => "Plugin type with specified criteria cannot be found";
        public override string Code  => "PLUGIN_TYPE_NOT_FOUND";
    }
}
