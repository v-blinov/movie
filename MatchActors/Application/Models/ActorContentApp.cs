namespace MatchActors.Application.Models;

internal record ActorContentApp
{
    public MovieApp[] CastMovies { get; init; }
}