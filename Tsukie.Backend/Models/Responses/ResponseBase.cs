using Tsukie.Backend.Global;
using Tsukie.Backend.Models.Exceptions;

namespace Tsukie.Backend.Models.Responses
{
    public class ResponseBase
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public string Code { get; set; } = Constants.RESPONSE_CODE_OK;
        public string Message { get; set; } = Constants.RESPONSE_MESSAGE_OK;
        public Object? Result { get; set; }

        public void FillByException(ExceptionBase ex)
        {
            Code = ex.Code;
            Message = ex.Message;
        }
    }
}
