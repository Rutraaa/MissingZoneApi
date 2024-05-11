using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MissingZoneApi.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(string email)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email)
        };
        try
        {
            var tokenKey = _configuration.GetSection("AppSettings:Token").Value;
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(tokenKey);
            if (keyBytes.Length < 64)
            {
                Array.Resize(ref keyBytes, 64);
            }
            var key = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}