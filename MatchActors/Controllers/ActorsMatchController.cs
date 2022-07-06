using MatchActors.Application.Models;
using MatchActors.Contracts;
using MatchActors.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MatchActors.Controllers;

[Route("[controller]")]
public class ActorsMatchController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActorsMatchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("movies")]
    public async Task<ActionResult<MatchActorsResponse>> Post([FromBody]MatchActorsRequest request, CancellationToken token)
    {
        try
        {
            var movies = await _mediator.Send(new MatchActorsCommand
            {
                Actor1 = request.Actor1,
                Actor2 = request.Actor2,
                MoviesOnly = request.MoviesOnly
            }, token);
            
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