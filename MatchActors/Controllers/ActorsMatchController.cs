using MatchActors.Contracts;
using MatchActors.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatchActors.Controllers;

[Route("[controller]")]
public class ActorsMatchController : ControllerBase
{
    private readonly IMovieSearchService _movieSearchService;

    public ActorsMatchController(IMovieSearchService movieSearchService)
    {
        _movieSearchService = movieSearchService;
    }
    
    [HttpPost("movies")]
    public async Task<ActionResult<MatchActorsResponse>> Post([FromBody]MatchActorsRequest request, CancellationToken token)
    {
        return await _movieSearchService.MovieSearch(request, token);
    }
}