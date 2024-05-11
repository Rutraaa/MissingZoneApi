namespace MissingZoneApi.Contracts.Comments;

public class CommentInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? OrganizationName { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; } 
}