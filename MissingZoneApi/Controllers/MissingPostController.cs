﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.MissingPost;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;
using MissingZoneApi.Services;

namespace MissingZoneApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MissingPostController : ControllerBase
{
    private readonly IMissingPost _missingPost;
    private readonly IMissingPostsFilterService _missingPostsFilterService;
    private readonly IPhotoRepo _photo;

    public MissingPostController(IMissingPost missingPost, IPhotoRepo photo,
        IMissingPostsFilterService missingPostsFilterService)
    {
        _missingPost = missingPost;
        _photo = photo;
        _missingPostsFilterService = missingPostsFilterService;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMissingPostRequest model)
    {
        try
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
                var photo = new Photo
                {
                    MissingPostId = missingPostsId,
                    Content = content
                };

                await _photo.Create(photo);
            }

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllMissingPostsRequest pageData)
    {
        try
        {
            var missingPosts = await _missingPost.GetAll();
            var filteredPosts = _missingPostsFilterService.FilterMissingPosts(missingPosts, pageData);

            var paginationService = new PaginationService<MissingPost>();
            var pagedResponse = await paginationService.GetPagedDataAsync(filteredPosts, pageData);

            var newList = await _missingPost.InsertPhotos(pagedResponse.Data);

            var response = new PayloadResponse<MissingPostInfo>
            {
                TotalCount = pagedResponse.TotalCount,
                PageNumber = pagedResponse.PageNumber,
                PageSize = pagedResponse.PageSize,
                TotalPages = pagedResponse.TotalPages,
                Data = newList
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var missingPost = await _missingPost.Get(id);
            if (missingPost == null) return NotFound("Missing post not found");
            return Ok(missingPost);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var missingPost = await _missingPost.Read(id);
            if (missingPost == null) return NotFound("Missing post not found");

            await _missingPost.Delete(id);
            return Ok("Missing post deleted successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}