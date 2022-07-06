namespace MatchActors.Infrastructure.MovieClient.ResponseModels;

internal record ActorContent
{
    public Movie[] CastMovies { get; init; }
}