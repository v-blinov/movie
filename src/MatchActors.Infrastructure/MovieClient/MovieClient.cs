using System.Net.Http.Json;
using MatchActors.Domain.Dtos;
using MatchActors.Domain.InfrastructureContracts;

namespace MatchActors.Infrastructure.MovieClient;

public class MovieClient : IMovieClient
{
    private readonly HttpClient _httpClient;
    private readonly MovieClientOptions _movieClientOptions;

    public MovieClient(HttpClient httpClient, MovieClientOptions movieClientOptions)
    {
        _httpClient = httpClient;
        _movieClientOptions = movieClientOptions;
    }
    
    public async Task<MovieClientActorsResponse?> GetActorId(string actor, CancellationToken token)
    {
        var query = $"SearchName/{_movieClientOptions.AppId}/{actor}";
        
        return await _httpClient.GetFromJsonAsync<MovieClientActorsResponse>(query,  token);
    }

    public async Task<ActorContent?> GetActorContent(string actorId, CancellationToken token)
    {
        var query = $"Name/{_movieClientOptions.AppId}/{actorId}";
        return await _httpClient.GetFromJsonAsync<ActorContent>(query, token);
    }
}
