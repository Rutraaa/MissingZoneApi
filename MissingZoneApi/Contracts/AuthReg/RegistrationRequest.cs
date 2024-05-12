using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.AuthReg;

public class RegistrationRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    public string Photo { get; set; }

    [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Organization name is required")]
    public string OrganizationName { get; set; }
}