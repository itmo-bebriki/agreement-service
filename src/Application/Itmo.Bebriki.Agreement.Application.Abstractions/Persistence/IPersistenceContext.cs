using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Repositories;

namespace Itmo.Bebriki.Agreement.Application.Abstractions.Persistence;

public interface IPersistenceContext
{
    IAgreementRepository AgreementRepository { get; }
}