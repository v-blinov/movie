namespace MatchActors.Domain.Models;

public record MovieClientActorsResponse
{
    public IEnumerable<Actor>? Results { get; init; } = Enumerable.Empty<Actor>();
}
