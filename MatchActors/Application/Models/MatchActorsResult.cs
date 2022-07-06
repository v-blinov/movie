namespace MatchActors.Application.Models;

internal record MatchActorsResult
{
    public IEnumerable<string> Movies { get; init; } = Enumerable.Empty<string>();
}
