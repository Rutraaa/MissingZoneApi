using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.Comments;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IComment
{
    Task Verify(int commentId);
    Task<List<CommentInfo>> GetAnomimList(int misssingId);
    Task<List<CommentInfo>> GetList(int misssingId);
    Task Create(CommentRequest request);
}