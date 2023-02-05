using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthJwtApi.Application.Config;
using AuthJwtApi.Application.Domain;
using AuthJwtApi.Application.Ports.In;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthJwtApi.Application.Services;

public class TokenService : ITokenServicePortIn
{
    private readonly AppSettings _appSettings;

    public TokenService(IOptions<AppSettings> appSettingsOptions)
    {
        _appSettings = appSettingsOptions.Value;
    }

    public string GenerateToken(Customer user)
    {
        var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddSeconds(3600),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}