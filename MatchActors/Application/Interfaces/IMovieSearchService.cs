using MatchActors.Application.Models;

namespace MatchActors.Application.Interfaces;

internal interface IMovieSearchService
{
    Task<ActorContentApp> GetActorContent(string actor, CancellationToken token);
    string[] GetCommonContent(IEnumerable<MovieApp> actor1Content, IEnumerable<MovieApp> actor2Content);
    string[] GetOnlyMovieCommonContent(IEnumerable<MovieApp> actor1Content, IEnumerable<MovieApp> actor2Content);
}
