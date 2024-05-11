using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok( await _admin.GetMe());
        }
    }
}