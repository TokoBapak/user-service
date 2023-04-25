using System.Threading.Tasks;
using FluentValidation.Results;
using Grpc.Core;
using TokoBapak.Protobuf.AuthenticationSchema;
using TokoBapak.Protobuf.CommonSchema;
using UserService.Authentication;
using UserService.Infrastructures;

public class AuthenticationGrpcService : Authentication.AuthenticationBase
{
    private readonly LoginRequestValidator _loginRequestValidator;
    private const int DefaultAttempts = 2;

    public AuthenticationGrpcService(LoginRequestValidator loginRequestValidator)
    {
        _loginRequestValidator = loginRequestValidator;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        await ValidateLoginRequest(request, context);
        CalculateRemainingAttempt(request, context);
        GenerateToken(request, context);
        return ConstructLoginResponse(request, context);


        /*if (!validationResult.IsValid)
        {
            var errorDescriptors = validationResult.ToLoginResponse();
            var errorResponse = new LoginErrorResponse
            {
                Descriptors = { errorDescriptors },
                AttemptsRemaining = DefaultAttempts
            };

            return new LoginResponse { LoginErrorResponse = errorResponse };
        }

        return new LoginResponse
        {
            TokenSetReply = new TokenSetReply{ AccessToken = "dummy_access_token" }
        };*/
    }

    public override Task<EmptyReply> Logout(LogoutRequest request, ServerCallContext context)
    {
        return base.Logout(request, context);
    }

    public override Task<LoginResponse> Refresh(RefreshRequest request, ServerCallContext context)
    {
        return base.Refresh(request, context);
    }


    private async Task ValidateLoginRequest(LoginRequest request, ServerCallContext context)
    {
        var validationResult = await _loginRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            context.SetError(validationResult.ToLoginResponse());
        }
    }

    private void CalculateRemainingAttempt(LoginRequest request, ServerCallContext context)
    {
    }

    private void GenerateToken(LoginRequest request, ServerCallContext context)
    {
    }

    private LoginResponse ConstructLoginResponse(LoginRequest request, ServerCallContext context)
    {
        return new LoginResponse();
    }
}
