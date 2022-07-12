namespace MatchActors.Infrastructure.Storage;

public record DbConnectionOptions
{
    public string Connection { get; init; } = string.Empty;
}
