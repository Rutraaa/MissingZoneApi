using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts.Comments;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly IComment _comment;

    public CommentController(IComment comment)
    {
        _comment = comment;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("/{commentId}")]
    public async Task<IActionResult> VerifyComment(int commentId)
    {
        try
        {
            _comment.Verify(commentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("/anonim")]
    public async Task<IActionResult> GetAnomimListForAdmin()
    {
        try
        {
            var response = await _comment.GetAnomimList();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("/{missingPostId}")]
    public async Task<IActionResult> GetList(int missingPostId)
    {
        try
        {
            var response = await _comment.GetList(missingPostId);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CommentRequest request)
    {
        try
        {
            await _comment.Create(request);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}