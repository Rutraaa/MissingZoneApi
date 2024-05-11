namespace MissingZoneApi.Contracts.Comments;

public class CommentRequest
{
    public int MissingPostId { get; set; }
    public string UserId { get; set; }
    public string Comment { get; set; }
}