using MatchActors.Application.Contracts;
using MatchActors.Application.MovieSearchHandler;
using MatchActors.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MatchActors.Application.Controllers;

[Route("[controller]")]
public class ActorsMatchController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActorsMatchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получить список единиц контента, где участуют оба актера
    /// </summary>
    /// <param name="request">Параметры запроса</param>
    /// <param name="token">CancellationToken</param>
    /// <returns>Список названий единиц контента</returns>
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