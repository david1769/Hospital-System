namespace hospital_api.DTO.Response
{
    public class DoctorResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public long? PhoneNumber { get; set; }
        public int? SpecialtyId { get; set; }
        public string? SpecialtyName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? LicenseNumber { get; set; }

        public int? ScheduleId { get; set; }
        public string? ScheduleName { get; set; }
        public string? ProfileDescription { get; set; }
        public int? YearsOfExperience { get; set; }
        public int? QualificationId { get; set; }
        public string? QualificationName { get; set; }

        public string? OfficeAddress { get; set; }
    }
}
