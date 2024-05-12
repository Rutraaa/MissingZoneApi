using Microsoft.EntityFrameworkCore;
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
            var missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

            if (missingPost == null) throw new ArgumentException("Missing post not found");

            var photos = await _mzonedbContext.Photos
                .Where(item => item.MissingPostId == missingPost.MissingPostId).Select(item => item.Content)
                .ToListAsync();

            var result = new MissingPostInfo
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

    public async Task<List<MissingPostInfo>> InsertPhotos(List<MissingPost> list)
    {
        var listPhotos = _mzonedbContext.Photos.ToList();
        var result = list.Select(item => new MissingPostInfo
        {
            MissingPostId = item.MissingPostId,
            Title = item.Title,
            Description = item.Description,
            ContactInfo = item.ContactInfo,
            UserId = item.UserId,
            FirstName = item.FirstName,
            LastName = item.LastName,
            FatherName = item.FatherName,
            BirthDate = item.BirthDate,
            CreateDate = item.CreateDate,
            City = item.City,
            Coordinates = item.Coordinates,
            PrePhoto = listPhotos.First(item => item.MissingPostId == item.MissingPostId).Content
        }).ToList();
        return result;
    }

    public async Task Delete(int id)
    {
        var missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

        if (missingPost == null) throw new ArgumentException("Missing post not found");

        _mzonedbContext.MissingPosts.Remove(missingPost);
        await _mzonedbContext.SaveChangesAsync();
    }

    public async Task<int?> GetIdByDate(DateTime createdDate)
    {
        var missingPost = await _mzonedbContext.MissingPosts.FirstOrDefaultAsync(x => x.CreateDate == createdDate);
        return missingPost?.MissingPostId;
    }

    public async Task<MissingPost> Read(int id)
    {
        var missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

        if (missingPost == null) throw new ArgumentException("Missing post not found");

        return missingPost;
    }
}