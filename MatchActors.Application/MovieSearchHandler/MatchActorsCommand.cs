using MediatR;

namespace MatchActors.Application.MovieSearchHandler;

internal record MatchActorsCommand : IRequest<MatchActorsResult>
{
    public string Actor1 { get; init; } = null!;
    public string Actor2 { get; init; } = null!;
    public bool MoviesOnly { get; init; }
}
