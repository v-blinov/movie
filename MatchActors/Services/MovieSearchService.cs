using MatchActors.Contracts;
using MatchActors.Exceptions;
using MatchActors.Infrastructure.MovieClient;
using MatchActors.Infrastructure.MovieClient.ResponseModels;
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
    
    public async Task<MatchActorsResponse> MovieSearch(MatchActorsRequest request, CancellationToken token)
    {
        var actor1Content = await GetActorContent(request.Actor1, token);
        if (!actor1Content.CastMovies.Any())
            return new MatchActorsResponse { Movies = Enumerable.Empty<string>() };
        
        var actor2Content = await GetActorContent(request.Actor2, token);
        if (!actor2Content.CastMovies.Any())
            return new MatchActorsResponse { Movies = Enumerable.Empty<string>() };

        // фильтр MoviesOnly
        //if (request.MoviesOnly == true)
        //{
        //    movs1 = movs1.Where(m => m.Role == "Actress" || m.Role == "Actor").ToArray();
        //    movs1 = movs1.Where(m => m.Role == "Actress" || m.Role == "Actor").ToArray();
        //}

        return new MatchActorsResponse
        {
            Movies = GetCommonContent(actor1Content, actor2Content)
        };
    }

    private async Task<ActorContent> GetActorContent(string actorId, CancellationToken token)
    {
        var actor = await GetActorId(actorId, token);
        var actorContent = await _movieClient.GetActorContent(actor, token);

        return actorContent ?? new ActorContent();
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

    private string[] GetCommonContent(ActorContent actor1Content, ActorContent actor2Content)
    {
        var content1Ids = actor1Content.CastMovies.Select(p => p.Id);
        var content2Ids = actor2Content.CastMovies.Select(p => p.Id);

        var intersectActorContentIds = content1Ids.Intersect(content2Ids, StringComparer.Ordinal);

        return actor1Content.CastMovies
                            .Where(p => intersectActorContentIds.Contains(p.Id))
                            .Select(p => p.Title)
                            .ToArray();
    }
}
