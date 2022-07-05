namespace MatchActors.Contracts;

public record MatchActorsResponse
{
    public IEnumerable<string> Movies { get; init; } = null!;
}
