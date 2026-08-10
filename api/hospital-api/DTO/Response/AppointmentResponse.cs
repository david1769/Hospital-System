namespace hospital_api.DTO.Response
{
    public class AppointmentResponse : BaseResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Notes { get; set; }
        public bool FollowUpRequired { get; set; }
    }
}
