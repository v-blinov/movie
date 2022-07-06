using MatchActors.Application.Models;

namespace MatchActors.Application.Interfaces;

internal interface IMovieSearchService
{
    Task<ActorContentApp> GetActorContent(string actorId, CancellationToken token);
    string[] GetCommonContent(ActorContentApp actor1Content, ActorContentApp actor2Content);
}
