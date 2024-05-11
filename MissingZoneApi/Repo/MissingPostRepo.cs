using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts;
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

    public async Task<MissingPost> Read(int id)
    {
        try
        {
            var missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

            if (missingPost == null)
            {
                throw new ArgumentException("Missing post not found");
            }

            return await _mzonedbContext.MissingPosts.FindAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        var missingPost = await _mzonedbContext.MissingPosts.FindAsync(id);

        if (missingPost == null)
        {
            throw new ArgumentException("Missing post not found");
        }

        _mzonedbContext.MissingPosts.Remove(missingPost);
        await _mzonedbContext.SaveChangesAsync();
    }

    public async Task<int?> GetIdByDate(DateTime createdDate)
    {
        var missingPost = await _mzonedbContext.MissingPosts.FirstOrDefaultAsync(x=>x.CreateDate == createdDate);
        return missingPost?.MissingPostId;
    }
}