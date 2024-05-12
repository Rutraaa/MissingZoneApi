using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.AuthReg;

public class LoginResponse
{
    [Required(ErrorMessage = "Token is required")]
    public string Token { get; set; }
    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }
}