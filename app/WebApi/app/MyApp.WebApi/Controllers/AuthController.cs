using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Constants;

namespace MyApp.WebApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("api/Login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties(), AuthentificationContants.AuthenticationScheme);
        }
    }
}
