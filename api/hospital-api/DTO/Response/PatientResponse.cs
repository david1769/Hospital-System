using hospital_api.Models;

namespace hospital_api.DTO.Response
{
    public class PatientResponse : BaseResponse
    {
        public int Id { get; set; }

        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public long? NationalIdNumber { get; set; }
        public long? HealthInsuranceNumber { get; set; }
        public long? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public int? EmergencyContact { get; set; }

        public string? MedicalHistory { get; set; }

        
        public int? InsuranceProviderId { get; set; }
        public string? InsuranceProviderName { get; set; }
    }
}
