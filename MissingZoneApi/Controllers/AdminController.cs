using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        private readonly IUser _user;
        private readonly IVolunteer _volunteer;
        private readonly IComment _comment;

        public AdminController(IAdmin admin, IUser user, IVolunteer volunteer, IComment comment)
        {
            _admin = admin;
            _user = user;
            _volunteer = volunteer;
            _comment = comment;
        }

        [HttpGet("/{adminEmail}")]
        public async Task<IActionResult> Get(string adminEmail)
        {
            return Ok( await _admin.GetMe(adminEmail));
        }

        [Authorize]
        [HttpPost("/verifyUser/{email}")]
        public async Task<IActionResult> VerifyUser(string email)
        {
            await _user.Verify(email);
            return Ok();
        }

        [Authorize]
        [HttpPost("/verifyVolunteer/{email}")]
        public async Task<IActionResult> VerifyVolunteer(string email)
        {
            await _volunteer.Verify(email);
            return Ok();
        }

        [Authorize]
        [HttpPost("/verifyComment/{commentId}")]
        public async Task<IActionResult> VerifyComment(int commentId)
        {
            _comment.Verify(commentId);
            return Ok();
        }
    }
}