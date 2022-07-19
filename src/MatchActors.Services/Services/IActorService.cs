using MatchActors.Services.Dtos;

namespace MatchActors.Services.Services;

public interface IActorService
{
    Task<ActorContentDto> GetActorContent(string actor, CancellationToken token);
}
