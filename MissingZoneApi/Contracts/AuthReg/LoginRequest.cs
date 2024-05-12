using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.AuthReg;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}