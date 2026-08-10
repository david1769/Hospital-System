using Core.Models;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;

namespace hospital_web.Services
{
    public class ReferenceDataService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/ReferenceData/"; // relative if HttpClient has BaseAddress set
        private const string BaseUrlWithCategory = "api/ReferenceDataCategory/"; // relative if HttpClient has BaseAddress set
        public ReferenceDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ReferenceData>?> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ReferenceDataResponse>(BaseUrl + "get-all");
            return response?.Data;

        }


        public async Task<bool> CreateAsync(ReferenceData ReferenceData)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl + "create", ReferenceData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ReferenceData ReferenceData)
        {
            var response = await _httpClient.PutAsJsonAsync(BaseUrl + "update", ReferenceData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(BaseUrl + "delete/" + id);
            return response.IsSuccessStatusCode;
        }


        public async Task<DataResponse<ReferenceData>?> GetByCategoryAsync(int id)
        {
            try
            {
                // Call the API and get the wrapped response
                var response = await _httpClient.GetFromJsonAsync<DataResponse<ReferenceData>>(
                    $"{BaseUrl}get-by-referencecategory?referencecategoryid={id}");

                if (response != null) 
                {
                    return response; // Extract the list
                }
                else
                {
                    // Handle errors (e.g., NotFound = 404)
                    Console.Error.WriteLine($"Error fetching reference data: Status 404");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call failed: {ex.Message}");
                return null;
            }

        }


        public async Task<List<ReferenceDataCategory>?> GetByReferenceCategoryAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReferenceDataCategory>>($"{BaseUrlWithCategory}get-all");
        }

    }


}