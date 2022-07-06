namespace MatchActors.Infrastructure.MovieClient.ResponseModels;

internal record MovieClientResponse
{
    public Actor[] Results { get; init; }
}
