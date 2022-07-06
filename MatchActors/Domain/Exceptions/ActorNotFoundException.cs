namespace MatchActors.Domain.Exceptions;

internal sealed class ActorNotFoundException : Exception
{
    public ActorNotFoundException() { }

    public ActorNotFoundException(string message) : base(message) { }

    public ActorNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}
