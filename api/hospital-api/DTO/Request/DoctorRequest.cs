using hospital_api.Models;

namespace hospital_api.DTO.Request
{
    public class DoctorRequest : BaseRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long? PhoneNumber { get; set; }
        public int? SpecialtyId { get; set; }
        public int? DepartmentId { get; set; }
        public long? LicenseNumber { get; set; }

        public int? ScheduleId { get; set; }
        public string? ProfileDescription { get; set; }
        public int? YearsOfExperience { get; set; }
        public int? QualificationId { get; set; }

        public string? OfficeAddress { get; set; }
    }
}
