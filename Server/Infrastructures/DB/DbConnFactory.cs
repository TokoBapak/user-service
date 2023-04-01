using System.Data;
using Npgsql;

namespace UserService.DB;

public class DbConnFactory
{
    private readonly string? _connectionString;
    
    public DbConnFactory(IConfiguration config)
    {
        _connectionString = config["ConnectionStrings:PostgreSql"];
    }

    public async Task<IDbConnection> GetConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
