namespace Tsukie.Backend.Models.Exceptions
{
    public class GeneralException:ExceptionBase
    {
        public GeneralException()
        {

        }
        public GeneralException(string message, Exception ex) : base(message, ex)
        {

        }

        public GeneralException(string message) : base(message)
        {

        }
        public override string ErrorMessage => "An unexpected situation is present";
        public override string Code => "GENERAL_UNEXPECTED";
    }
}
