using MatchActors.Domain.Exceptions;
using MatchActors.Services.Dtos;
using MatchActors.Services.InfrastructureContracts;

namespace MatchActors.Services.Services;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;
    private readonly IMovieClient _movieClient;

    public ActorService(IActorRepository actorRepository, IMovieClient movieClient)
    {
        _actorRepository = actorRepository;
        _movieClient = movieClient;
    }
    
    public async Task<ActorContentDto> GetActorContent(string actor, CancellationToken token)
    {
        var actorId = await GetActorId(actor, token);
        var actorContent = await _movieClient.GetActorContent(actorId, token);

        if(actorContent is null || actorContent.CastMovies is null || !actorContent.CastMovies.Any())
            return new ActorContentDto { CastMovies = Enumerable.Empty<ActorsContentItemDto>() };
        
        return new ActorContentDto
        {
            CastMovies = actorContent.CastMovies.Select(p => new ActorsContentItemDto
            {
                Id = p.Id,
                Role = p.Role,
                Title = p.Title
            }).ToArray()
        };
    }
    
    private async Task<string> GetActorId(string actor, CancellationToken token)
    {
        var actorId = await _actorRepository.GetActorId(actor, token);

        if(string.IsNullOrEmpty(actorId))
        {
            var actors = await _movieClient.GetActorId(actor, token);
            actorId = actors?.Results?.FirstOrDefault(p => string.Compare(p.Title, actor, StringComparison.InvariantCultureIgnoreCase) == 0)?.Id;
        }

        if(string.IsNullOrEmpty(actorId))
            throw new ActorNotFoundException($"Actor '{actor}' was not found");

        //TODO: Положить значение в базу

        return actorId;
    }
}
