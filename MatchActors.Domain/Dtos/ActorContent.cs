namespace MatchActors.Domain.Dtos;

public record ActorContent
{
    public IEnumerable<ActorsContentItem>? CastMovies { get; init; } = Enumerable.Empty<ActorsContentItem>();
}