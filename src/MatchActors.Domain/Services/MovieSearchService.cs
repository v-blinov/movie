using MatchActors.Domain.Dtos;

namespace MatchActors.Domain.Services;

public sealed class MovieSearchService : IMovieSearchService
{
    public string[] GetCommonContent(IEnumerable<ActorsContentItem> actor1Content, IEnumerable<ActorsContentItem> actor2Content)
    {
        if(!actor1Content.Any() || !actor2Content.Any())
            return Array.Empty<string>();
        
        var content1Ids = actor1Content.Select(p => p.Id);
        var content2Ids = actor2Content.Select(p => p.Id);

        var intersectActorContentIds = content1Ids.Intersect(content2Ids, StringComparer.Ordinal);

        return actor1Content
               .Where(p => intersectActorContentIds.Contains(p.Id))
               .Select(p => p.Title)
               .ToArray();
    }

    public string[] GetOnlyMovieCommonContent(IEnumerable<ActorsContentItem> actor1Content, IEnumerable<ActorsContentItem> actor2Content)
    {
        ActorsContentItem[] GetMoviesOnly(IEnumerable<ActorsContentItem> contents) 
            => contents.Where(m => m.Role == "Actress" || m.Role == "Actor").ToArray();

        var actor1MoviesOnly = GetMoviesOnly(actor1Content);
        var actor2MoviesOnly = GetMoviesOnly(actor2Content);
        
        return GetCommonContent(actor1MoviesOnly, actor2MoviesOnly);
    }
}
