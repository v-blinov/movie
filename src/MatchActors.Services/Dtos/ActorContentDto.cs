using MatchActors.Domain.Models;

namespace MatchActors.Services.Dtos;

public record ActorContentDto
{
    public IEnumerable<ActorsContentItemDto>? CastMovies { get; init; } = Enumerable.Empty<ActorsContentItemDto>();

    public ActorContentDto()
    { }
    
    public ActorContentDto(ActorContent actorContent)
    {
        CastMovies = actorContent.CastMovies?.Select(p => new ActorsContentItemDto(p)).ToArray();
    }
    
    public ActorContent ConvertToActorsContent()
    {
        return new ActorContent
        {
            CastMovies = CastMovies?.Select(p => p.ConvertToActorsContentItem()).ToArray()
        };
    }
}