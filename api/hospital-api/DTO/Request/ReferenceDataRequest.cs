using hospital_api.Models;

namespace hospital_api.DTO.Request
{
    public class ReferenceDataRequest : BaseRequest
    {
        public int? ReferenceDataCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
