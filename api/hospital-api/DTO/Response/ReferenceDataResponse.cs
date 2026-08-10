using hospital_api.Models;

namespace hospital_api.DTO.Response
{
    public class ReferenceDataResponse : BaseResponse
    {
        public int Id { get; set; }
        public int? ReferenceDataCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? ReferenceDataCatName { get; set; }

       
    }
}
