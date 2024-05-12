using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.MissingPost;

public class MissingPostInfo
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Contact info is required")]
    public string ContactInfo { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    public string FatherName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? CreateDate { get; set; }

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    // Assuming Coordinates are stored as a string, may need further validation
    [Required(ErrorMessage = "Coordinates are required")]
    public string Coordinates { get; set; }

    [Required(ErrorMessage = "At least one photo is required")]
    [MinLength(1, ErrorMessage = "At least one photo is required")]
    public List<string> Photos { get; set; }
}