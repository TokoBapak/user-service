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

var app = builder.Build();

app.MapGet("/generateJwtToken", JwtTokenGenerator.Generate(SecurityKey));
app.MapGrpcService<AuthenticationService>();
//app.MapGrpcService<GrpcEchoService>();
app.MapGrpcReflectionService();

app.Run();
