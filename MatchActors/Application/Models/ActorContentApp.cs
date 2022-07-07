namespace MatchActors.Application.Models;

internal record ActorContentApp
{
    public IEnumerable<MovieApp> CastMovies { get; init; }
}