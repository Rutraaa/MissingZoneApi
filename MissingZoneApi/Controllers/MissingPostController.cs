using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.MissingPost;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;
using MissingZoneApi.Services;

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
                Coordinates = string.Join(",", model.Coordinates),
                FatherName = model.FatherName,
                BirthDate = model.BirthDate,
                CreateDate = createdDate,
                City = model.City
            };

            await _missingPost.Create(missingPost);

            var missingPostsId = await _missingPost.GetIdByDate(createdDate);

            foreach (var content in model.Contents)
            {
                var photo = new Photo()
                {
                    MissingPostId = missingPostsId,
                    Content = content
                };

                await _photo.Create(photo);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMissingPostsRequest pageData)
        {
            try
            {
                var missingPosts = await _missingPost.GetAll();
                var paginationService = new PaginationService<MissingPost>();
                var pagedResponse = await paginationService.GetPagedDataAsync(missingPosts, pageData);

                var filteredPosts = (IEnumerable<MissingPost>)pagedResponse.Data;

                if (pageData.BirthDate.HasValue)
                {
                    var birthDate = pageData.BirthDate.Value.Date; // Відсікати час, лише дата
                    filteredPosts = filteredPosts.Where(post =>
                        post.BirthDate.HasValue &&
                        post.BirthDate.Value.ToShortDateString() == birthDate.ToShortDateString());
                }

                if (!string.IsNullOrEmpty(pageData.FirstName))
                    filteredPosts = filteredPosts.Where(post => post.FirstName.ToLower() == pageData.FirstName.ToLower());

                if (!string.IsNullOrEmpty(pageData.LastName))
                    filteredPosts = filteredPosts.Where(post => post.LastName.ToLower() == pageData.LastName.ToLower());

                if (!string.IsNullOrEmpty(pageData.FatherName))
                    filteredPosts = filteredPosts.Where(post => post.FatherName.ToLower() == pageData.FatherName.ToLower());

                if (!string.IsNullOrEmpty(pageData.City))
                    filteredPosts = filteredPosts.Where(post => post.City.ToLower() == pageData.City.ToLower());

                pagedResponse.Data = filteredPosts.ToList();

                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var missingPost = await _missingPost.Get(id);
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
