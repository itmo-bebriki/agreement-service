namespace Itmo.Bebriki.Agreement.Application.Contracts.Exceptions;

public sealed class AgreementNotFoundException : Exception
{
    public AgreementNotFoundException() { }

    public AgreementNotFoundException(string message) : base(message) { }

    public AgreementNotFoundException(string message, Exception inner) : base(message, inner) { }
}