using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Contracts;
using Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Dtos.Enums;

namespace Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Requests;

internal static class QueryAgreementRequestConverter
{
    public static QueryAgreementCommand ToInternal(QueryAgreementRequest request)
    {
        return new QueryAgreementCommand(
            request.AgreementIds.ToArray(),
            request.JobTaskIds.ToArray(),
            request.States.Select(JobTaskStateConverter.ToInternal).ToArray(),
            request.AssigneeIds.ToArray(),
            request.FromDeadline?.ToDateTime(),
            request.ToDeadline?.ToDateTime(),
            request.FromCreatedAt?.ToDateTime(),
            request.ToCreatedAt?.ToDateTime(),
            request.Cursor,
            request.PageSize);
    }
}