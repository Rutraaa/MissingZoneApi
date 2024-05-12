using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Interfaces;
using MissingZoneApi.Services;

namespace MissingZoneApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAdmin _admin;
    private readonly IConfiguration _configuration;
    private readonly IUser _user;
    private readonly IVolunteer _volunteer;

    public AuthController(IAdmin admin, IConfiguration configuration, IUser user, IVolunteer volunteer)
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
        try
        {
            if (request.OrganizationName.IsNullOrEmpty())
            {
                var isExist = _user
                    .CheckIsExist(new LoginRequest { Email = request.Email, Password = request.Password }).IsExist;
                if (isExist) return BadRequest("Current email does exist");
                await _user.Create(request);
            }
            else
            {
                var isExist = _volunteer.CheckIsExist(new LoginRequest
                    { Email = request.Email, Password = request.Password }).IsExist;
                if (isExist) return BadRequest("Current email does exist");
                await _volunteer.Create(request);
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginResponse>> LoginUser([FromBody] LoginRequest userLogin)
    {
        try
        {
            var isAdmin = _admin.CheckIsExist(userLogin);

            if (isAdmin.IsExist)
            {
                var tokenService = new TokenService(_configuration);
                var token = "bearer " + tokenService.CreateToken(userLogin.Email);

                return Ok(new LoginResponse { Token = token, Role = "admin" });
            }

            if (!isAdmin.Messsage.IsNullOrEmpty())
                return BadRequest(isAdmin.Messsage);

            var isUser = _user.CheckIsExist(userLogin);

            if (isUser.IsExist)
            {
                var tokenService = new TokenService(_configuration);
                var token = "bearer " + tokenService.CreateToken(userLogin.Email);

                return Ok(new LoginResponse { Token = token, Role = "user" });
            }

            if (!isUser.Messsage.IsNullOrEmpty())
                return BadRequest(isUser.Messsage);

            var isVolunteer = _volunteer.CheckIsExist(userLogin);

            if (isVolunteer.IsExist)
            {
                var tokenService = new TokenService(_configuration);
                var token = "bearer " + tokenService.CreateToken(userLogin.Email);

                return Ok(new LoginResponse { Token = token, Role = "volunteer" });
            }

            if (!isVolunteer.Messsage.IsNullOrEmpty())
                return BadRequest(isVolunteer.Messsage);

            return BadRequest("User not found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}