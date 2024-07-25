using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TasteTrove.Application.Common.Interfaces.Authentication;
using TasteTrove.Application.Common.Interfaces.Services;
using TasteTrove.Domain.Entities;

namespace TasteTrove.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }
    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super-doper-secret-key-guess-no-one-can-crack-this-one")
            ),
            SecurityAlgorithms.HmacSha256
        );
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer, audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddDays(_jwtSettings.ExpiryDays), claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
