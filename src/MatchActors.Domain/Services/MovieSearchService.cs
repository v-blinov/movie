using MatchActors.Domain.Dtos;
using MatchActors.Domain.Exceptions;
using MatchActors.Domain.InfrastructureContracts;

namespace MatchActors.Domain.Services;

public sealed class MovieSearchService : IMovieSearchService
{
    private readonly IActorRepository _actorRepository;
    private readonly IMovieClient _movieClient;

    public MovieSearchService(IActorRepository actorRepository, IMovieClient movieClient)
    {
        _actorRepository = actorRepository;
        _movieClient = movieClient;
    }
    
    public string[] GetCommonContent(IEnumerable<ActorsContentItem> actor1Content, IEnumerable<ActorsContentItem> actor2Content)
    {
        // TODO: не работать с моделями уровня App
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

    public async Task<ActorContent> GetActorContent(string actor, CancellationToken token)
    {
        var actorId = await GetActorId(actor, token);
        var actorContent = await _movieClient.GetActorContent(actorId, token);

        if(actorContent is null || actorContent.CastMovies is null || !actorContent.CastMovies.Any())
            return new ActorContent { CastMovies = Enumerable.Empty<ActorsContentItem>() };
        
        return new ActorContent
        {
            CastMovies = actorContent.CastMovies.Select(p => new ActorsContentItem
            {
                Id = p.Id,
                Role = p.Role,
                Title = p.Title
            }).ToArray()
        };
    }
    
    private async Task<string> GetActorId(string actor, CancellationToken token)
    {
        var actorId = await _actorRepository.GetActorId(actor, token);

        if(string.IsNullOrEmpty(actorId))
        {
            var actors = await _movieClient.GetActorId(actor, token);
            actorId = actors?.Results?.FirstOrDefault(p => string.Compare(p.Title, actor, StringComparison.InvariantCultureIgnoreCase) == 0)?.Id;
        }

        if(string.IsNullOrEmpty(actorId))
            throw new ActorNotFoundException($"Actor '{actor}' was not found");

        //TODO: Положить значение в базу

        return actorId;
    }
}
