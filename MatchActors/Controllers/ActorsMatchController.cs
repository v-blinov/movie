using MatchActors.Contracts;
using MatchActors.Exceptions;
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
        try
        {
            var movies = await _movieSearchService.MovieSearch(request, token);
            return Ok(movies);
        }
        catch(ActorNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch(Exception ex)
        {
            var errorMessage = "Ошибка на стороне сервера, попробуйте позже или обратитесь к администратору";
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }
    }
}