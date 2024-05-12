using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.Comments;
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
            Comment comment = _mzonedbContext.Comments.First(item => item.CommentId == commentId);
            comment.IsVerified = true;
            await _mzonedbContext.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<CommentInfo>> GetAnomimList()
    {
        List<Comment> list = await _mzonedbContext.Comments
            .Where(item => item.IsVerified == false).ToListAsync();
        List<User> listUsers = await _mzonedbContext.Users.ToListAsync();
        var result = list.Join(listUsers, comment => comment.UserId, user => user.Email, (comment, user) =>
            new CommentInfo
            {
                CommnetId = comment.CommentId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                OrganizationName = null,
                Comment = comment.Comment1,
                CreatedDate = comment.CreatedDate.Value
            }).ToList();
        return result;
    }

    public async Task<List<CommentInfo>> GetList(int misssingId)
    {
        List<Comment> list = await _mzonedbContext.Comments
            .Where(item => item.MissingPostId == misssingId && item.IsVerified == true).ToListAsync();
        List<User> listUsers = await _mzonedbContext.Users.ToListAsync();
        var result = list.Join(listUsers, comment => comment.UserId, user => user.Email, (comment, user) =>
            new CommentInfo
            {
                CommnetId = comment.CommentId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                OrganizationName = null,
                Comment = comment.Comment1,
                CreatedDate = comment.CreatedDate.Value
            }).ToList();
        return result;
    }

    public async Task Create(CommentRequest request)
    {
        bool isUser = await _mzonedbContext.Users.AnyAsync(item => item.Email == request.UserId);
        if (isUser)
        {
            await _mzonedbContext.Comments.AddRangeAsync(new Comment
            {
                MissingPostId = request.MissingPostId,
                UserId = request.UserId,
                Comment1 = request.Comment,
                IsVerified = false,
                CreatedDate = DateTime.Now
            });
        }
        bool isVolunteer = await _mzonedbContext.Volunteers.AnyAsync(item => item.Email == request.UserId);
        if (isVolunteer)
        {
            await _mzonedbContext.Comments.AddRangeAsync(new Comment
            {
                MissingPostId = request.MissingPostId,
                UserId = request.UserId,
                Comment1 = request.Comment,
                IsVerified = false,
                CreatedDate = DateTime.Now
            });
        }

        await _mzonedbContext.SaveChangesAsync();
    }
}