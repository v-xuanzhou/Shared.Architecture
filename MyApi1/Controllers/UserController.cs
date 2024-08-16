using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Msic.Auth;

namespace UserApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;

        public UserController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpGet("login")]
        public string GetToken()
        {
            return _jwtHelper.CreateToken();
        }

        [Authorize]
        [HttpGet("test")]
        public string GetTest()
        {
            return "Test Authorize";
        }
    }
}
