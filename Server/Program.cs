using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UserService.Helpers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireClaim(ClaimTypes.Name);
    });
});
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateActor = false,
                ValidateLifetime = true,
                IssuerSigningKey = SecurityKey
            };
    });

var app = builder.Build();


app.MapGet("/generateJwtToken", context =>
{
    var token = context.Request.Query["name"].ToString().GenerateJwtToken(SecurityKey);
    return context.Response.WriteAsync(JwtTokenHandler.WriteToken(token));
});

app.MapGrpcService<AuthenticationService>();
app.MapGrpcReflectionService();
app.Run();

public partial class Program
{
    private static readonly JwtSecurityTokenHandler JwtTokenHandler = new ();
    private static readonly SymmetricSecurityKey SecurityKey = new (Guid.NewGuid().ToByteArray());
}