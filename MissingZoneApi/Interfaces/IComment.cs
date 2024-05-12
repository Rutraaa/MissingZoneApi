using MissingZoneApi.Contracts.Comments;

namespace MissingZoneApi.Interfaces;

public interface IComment
{
    Task Verify(int commentId);
    Task<List<CommentInfo>> GetAnomimList();
    Task<List<CommentInfo>> GetList(int misssingId);
    Task Create(CommentRequest request);
}