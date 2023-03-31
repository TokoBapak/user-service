/*using Dapper;
using Grpc.Core;
using TokoBapak.Protobuf.UserSchema;
using UserService.DB;

namespace UserService.GrpcServices;

public class GrpcEchoService : EchoService.EchoServiceBase
{
    private readonly DbConnFactory _dbConnFactory;

    public GrpcEchoService(DbConnFactory dbConnFactory)
    {
        _dbConnFactory = dbConnFactory;
    }

    public override async Task<EchoResponse> Echo(EchoRequest request, ServerCallContext context)
    {
        using var con = await _dbConnFactory.GetConnection();
        var uuid = await con.QuerySingleAsync<Guid>("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\"; SELECT uuid_generate_v4()");
        return new EchoResponse{ Response = $"{request.Request} {uuid}"};
    }
}*/