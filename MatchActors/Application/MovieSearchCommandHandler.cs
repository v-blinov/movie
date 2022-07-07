using MatchActors.Application.Interfaces;
using MatchActors.Application.Models;
using MediatR;

namespace MatchActors.Application;

internal sealed class MovieSearchCommandHandler : IRequestHandler<MatchActorsCommand, MatchActorsResult>
{
    private readonly IMovieSearchService _movieSearchService;

    public MovieSearchCommandHandler(IMovieSearchService movieSearchService)
    {
        _movieSearchService = movieSearchService;
    }
    
    public async Task<MatchActorsResult> Handle(MatchActorsCommand request, CancellationToken cancellationToken)
    {
        var actor1Content = await _movieSearchService.GetActorContent(request.Actor1, cancellationToken);
        if (!actor1Content.CastMovies.Any())
            return new MatchActorsResult { Movies = Enumerable.Empty<string>() };
        
        var actor2Content = await _movieSearchService.GetActorContent(request.Actor2, cancellationToken);
        if (!actor2Content.CastMovies.Any())
            return new MatchActorsResult { Movies = Enumerable.Empty<string>() };

        var content = request.MoviesOnly
            ? _movieSearchService.GetOnlyMovieCommonContent(actor1Content.CastMovies, actor2Content.CastMovies) 
            : _movieSearchService.GetCommonContent(actor1Content.CastMovies, actor2Content.CastMovies);

        return new MatchActorsResult
        {
            Movies = content
        };
    }
}

