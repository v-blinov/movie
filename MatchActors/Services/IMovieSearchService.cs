using MatchActors.Contracts;

namespace MatchActors.Services;

public interface IMovieSearchService
{
    Task<MatchActorsResponse> MovieSearch(MatchActorsRequest request, CancellationToken token);
}
