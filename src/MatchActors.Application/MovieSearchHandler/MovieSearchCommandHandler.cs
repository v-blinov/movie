using MatchActors.Domain.Services;
using MatchActors.Services.Services;
using MediatR;

namespace MatchActors.Application.MovieSearchHandler;

internal sealed class MovieSearchCommandHandler : IRequestHandler<MatchActorsCommand, MatchActorsResult>
{
    private readonly IMovieSearchService _movieSearchService;
    private readonly IActorService _actorService;

    public MovieSearchCommandHandler(IMovieSearchService movieSearchService, IActorService actorService)
    {
        _movieSearchService = movieSearchService;
        _actorService = actorService;
    }
    
    public async Task<MatchActorsResult> Handle(MatchActorsCommand request, CancellationToken cancellationToken)
    {
        var actor1Content = await _actorService.GetActorContent(request.Actor1, cancellationToken);
        if (actor1Content.CastMovies == null || !actor1Content.CastMovies.Any())
            return new MatchActorsResult { Movies = Enumerable.Empty<string>() };
        
        var actor2Content = await _actorService.GetActorContent(request.Actor2, cancellationToken);
        if (actor2Content.CastMovies == null || !actor2Content.CastMovies.Any())
            return new MatchActorsResult { Movies = Enumerable.Empty<string>() };

        var actorsCommonContent = request.MoviesOnly
            ? _movieSearchService.GetOnlyMovieCommonContent(
                actor1Content.CastMovies.Select(p => p.ConvertToActorsContentItem()).ToArray(), 
                actor2Content.CastMovies.Select(p => p.ConvertToActorsContentItem()).ToArray()) 
            : _movieSearchService.GetCommonContent(
                actor1Content.CastMovies.Select(p => p.ConvertToActorsContentItem()).ToArray(), 
                actor2Content.CastMovies.Select(p => p.ConvertToActorsContentItem()).ToArray());

        return new MatchActorsResult
        {
            Movies = actorsCommonContent
        };
    }
}

