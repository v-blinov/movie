namespace MatchActors.Infrastructure.MovieClient.ResponseModels;

internal record Movie
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string Role { get; init; }
}