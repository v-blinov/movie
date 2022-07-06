using MatchActors.Infrastructure.MovieClient.ResponseModels;

namespace MatchActors.Infrastructure.MovieClient;

internal interface IMovieClient
{
    Task<MovieClientResponse?> GetActorId(string actor, CancellationToken token);
    Task<ActorContent?> GetActorContent(string actorId, CancellationToken token);
}
