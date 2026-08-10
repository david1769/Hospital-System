using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Patient
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

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get => _createdAt ??= DateTime.UtcNow;
            set => _createdAt = value;
        }

    }
}

