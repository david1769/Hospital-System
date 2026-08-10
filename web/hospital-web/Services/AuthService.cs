using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace hospital_web.Services
{

    public class AuthService : IAuthService

    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private const string TokenKey = "authToken";
       
        public AuthService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        }

        public async Task SetTokenAsync(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
            SetHttpClientHeader(_httpClient, token);
        }

        public async Task RemoveTokenAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            SetHttpClientHeader(_httpClient, null);
        }

        // Sets the token on the HttpClient's default headers
        public void SetHttpClientHeader(HttpClient client, string? token)
        {
            client.DefaultRequestHeaders.Authorization =
                !string.IsNullOrEmpty(token)
                ? new AuthenticationHeaderValue("Bearer", token)
                : null;
        }
    }
}