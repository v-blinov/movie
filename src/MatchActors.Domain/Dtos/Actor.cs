namespace MatchActors.Domain.Dtos;

public record Actor
{
    public string Id { get; init; } = null!;
    public string Title { get; init; } = null!;
}