using Core.Models;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;

namespace hospital_web.Services
{
  
public class DashboardService
{
        private readonly HttpClient _httpClient;
        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
     
        public async Task<List<Appointment>> GetAppointmentsAsync(DateTime start, DateTime end,
            string? doctorId = null, string? departmentId = null)
        {
            var url = BuildAppointmentsUrl(start, end, doctorId, departmentId);


            var response = await _httpClient.GetFromJsonAsync<List<Appointment>>(url);
            return response ?? new List<Appointment>();
        }

        // Add more methods below...



        public async Task<int> GetTodayAppointmentsCountAsync(string? doctorId = null, string? deptId = null)
        {
            var today = DateTime.Today;
            var appointments = await GetAppointmentsAsync(today, today.AddDays(1), doctorId, deptId);
            return appointments.Count;
        }

        public async Task<int> GetUpcomingAppointmentsCountAsync(int days = 7)
        {
            var start = DateTime.Today;
            var end = start.AddDays(days);
            var appointments = await GetAppointmentsAsync(start, end);
            return appointments.Count;
        }

        private string BuildAppointmentsUrl(DateTime start, DateTime end, string? doctorId, string? deptId)
        {
            var uri = new UriBuilder("http://localhost:5297/api/appointments");
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            query["start"] = start.ToString("o");
            query["end"] = end.ToString("o");
            if (!string.IsNullOrEmpty(doctorId)) query["doctorId"] = doctorId;
            if (!string.IsNullOrEmpty(deptId)) query["departmentId"] = deptId;

            uri.Query = query.ToString()!;
            return uri.ToString();
        }

        public async Task<int> GetTotalPatientsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CountResponse>("api/Patient/count");
            return response?.Count ?? 0;
        }

        public async Task<int> GetTodayAppointmentsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CountResponse>("api/Appointment/get-todays-count");
            return response?.Count ?? 0;
        }

        public async Task<int> GetActiveDoctorsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CountResponse>("api/Doctor/count");
            return response?.Count ?? 0;
        }

        public async Task<int> GetPendingAppointmentsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CountResponse>("api/Appointment/get-pending");
            return response?.Count ?? 0;
        }

        //Lists

        public async Task<List<Appointment>> GetTodayAppointmentsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DataResponse<Appointment>>("api/Appointment/get-todays-list");
            return response?.Data ?? new List<Appointment>();
        }

    }
}
