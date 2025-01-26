using Grpc.Core;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;
using Itmo.Bebriki.Agreement.Contracts;
using Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Requests;
using Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Responses;

namespace Itmo.Bebriki.Agreement.Presentation.Grpc.Controllers;

public sealed class AgreementController : AgreementService.AgreementServiceBase
{
    private readonly IAgreementService _agreementService;

    public AgreementController(IAgreementService agreementService)
    {
        _agreementService = agreementService;
    }

    public override async Task<QueryAgreementResponse> QueryAgreements(
        QueryAgreementRequest request,
        ServerCallContext context)
    {
        QueryAgreementCommand internalCommand = QueryAgreementRequestConverter.ToInternal(request);

        PagedAgreementDto internalResponse =
            await _agreementService.QueryAgreementsAsync(internalCommand, context.CancellationToken);

        QueryAgreementResponse response = QueryAgreementResponseConverter.FromInternal(internalResponse);

        return response;
    }

    public override async Task<ApproveAgreementResponse> ApproveAgreement(
        ApproveAgreementRequest request,
        ServerCallContext context)
    {
        ApproveAgreementCommand internalCommand = ApproveAgreementRequestConverter.ToInternal(request);

        await _agreementService.ApproveAgreementAsync(internalCommand, context.CancellationToken);

        return new ApproveAgreementResponse();
    }

    public override async Task<RejectAgreementResponse> RejectAgreement(
        RejectAgreementRequest request,
        ServerCallContext context)
    {
        RejectAgreementCommand internalCommand = RejectAgreementRequestConverter.ToInternal(request);

        await _agreementService.RejectAgreementAsync(internalCommand, context.CancellationToken);

        return new RejectAgreementResponse();
    }
}