using MatchActors.Contracts;
using MatchActors.Infrastructure.MovieClient;
using MatchActors.Infrastructure.Storage;

namespace MatchActors.Services;

internal sealed class MovieSearchService : IMovieSearchService
{
    private readonly IActorRepository _actorRepository;
    private readonly IMovieClient _movieClient;

    public MovieSearchService(IActorRepository actorRepository, IMovieClient movieClient)
    {
        _actorRepository = actorRepository;
        _movieClient = movieClient;
    }
    
    public async Task<MatchActorsResponse> MovieSearch(MatchActorsRequest request, CancellationToken token)
    {
        var result = new List<string>();

        var val1 = await _actorRepository.GetActorId(request.Actor1, token);
        var val2 = await _actorRepository.GetActorId(request.Actor2, token);

        if(val1 == null)
        {
            var movieClientResponse = await _movieClient.GetActorId(request.Actor1, token);
            val1 = movieClientResponse?.Results.FirstOrDefault(p => string.Compare(p.Title, request.Actor1, StringComparison.InvariantCultureIgnoreCase) == 0)?.Id;
        }

        if(val2 == null)
        {
            var movieClientResponse = await _movieClient.GetActorId(request.Actor2, token);
            val2 = movieClientResponse?.Results.FirstOrDefault(p => string.Compare(p.Title, request.Actor2, StringComparison.InvariantCultureIgnoreCase) == 0)?.Id;
        }

        if (val1 != null && val2 != null)
        {
            var actor1Content = await _movieClient.GetActorContent(val1, token);
            var actor2Content = await _movieClient.GetActorContent(val2, token);
            
            if (actor1Content is null || !actor1Content.CastMovies.Any() 
             || actor2Content is null || !actor2Content.CastMovies.Any())
                return new MatchActorsResponse { Movies = Enumerable.Empty<string>() };

            // фильтр MoviesOnly
            //if (request.MoviesOnly == true)
            //{
            //    movs1 = movs1.Where(m => m.Role == "Actress" || m.Role == "Actor").ToArray();
            //    movs1 = movs1.Where(m => m.Role == "Actress" || m.Role == "Actor").ToArray();
            //}

            foreach (var movies1 in actor1Content.CastMovies)
            {
                foreach (var movies2 in actor2Content.CastMovies)
                {
                    if (movies1.Id == movies2.Id)
                    {
                        result.Add(movies1.Title);
                    }
                }
            }
        }

        return new MatchActorsResponse { Movies = result };
    }
}
