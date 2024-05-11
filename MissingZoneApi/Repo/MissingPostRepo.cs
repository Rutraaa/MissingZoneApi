﻿using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.MissingPost;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class MissingPostRepo : IMissingPost
{
    public readonly mzonedbContext _mzonedbContext;

    public MissingPostRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }

    public async Task Create(MissingPost missingPost)
    {
        _mzonedbContext.MissingPosts.Add(missingPost);

        await _mzonedbContext.SaveChangesAsync();
    }

    public async Task<List<MissingPost>> GetAll()
    {
        return await _mzonedbContext.MissingPosts.ToListAsync();
    }

    public async Task<MissingPostInfo> Get(int id)
    {
        try
        {
            MissingPost missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

            if (missingPost == null)
            {
                throw new ArgumentException("Missing post not found");
            }

            List<string> photos = _mzonedbContext.Photos
                .Where(item => item.MissingPostId == missingPost.MissingPostId).Select(item => item.Content).ToList();

            MissingPostInfo result = new MissingPostInfo
            {
                Title = missingPost.Title,
                Description = missingPost.Description,
                ContactInfo = missingPost.ContactInfo,
                UserId = missingPost.UserId,
                FirstName = missingPost.FirstName,
                LastName = missingPost.LastName,
                FatherName = missingPost.FatherName,
                BirthDate = missingPost.BirthDate,
                CreateDate = missingPost.CreateDate,
                City = missingPost.City,
                Coordinates = missingPost.Coordinates,
                Photos = photos
            };

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        MissingPost missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

        if (missingPost == null)
        {
            throw new ArgumentException("Missing post not found");
        }

        _mzonedbContext.MissingPosts.Remove(missingPost);
        await _mzonedbContext.SaveChangesAsync();
    }

    public async Task<int?> GetIdByDate(DateTime createdDate)
    {
        MissingPost missingPost = await _mzonedbContext.MissingPosts.FirstOrDefaultAsync(x=>x.CreateDate == createdDate);
        return missingPost?.MissingPostId;
    }

    public async Task<MissingPost> Read(int id)
    {
        MissingPost missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

        if (missingPost == null)
        {
            throw new ArgumentException("Missing post not found");
        }

        return missingPost;
    }
}