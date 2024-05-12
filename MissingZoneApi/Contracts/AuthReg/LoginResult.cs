using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.AuthReg;

public class LoginResult
{
    public bool IsExist { get; set; }
    public string Messsage { get; set; }
}