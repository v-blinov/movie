namespace MatchActors.Infrastructure.MovieClient.ResponseModels;

internal record ActorContent
{
    public IEnumerable<Movie>? CastMovies { get; init; } = Enumerable.Empty<Movie>();
}