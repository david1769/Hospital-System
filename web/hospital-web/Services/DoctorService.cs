using Core.Models;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;

namespace hospital_web.Services
{
    public class DoctorService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Doctor/"; // relative if HttpClient has BaseAddress set

        public DoctorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Doctor>?> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DoctorResponse>(BaseUrl + "get-all");
            return response?.Data;
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<SingleDataResponse<Doctor>>($"{BaseUrl}get-by-id?id={id}");
            return response?.Data;
        }

        public async Task<bool> CreateAsync(Doctor doctor)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl + "create", doctor);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            var response = await _httpClient.PutAsJsonAsync(BaseUrl + "update", doctor);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(BaseUrl + "delete/" + id);
            return response.IsSuccessStatusCode;
        }


        public async Task<PagedResponse<Doctor>> SearchPagedAsync(string term)
        {
            try
            {
                var url = $"api/Doctor/find?Search={Uri.EscapeDataString(term)}";

                Console.WriteLine($"Searching: {url}");

                var responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();

                var response = await responseMessage.Content.ReadFromJsonAsync<PagedResponse<Doctor>>();

                return response ?? new PagedResponse<Doctor>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SearchPagedAsync Error: {ex.Message}");
                return new PagedResponse<Doctor>();
            }
        }
        public async Task<List<Doctor>?> GetActiveDoctorsCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DoctorResponse>(BaseUrl + "count");
            return response?.Data;
        }



    }
}
