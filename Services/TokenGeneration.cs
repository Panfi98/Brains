using System.Security.Claims;
using System.Text;
using BrainsToDo.Interfaces;
using BrainsToDo.Models;
using Microsoft.IdentityModel.Tokens;

namespace BrainsToDo.Services;

public class TokenGeneration : ITokenGeneration
{
    private readonly IConfiguration _configuration;

    public TokenGeneration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ??
                                         throw new InvalidOperationException("JWT Key is not configured"));

        int expirationHours = int.TryParse(_configuration["Jwt:ExpireHours"], out var parsed) ? parsed : 3;
        string issuer = _configuration["Jwt:Issuer"] ?? "defaultIssuer";
        string audience = _configuration["Jwt:Audience"] ?? "defaultAudience";

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            }),
            Expires = DateTime.UtcNow.AddHours(expirationHours),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}