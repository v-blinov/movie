using MatchActors.Domain.Dtos;

namespace MatchActors.Domain.InfrastructureContracts;

public interface IMovieClient
{
    Task<MovieClientActorsResponse?> GetActorId(string actor, CancellationToken token);
    Task<ActorContent?> GetActorContent(string actorId, CancellationToken token);
}
