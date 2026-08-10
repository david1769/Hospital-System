using Core.Models;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;

namespace hospital_web.Services
{
    public class AppointmentService
{
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Appointment/";

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }

        public async Task<List<Appointment>?> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DataResponse<Appointment>>(BaseUrl + "get-all");
            return response?.Data;
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<SingleDataResponse<Appointment>>($"{BaseUrl}get-by-id?id={id}");
            return response?.Data;
        }

        public async Task<bool> CreateAsync(Appointment appointment)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl + "create", appointment);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            var response = await _httpClient.PutAsJsonAsync(BaseUrl + "update", appointment);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(BaseUrl + "delete/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Appointment>?> GetTotalAppointmentsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DataResponse<Appointment>>(BaseUrl + "get-total-appointments");
            return response?.Data;
        }

        public async Task<List<Appointment>?> GetPendingAppointmentsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DataResponse<Appointment>>(BaseUrl + "get-pending");
            return response?.Data;
        }

        public async Task<List<Appointment>?> GetTodayAppointmentAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DataResponse<Appointment>>(BaseUrl + "get-todays");
            return response?.Data;
        }


    }

}

