namespace Tsukie.Backend.Models.Exceptions
{
    public abstract class ExceptionBase : ApplicationException
    {
        public abstract string ErrorMessage { get; }
        public abstract string Code { get; }

        protected ExceptionBase()
        {
        }

        protected ExceptionBase(string message, Exception ex) : base(message, ex)
        {

        }

        protected ExceptionBase(string message) : base(message)
        {

        }

        public override string Message
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(base.Message))
                {
                    return $"{ErrorMessage}: {base.Message}";
                }

                if (InnerException != null)
                {
                    return $"{ErrorMessage}: {InnerException.Message}";
                }

                return $"{ErrorMessage}.";


            }
        }
    }
}
