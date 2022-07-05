namespace MatchActors.Contracts;

public record MatchActorsRequest
{
    public string Actor1 { get; init; } = null!;
    public string Actor2 { get; init; } = null!;
    public bool MoviesOnly { get; init; }
}