using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteer _volunteer;

        public VolunteerController(IVolunteer volunteer)
        {
            _volunteer = volunteer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] PageData pageData)
        {
            PayloadResponse<VolunteerInfo> response = await _volunteer.GetAll(pageData);
            return Ok(response);
        }
    }
}