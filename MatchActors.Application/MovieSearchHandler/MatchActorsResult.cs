namespace MatchActors.Application.MovieSearchHandler;

internal record MatchActorsResult
{
    public IEnumerable<string> Movies { get; init; } = Enumerable.Empty<string>();
}
