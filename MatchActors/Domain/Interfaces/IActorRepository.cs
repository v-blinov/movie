namespace MatchActors.Domain.Interfaces;

internal interface IActorRepository
{
    Task<string?> GetActorId(string actor, CancellationToken token);
}
