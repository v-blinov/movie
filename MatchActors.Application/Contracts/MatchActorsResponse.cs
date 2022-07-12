namespace MatchActors.Application.Contracts;

public record MatchActorsResponse
{
    /// <summary>
    /// Список названий единиц контента
    /// </summary>
    public IEnumerable<string> Movies { get; init; } = null!;
}
