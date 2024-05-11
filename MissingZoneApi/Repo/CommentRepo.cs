using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class CommentRepo : IComment
{
    public readonly mzonedbContext _mzonedbContext;

    public CommentRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }

    public async Task Verify(int commentId)
    {
        try
        {
            Comment comment = await _mzonedbContext.Comments.FirstAsync(item => item.CommentId == commentId);
            comment.IsVerified = true;
            await _mzonedbContext.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}