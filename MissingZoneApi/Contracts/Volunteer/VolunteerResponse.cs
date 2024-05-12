using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.Admin;

public class VolunteerGetResponse
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }
    [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }
}