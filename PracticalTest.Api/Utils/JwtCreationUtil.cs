using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticalTest.Api.Utils;

public static class JwtCreationUtil
{
    public static string CreateJwtToken(Claim[] claims, IConfiguration configuration)
    {
        var jwtKey = GetJwtToken(configuration);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public static string GetJwtToken(IConfiguration configuration)
    {
        var jwtKey = configuration.GetSection("AppSettings:Jwt:Token").Value;

        return jwtKey!;
    }
}
