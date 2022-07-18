using Dapper;
using MatchActors.Services.InfrastructureContracts;
using Npgsql;

namespace MatchActors.Infrastructure.Storage;

public sealed class ActorRepository : IActorRepository
{
    private readonly string _connection;
    
    public ActorRepository(DbConnectionOptions connectionOptions)
    {
        _connection = connectionOptions.Connection;
    }
    
    public async Task<string?> GetActorId(string actorName, CancellationToken token)
    {
        const string baseQuery = "select actor_id from actors where name=@actor;";
        
        await using var connection = new NpgsqlConnection(_connection);
        await connection.OpenAsync(token);

        return await connection.QueryFirstOrDefaultAsync<string>(baseQuery, new { actor = actorName });
    }
}
