namespace hospital_api.DTO.Request
{
    public class FilterableRequest : BaseRequest
    {
        public int Page { get; set; } = 1;  
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
    }
}
