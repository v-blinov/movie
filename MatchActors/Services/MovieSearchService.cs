using MatchActors.Application.Interfaces;
using MatchActors.Application.Models;
using MatchActors.Exceptions;
using MatchActors.Infrastructure.MovieClient;
using MatchActors.Infrastructure.Storage;

namespace MatchActors.Services;

internal sealed class MovieSearchService : IMovieSearchService
{
    private readonly IActorRepository _actorRepository;
    private readonly IMovieClient _movieClient;

    public MovieSearchService(IActorRepository actorRepository, IMovieClient movieClient)
    {
        _actorRepository = actorRepository;
        _movieClient = movieClient;
    }
    
    public string[] GetCommonContent(ActorContentApp actor1Content, ActorContentApp actor2Content)
    {
        // TODO: не работать с моделями уровня App
        
        var content1Ids = actor1Content.CastMovies.Select(p => p.Id);
        var content2Ids = actor2Content.CastMovies.Select(p => p.Id);

        var intersectActorContentIds = content1Ids.Intersect(content2Ids, StringComparer.Ordinal);

        return actor1Content.CastMovies
                            .Where(p => intersectActorContentIds.Contains(p.Id))
                            .Select(p => p.Title)
                            .ToArray();
    }

    public async Task<ActorContentApp> GetActorContent(string actorId, CancellationToken token)
    {
        var actor = await GetActorId(actorId, token);
        var actorContent = await _movieClient.GetActorContent(actor, token);

        if(actorContent is null)
            return new ActorContentApp();
        
        return new ActorContentApp
        {
            CastMovies = actorContent.CastMovies.Select(p => new MovieApp
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
            actorId = actors?.Results.FirstOrDefault(p => string.Compare(p.Title, actor, StringComparison.InvariantCultureIgnoreCase) == 0)?.Id;
        }

        if(string.IsNullOrEmpty(actorId))
            throw new ActorNotFoundException($"Actor '{actor}' was not found");

        //TODO: Положить значение в базу

        return actorId;
    }
}
