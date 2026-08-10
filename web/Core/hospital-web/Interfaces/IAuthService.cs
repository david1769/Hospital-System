namespace hospital_web.Services
{
    public interface IAuthService
    {
        Task<string?> GetTokenAsync();
        Task SetTokenAsync(string token);
        Task RemoveTokenAsync();
    }
}