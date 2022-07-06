using MatchActors.Domain.Interfaces;
using MatchActors.Infrastructure.MovieClient.ResponseModels;

namespace MatchActors.Infrastructure.MovieClient;

internal class MovieClient : IMovieClient
{
    private readonly HttpClient _httpClient;
    private readonly MovieClientOptions _movieClientOptions;

    public MovieClient(HttpClient httpClient, MovieClientOptions movieClientOptions)
    {
        _httpClient = httpClient;
        _movieClientOptions = movieClientOptions;
    }
    
    public async Task<MovieClientResponse?> GetActorId(string actor, CancellationToken token)
    {
        var query = $"SearchName/{_movieClientOptions.AppId}/{actor}";
        
        return await _httpClient.GetFromJsonAsync<MovieClientResponse>(query,  token);
    }

    public async Task<ActorContent?> GetActorContent(string actorId, CancellationToken token)
    {
        var query = $"Name/{_movieClientOptions.AppId}/{actorId}";
        return await _httpClient.GetFromJsonAsync<ActorContent>(query, token);
    }
}
