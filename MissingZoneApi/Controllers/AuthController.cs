using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Interfaces;
using MissingZoneApi.Services;

namespace MissingZoneApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAdmin _admin;
        private readonly IUser _user;
        private readonly IVolunteer _volunteer;
        private readonly IConfiguration _configuration;

        public AuthController(IAdmin admin, IConfiguration configuration, IUser user, IVolunteer volunteer )
        {
            _configuration = configuration;
            _admin = admin;
            _user = user;
            _volunteer = volunteer;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            if (request.OrganizationName.IsNullOrEmpty())
            {
                bool isExist = _user.CheckIsExist(new LoginRequest { Email = request.Email, Password = request.Password }).IsExist;
                if (isExist)
                {
                    return BadRequest("Current email does exist");
                }
                await _user.Create(request);
            }
            else
            {
                bool isExist = _volunteer.CheckIsExist(new LoginRequest{Email = request.Email, Password = request.Password}).IsExist;
                if (isExist)
                {
                    return BadRequest("Current email does exist");
                }
                await _volunteer.Create(request);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginRequest userLogin)
        {
            LoginResponse isAdmin = _admin.CheckIsExist(userLogin);

            if (isAdmin.IsExist)
            {
                TokenService tokenService = new TokenService(_configuration);
                string token = "bearer " + tokenService.CreateToken(userLogin.Email);

                return Ok(token);
            }
            if (!isAdmin.Messsage.IsNullOrEmpty())
                return BadRequest(isAdmin.Messsage);

            LoginResponse isUser = _user.CheckIsExist(userLogin);

            if (isUser.IsExist)
            {
                TokenService tokenService = new TokenService(_configuration);
                string token = "bearer " + tokenService.CreateToken(userLogin.Email);

                return Ok(token);
            }
            if (isUser.Messsage.IsNullOrEmpty())
                return BadRequest(isUser.Messsage);

            LoginResponse isVolunteer = _volunteer.CheckIsExist(userLogin);

            if (isVolunteer.IsExist)
            {
                TokenService tokenService = new TokenService(_configuration);
                string token = "bearer " + tokenService.CreateToken(userLogin.Email);

                return Ok(token);
            }
            if (isVolunteer.Messsage.IsNullOrEmpty())
                return BadRequest(isVolunteer.Messsage);

            return BadRequest("User not found");
        }
    }
}
