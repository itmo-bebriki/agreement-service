using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;

namespace Itmo.Bebriki.Agreement.Application.Contracts.Agreements;

public interface IAgreementService
{
    Task<long> AddAgreementAsync(AddAgreementCommand command, CancellationToken cancellationToken);

    Task<PagedAgreementDto> QueryAgreementsAsync(QueryAgreementCommand command, CancellationToken cancellationToken);

    Task ApproveAgreementAsync(ApproveAgreementCommand command, CancellationToken cancellationToken);

    Task RejectAgreementAsync(RejectAgreementCommand command, CancellationToken cancellationToken);
}