using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Contracts;

namespace Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Requests;

internal static class ApproveAgreementRequestConverter
{
    internal static ApproveAgreementCommand ToInternal(ApproveAgreementRequest request)
    {
        return new ApproveAgreementCommand(request.AgreementId);
    }
}