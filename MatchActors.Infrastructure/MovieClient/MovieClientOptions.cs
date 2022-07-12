namespace MatchActors.Infrastructure.MovieClient;

public record MovieClientOptions
{
    public string BaseUrl { get; init; } = string.Empty;
    public string AppId { get; init; } = string.Empty;
}
