using System.Threading.Tasks;
using Grpc.Core;
using TokoBapak.Protobuf.AuthenticationSchema;
using TokoBapak.Protobuf.CommonSchema;

public class AuthenticationService : Authentication.AuthenticationBase
{
    public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        // Implement your login logic here
        return Task.FromResult(new LoginResponse());
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