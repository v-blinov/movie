using MatchActors.Domain.Dtos;

namespace MatchActors.Domain.Services;

public interface IMovieSearchService
{
    Task<ActorContent> GetActorContent(string actor, CancellationToken token);
    string[] GetCommonContent(IEnumerable<ActorsContentItem> actor1Content, IEnumerable<ActorsContentItem> actor2Content);
    string[] GetOnlyMovieCommonContent(IEnumerable<ActorsContentItem> actor1Content, IEnumerable<ActorsContentItem> actor2Content);
}
