using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts;
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
        private readonly IPhotoRepo _photo;

        public MissingPostController(IMissingPost missingPost, IPhotoRepo photo)
        {
            _missingPost = missingPost;
            _photo = photo;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMissingPostRequest model)
        {
            var createdDate = DateTime.Now;

            var missingPost = new MissingPost
            {
                Title = model.Title,
                Description = model.Description,
                ContactInfo = model.ContactInfo,
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                BirthDate = model.BirthDate,
                CreateDate = createdDate,
                City = model.City
            };

            await _missingPost.Create(missingPost);

            var missingPostsId = await _missingPost.GetIdByDate(createdDate);

            foreach (var conetents in model.Contents)
            {
                var photo = new Photo()
                {
                    MissingPostId = missingPostsId,
                    Content = conetents
                };

                await _photo.Create(photo);
            }

            return Ok();
        }

        [HttpGet]
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

                return Ok(new PayloadResponse<MissingPost>
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

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var missingPost = await _missingPost.Read(id);
            if (missingPost == null)
            {
                return NotFound("Missing post not found");
            }
            return Ok(missingPost);
        }


        [HttpDelete("/{id}")]
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
