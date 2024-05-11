using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] PageData pageData)
        {
            PayloadResponse<UserInfo> response = await _user.GetAll(pageData);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("/verifyUser/{email}")]
        public async Task<IActionResult> VerifyUser(string email)
        {
            await _user.Verify(email);
            return Ok();
        }
    }
}