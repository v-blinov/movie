namespace MatchActors.Domain.Dtos;

public record ActorsContentItem
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string Role { get; init; }
}