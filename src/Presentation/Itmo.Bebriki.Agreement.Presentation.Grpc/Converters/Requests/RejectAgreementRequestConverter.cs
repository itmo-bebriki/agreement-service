using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Contracts;

namespace Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Requests;

internal static class RejectAgreementRequestConverter
{
    internal static RejectAgreementCommand ToInternal(RejectAgreementRequest request)
    {
        return new RejectAgreementCommand(request.AgreementId);
    }
}