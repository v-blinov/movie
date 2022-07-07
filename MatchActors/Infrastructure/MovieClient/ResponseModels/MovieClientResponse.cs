namespace MatchActors.Infrastructure.MovieClient.ResponseModels;

internal record MovieClientResponse
{
    public IEnumerable<Actor>? Results { get; init; } = Enumerable.Empty<Actor>();
}
