namespace MatchActors.Infrastructure.Storage;

internal interface IActorRepository
{
    Task<string?> GetActorId(string actor, CancellationToken token);
}
