using Dapper;
using Npgsql;

namespace MatchActors.Infrastructure.Storage;

internal sealed class ActorRepository : IActorRepository
{
    private readonly string _connection;
    
    public ActorRepository(IConfiguration configuration)
    {
        _connection = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<string?> GetActorId(string actor, CancellationToken token)
    {
        const string baseQuery = "select actor_id from actors where name=@actor;";
        
        await using var connection = new NpgsqlConnection(_connection);
        await connection.OpenAsync(token);

        return await connection.QueryFirstOrDefaultAsync<string>(baseQuery, new { actor });
    }
}
