namespace MatchActors.Domain.Dtos;

public record MovieClientActorsResponse
{
    public IEnumerable<Actor>? Results { get; init; } = Enumerable.Empty<Actor>();
}
