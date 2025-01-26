using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Queries;

namespace Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Repositories;

public interface IAgreementRepository
{
    IAsyncEnumerable<Models.Agreements.Agreement> QueryAsync(
        AgreementQuery query,
        CancellationToken cancellationToken);

    IAsyncEnumerable<long> AddAsync(
        IReadOnlyCollection<Models.Agreements.Agreement> agreements,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        IReadOnlyCollection<Models.Agreements.Agreement> agreements,
        CancellationToken cancellationToken);
}