using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.Comments;

public class CommentInfo
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    public string OrganizationName { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    public string Comment { get; set; }

    [Required(ErrorMessage = "Created date is required")]
    public DateTime CreatedDate { get; set; }
}