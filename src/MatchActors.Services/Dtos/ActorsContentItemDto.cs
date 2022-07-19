using MatchActors.Domain.Models;

namespace MatchActors.Services.Dtos;

public record ActorsContentItemDto
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string Role { get; init; }

    public ActorsContentItemDto()
    { }
    
    public ActorsContentItemDto(ActorsContentItem actorsContentItem)
    {
        Id = actorsContentItem.Id;
        Role = actorsContentItem.Role;
        Title = actorsContentItem.Title;
    }

    public ActorsContentItem ConvertToActorsContentItem()
    {
        return new ActorsContentItem
        {
            Id = Id,
            Role = Role,
            Title = Title
        };
    }
}