using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.Comments;

public class CommentRequest
{
    [Required(ErrorMessage = "MissingPostId is required")]
    public int MissingPostId { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    public string Comment { get; set; }
}