namespace hospital_api.Models
{
    public class ReferenceData : Entity
    {
        public int? ReferenceDataCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public ReferenceDataCategory? ReferenceDataCategory { get; set; }
    }
}
