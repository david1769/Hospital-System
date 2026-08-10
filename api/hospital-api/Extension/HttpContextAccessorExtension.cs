using System.Linq;
using Microsoft.AspNetCore.Http;
namespace hospital_api.Extension
{
    public static class HttpContextAccessorExtension
    {
        public static string GetUser(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext == null)
                return "backend-service";

            string? headerValue = httpContextAccessor.HttpContext.Request.Headers["User-Name"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(headerValue))
            {
                // Temporary fallback – remove this later
                return "anonymous-dev";     // or "system", "test-user", Guid.Empty.ToString(), etc.
                                            // logger.LogWarning("No User-Name header provided – using fallback");
            }

            return headerValue;
        }
    }
}
