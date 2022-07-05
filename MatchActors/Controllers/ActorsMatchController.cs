using MatchActors.Contracts;
using MatchActors.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatchActors.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorsMatchController : ControllerBase
{
    [HttpPost("movies")]
    public async Task<ActionResult<MatchActorsResponse>> Post(MatchActorsRequest request, CancellationToken token)
    {
        return await new MovieSearchService().MovieSearch(request, token);
    }
}