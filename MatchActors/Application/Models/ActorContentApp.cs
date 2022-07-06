namespace MatchActors.Application.Models;

public record ActorContentApp
{
    public MovieApp[] CastMovies { get; init; }
}

public record MovieApp
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string Role { get; init; }
}