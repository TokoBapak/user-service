// commented for now but might be useful soon
/*
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace UserService.Authentication;

public static class JwtTokenGenerator
{
    private static readonly JwtSecurityTokenHandler JwtTokenHandler = new ();
    public static RequestDelegate Generate(SymmetricSecurityKey SecurityKey)
    {
        return context =>
        {
            var name = context.Request.Query["name"].ToString();
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name is not specified.");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, name) };
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
            return context.Response.WriteAsync(JwtTokenHandler.WriteToken(token));
        };
    }
}
*/
