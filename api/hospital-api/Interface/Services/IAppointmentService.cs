using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Models;

namespace hospital_api.Interface.Services
{
    public interface IAppointmentService : IBaseService<Appointment, AppointmentResponse, AppointmentRequest, FilterableRequest>
    {
        Task<CountResponse> GetTodaysAppointmentsCountAsync();
        Task<CountResponse> GetTotalAppointmentsCountAsync();
        Task<CountResponse> GetPendingAppointmentsCountAsync();
        Task<DataResponse<List<AppointmentResponse>>> GetTodaysAppointmentsList();


    }

}
