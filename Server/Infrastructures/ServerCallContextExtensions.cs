using Grpc.Core;

namespace UserService.Infrastructures;

public static class ServerCallContextExtensions
{
    public const string ErrorKey = nameof(ErrorKey);

    public static bool HasError(this ServerCallContext context) =>
        context.UserState.ContainsKey(ErrorKey) && context.UserState[ErrorKey] is not null;

    public static void SetError(this ServerCallContext context, object error) =>
        context.UserState[ErrorKey] = error;
}
