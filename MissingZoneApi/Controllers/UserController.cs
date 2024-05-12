using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUser _user;

    public UserController(IUser user)
    {
        _user = user;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] PageData pageData)
    {
        var response = await _user.GetAll(pageData);
        return Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("/verifyUser/{email}")]
    public async Task<IActionResult> VerifyUser(string email)
    {
        await _user.Verify(email);
        return Ok();
    }
}