using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.AuthReg;

public class LoginResult
{
    [Required]
    public bool IsExist { get; set; }
    public string Messsage { get; set; }
}