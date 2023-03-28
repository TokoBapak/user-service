using System.Threading.Tasks;
using Grpc.Core;
using TokoBapak.Protobuf.AuthenticationSchema;

public class AuthenticationService : Authentication.AuthenticationBase
{
    public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        // Implement your login logic here
        return Task.FromResult(new LoginResponse());
    }
}