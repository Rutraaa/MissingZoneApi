using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts.Comments;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IComment _comment;

        public CommentController(IComment comment)
        {
            _comment = comment;
        }

        [Authorize]
        [HttpPost("/{commentId}")]
        public async Task<IActionResult> VerifyComment(int commentId)
        {
            _comment.Verify(commentId);
            return Ok();
        }

        [Authorize]
        [HttpGet("anonim/{missingPostId}")]
        public async Task<IActionResult> GetAnomimList(int missingPostId)
        {
           List<CommentInfo> response = await _comment.GetAnomimList(missingPostId);
           return Ok(response);
        }

        [Authorize]
        [HttpGet("/{missingPostId}")]
        public async Task<IActionResult> GetList(int missingPostId)
        {
            List<CommentInfo> response = await _comment.GetList(missingPostId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequest request)
        {
            await _comment.Create(request);
            return Ok();
        }
    }
}