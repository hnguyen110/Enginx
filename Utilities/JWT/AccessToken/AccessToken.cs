using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Utilities.JWT.AccessToken;

public class AccessToken : IAccessToken
{
    private readonly IConfiguration _configuration;

    public AccessToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string CreateAccessToken(Credential credential)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, credential.Id.ToString()),
            new(ClaimTypes.Name, credential.Username ?? "")
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").Value)),
            SecurityAlgorithms.HmacSha512Signature);
        var description = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials
        };
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(description);
        return handler.WriteToken(token);
    }
}