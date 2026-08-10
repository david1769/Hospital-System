using hospital_api.Models;

namespace hospital_api.DTO.Request
{
    public class AppointmentRequest : BaseRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? StatusId { get; set; }
        public int DepartmentId { get; set; }
        public string? Notes { get; set; }
        public bool FollowUpRequired { get; set; }
    }
}
