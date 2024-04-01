using Back_end_API.Payload.DataRequests;
using Back_end_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_API.Controller
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("/api/auth/Register")]
        public IActionResult Register([FromForm] Request_Register request)
        {
            return Ok(_userService.Register(request));
        }
        [HttpPost("/api/auth/Login")]
        public IActionResult Login([FromForm] Request_Login login)
        {
            return Ok(_userService.Login(login));
        }
        [HttpGet("/api/auth/get-all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
    }
}
