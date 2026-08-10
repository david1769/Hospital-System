using Microsoft.AspNetCore.Components.Authorization;

namespace Services
{
    public interface ICustomAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void NotifyUserLogin();
        void NotifyUserLogout();
    }
}