using Core.Models;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;

namespace hospital_web.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Patient/";

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient.CreateClient("AuthorizedClient");
        }

        public async Task<List<Patient>?> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DataResponse<Patient>>(BaseUrl + "get-all");
            return response?.Data;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<SingleDataResponse<Patient>>($"{BaseUrl}get-by-id?id={id}");
            return response?.Data;
        }

        public async Task<bool> CreateAsync(Patient Patient)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl + "create", Patient);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Patient Patient)
        {
            var response = await _httpClient.PutAsJsonAsync(BaseUrl + "update", Patient);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(BaseUrl + "delete/" + id);
            return response.IsSuccessStatusCode;
        }


        public async Task<List<Patient>> SearchAsync(string term)
        {
            try
            {

                var response = await _httpClient.GetFromJsonAsync<PagedResponse<Patient>>(
            $"api/Patient/find?Search={Uri.EscapeDataString(term)}");

                return response?.Items ?? new List<Patient>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Search error: {ex.Message}");
                return new List<Patient>();


            }
        }
        public async Task<PagedResponse<Patient>> GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var url = $"api/Patient/get-all?page={page}&pageSize={pageSize}";
                Console.WriteLine($"Calling: {url}");

                var responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();

                // Use ApiResponse instead because of different structure
                var apiResponse = await responseMessage.Content.ReadFromJsonAsync<DataResponse<Patient>>();

                Console.WriteLine($"Received {apiResponse?.Data?.Count ?? 0} patients from API");

                // Convert to PagedResponse
                return new PagedResponse<Patient>
                {
                    Items = apiResponse?.Data ?? new(),
                    TotalItems = apiResponse?.Data?.Count ?? 0,
                    TotalPages = 1,           // Since your get-all doesn't return pagination yet
                    CurrentPage = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPagedAsync Error: {ex.Message}");
                return new PagedResponse<Patient>();
            }
        }

        public async Task<PagedResponse<Patient>> SearchPagedAsync(string term, int page, int pageSize)
        {
            try
            {
                var url = $"api/Patient/find?Search={Uri.EscapeDataString(term)}&page={page}&pageSize={pageSize}";

                Console.WriteLine($"Searching: {url}");

                var responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();

                var response = await responseMessage.Content.ReadFromJsonAsync<PagedResponse<Patient>>();

                return response ?? new PagedResponse<Patient>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SearchPagedAsync Error: {ex.Message}");
                return new PagedResponse<Patient>();
            }
        }

        public async Task<List<Patient>?> GetPatientCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<DataResponse<Patient>>(BaseUrl + "count");
            return response?.Data;
        }

    }

}

