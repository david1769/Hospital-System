using hospital_api.Constants;

namespace hospital_api.DTO.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            StatusCode = ResponseStatus.Ok;
            Messages = new List<MessageResponse>();

        }
        public int StatusCode { get; set; }
        public List<MessageResponse> Messages { get; set; }



    }


}


