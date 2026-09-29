namespace hospital_api.Models
{
    public class Entity
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool? IsActive { get; set; }
        public int Id { get; set; }
    }
}
