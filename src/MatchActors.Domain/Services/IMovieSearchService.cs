using MatchActors.Domain.Models;

namespace MatchActors.Domain.Services;

public interface IMovieSearchService
{
    string[] GetCommonContent(IEnumerable<ActorsContentItem> actor1Content, IEnumerable<ActorsContentItem> actor2Content);
    string[] GetOnlyMovieCommonContent(IEnumerable<ActorsContentItem> actor1Content, IEnumerable<ActorsContentItem> actor2Content);
}
