using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hospital_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("get-users")]

        public IActionResult GetUsers()
        {
            var users = new[]
            {
            new { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

            return Ok(users);
        }
    }
}
