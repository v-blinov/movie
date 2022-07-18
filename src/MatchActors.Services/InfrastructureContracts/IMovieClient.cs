using MatchActors.Domain.Dtos;

namespace MatchActors.Services.InfrastructureContracts;

public interface IMovieClient
{
    Task<MovieClientActorsResponse?> GetActorId(string actor, CancellationToken token);
    Task<ActorContent?> GetActorContent(string actorId, CancellationToken token);
}
