using MatchActors.Domain.Dtos;

namespace MatchActors.Services.Services;

public interface IActorService
{
    Task<ActorContent> GetActorContent(string actor, CancellationToken token);
}
