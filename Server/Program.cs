using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using UserService.Authentication;
using UserService.DB;

SymmetricSecurityKey SecurityKey = new (Guid.NewGuid().ToByteArray());
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddAuthorization(options => options.AddJwtPolicy());
builder.Services.AddAuthentication().AddJwtBearer(options => options.SetupValidationsParams(SecurityKey));
builder.Services.AddScoped<DbConnFactory>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

var app = builder.Build();

//app.MapGet("/generateJwtToken", JwtTokenGenerator.Generate(SecurityKey));  commented for now, will revisit soon
app.MapGrpcService<AuthenticationService>();
app.MapGrpcReflectionService();

app.Run();
