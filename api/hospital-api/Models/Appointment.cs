

namespace hospital_api.Models
{
    public class Appointment : Entity
    {
        public Patient? Patient { get; set; }

        public int? PatientId { get; set; }
        public Doctor? Doctor { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? AppointmentDate  { get; set; }
        public ReferenceData? Status { get; set; }
        public int? StatusId { get; set; }
        public ReferenceData? Department { get; set; } 
        public int? DepartmentId { get; set; } 
        public string? Notes { get; set; }
        public bool FollowUpRequired { get; set; } 
        




    }
}
