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
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
        [HttpPut("/api/auth/update-user")]
        public IActionResult Update([FromForm] int id, [FromForm] Request_Register request)
        {
            return Ok(_userService.UpdateUser(id, request));
        }
        [HttpPut("/api/auth/update-role")]
        public IActionResult UpdateRole([FromForm] int UserID, [FromForm] int RoleID)
        {
            return Ok(_userService.UpdateRole(UserID, RoleID));
        }
        [HttpDelete("/api/auth/delete-user")]
        public IActionResult DeleteUser([FromForm] int UserID)
        {
            return Ok(_userService.Delete(UserID));
        }
    }
}
