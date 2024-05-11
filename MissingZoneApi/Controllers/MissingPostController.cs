using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts.MissingPost;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MissingPostController : ControllerBase
    {
        private readonly IMissingPost _missingPost;

        public MissingPostController(IMissingPost missingPost)
        {
            _missingPost = missingPost;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> Create([FromBody] CreateMissingPostRequest model)
        {

            var missingPost = new MissingPost
            {
                Title = model.Title,
                Description = model.Description,
                Photos = model.Photos,
                ContactInfo = model.ContactInfo,
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                BirthDate = model.BirthDate,
                CreateDate = DateTime.Now,
                City = model.City
            };

            // TODO: Save foto content

            await _missingPost.Create(missingPost);
            return Ok();
        }

        [HttpGet("/missingposts")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMissingPostsRequest request)
        {
            try
            {
                var missingPosts = await _missingPost.GetAll();

                int totalCount = missingPosts.Count();
                int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

                var pagedMissingPosts = missingPosts
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                return Ok(new
                {
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalPages = totalPages,
                    Data = pagedMissingPosts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/missingpost/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var missingPost = await _missingPost.Read(id);
            if (missingPost == null)
            {
                return NotFound("Missing post not found");
            }
            return Ok(missingPost);
        }


        [HttpDelete("/missingpost/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var missingPost = await _missingPost.Read(id);
            if (missingPost == null)
            {
                return NotFound("Missing post not found");
            }

            await _missingPost.Delete(id);
            return Ok("Missing post deleted successfully");
        }
    }
}
