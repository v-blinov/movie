namespace MatchActors.Domain.Models;

public record ActorContent
{
    public IEnumerable<ActorsContentItem>? CastMovies { get; init; } = Enumerable.Empty<ActorsContentItem>();
}