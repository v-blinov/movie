namespace MatchActors.Domain.InfrastructureContracts;

public interface IActorRepository
{
    Task<string?> GetActorId(string actorName, CancellationToken token);
}
