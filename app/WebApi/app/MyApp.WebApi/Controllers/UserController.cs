using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MyApp.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("api/GetUserData")]
        [HttpGet]
        public IActionResult GetUserData ()
        {
            var user = HttpContext.User.Identity;
            var json = JsonSerializer.Serialize (user);
            return Ok(json);
        }
    }
}
