namespace MatchActors.Infrastructure.MovieClient.ResponseModels;

internal record Actor
{
    public string Id { get; init; } = null!;
    public string Title { get; init; } = null!;
}