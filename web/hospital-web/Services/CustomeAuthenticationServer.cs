using hospital_web.Services;
using Microsoft.AspNetCore.Components.Authorization;

using System.Security.Claims;

namespace Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        // Define an anonymous user identity
        private readonly AuthenticationState _anonymous =
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public CustomAuthenticationStateProvider(IAuthService authService)
        {
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // 1. Check if a token exists
            var token = await _authService.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                // If no token, return the anonymous user
                return _anonymous;
            }

            // 2. If a token exists, create the claims identity (User is logged in)
            var identity = new ClaimsIdentity(new[]
            {
                // In a real app, this information would come from decoding the JWT.
                new Claim(ClaimTypes.Name, "Hospital User"),
                new Claim(ClaimTypes.Role, "Admin") // Simplified: assuming all logged-in users are 'Admin' for now
            }, "JwtBearer"); // Use "JwtBearer" as the authentication type

            // 3. Return the authenticated user state
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        // Method to call when the user logs in
        public void NotifyUserLogin()
        {
            var authState = GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(authState);
        }

        // Method to call when the user logs out
        public void NotifyUserLogout()
        {
            // Tell Blazor the state has changed to anonymous
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }
    }

}