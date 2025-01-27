using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence;
using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Repositories;

namespace Itmo.Bebriki.Agreement.Infrastructure.Persistence;

public class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(IAgreementRepository agreementRepository)
    {
        AgreementRepository = agreementRepository;
    }

    public IAgreementRepository AgreementRepository { get; }
}