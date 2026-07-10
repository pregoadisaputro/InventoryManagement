using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryManagement.Api.Data.Entity;
using Microsoft.IdentityModel.Tokens;

namespace InventoryManagement.Api.Features.Authentication.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public DateTimeOffset ExpiresAt { get; private set; }

    public string GenerateToken(User user)
    {
        var jwt = configuration.GetSection("Jwt");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var expirationMinutes = int.Parse(jwt["ExpirationMinutes"]!);

        ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(expirationMinutes);

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: ExpiresAt.UtcDateTime,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
