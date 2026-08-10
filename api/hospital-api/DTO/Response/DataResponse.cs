namespace hospital_api.DTO.Response
{
    public class DataResponse<T> : BaseResponse where T : class
    {
        public T Data { get; set; }
    }
}
