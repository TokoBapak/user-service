using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Authentication;
using UserService.DB;
using UserService.Infrastructures;

SymmetricSecurityKey SecurityKey = new (Guid.NewGuid().ToByteArray());
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddKeyPerFile("/Properties/Secrets", optional: true, reloadOnChange: true);

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.PostgreSql))));

builder.Services.AddAuthorization(options => options.AddJwtPolicy());
builder.Services.AddAuthentication().AddJwtBearer(options => options.SetupValidationsParams(SecurityKey));
builder.Services.AddScoped<DbConnFactory>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

var app = builder.Build();

//app.MapGet("/generateJwtToken", JwtTokenGenerator.Generate(SecurityKey));  commented for now, will revisit soon
app.MapGrpcService<AuthenticationGrpcService>();
app.MapGrpcReflectionService();

app.Run();
