using hospital_api.Constants;

namespace hospital_api.DTO.Response
{
    public class CountResponse : BaseResponse
    {
        public int Count { get; set; }

        public CountResponse(int count)
        {
            Count = count;
            StatusCode = ResponseStatus.Ok;
            Messages = new List<MessageResponse>();
        }


    }
}
