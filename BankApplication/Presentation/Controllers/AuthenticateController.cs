using DBLayer.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        IAuthenticateBusiness _AuthenticateBusinessAuthenticate;
        public AuthenticateController(IAuthenticateBusiness authenticateBusinessAuthenticate)
        {
            _AuthenticateBusinessAuthenticate = authenticateBusinessAuthenticate;
        }
        [HttpPost]
        [Route("authentication")]
        [AllowAnonymous]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = _AuthenticateBusinessAuthenticate.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
