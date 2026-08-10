using hospital_api.DTO.Request;
using hospital_api.DTO.Response;
using hospital_api.Models;

namespace hospital_api.Interface.Services
{
    public interface IPatientService : IBaseService<Patient, PatientResponse, PatientRequest,FilterableRequest>
    {
        Task<CountResponse>GetTotalPatientsAsync();
 
    }
}
