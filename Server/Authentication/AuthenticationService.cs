using System.Threading.Tasks;
using Grpc.Core;
using TokoBapak.Protobuf.AuthenticationSchema;
using TokoBapak.Protobuf.CommonSchema;
using UserService.Authentication;

public class AuthenticationService : Authentication.AuthenticationBase
{
    private readonly LoginRequestValidator _loginRequestValidator;
    private const int DefaultAttempts = 2;

    public AuthenticationService(LoginRequestValidator loginRequestValidator)
    {
        _loginRequestValidator = loginRequestValidator;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var validationResult = _loginRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToLoginResponse(DefaultAttempts);
        }

        return await Task.FromResult(new LoginResponse
        {
            TokenSetReply = new TokenSetReply{ AccessToken = "dummy_access_token" }
        });
    }

    public override Task<EmptyReply> Logout(LogoutRequest request, ServerCallContext context)
    {
        return base.Logout(request, context);
    }

    public override Task<LoginResponse> Refresh(RefreshRequest request, ServerCallContext context)
    {
        return base.Refresh(request, context);
    }
}
