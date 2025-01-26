using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;
using Itmo.Bebriki.Agreement.Contracts;
using Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Dtos;

namespace Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Responses;

internal static class QueryAgreementResponseConverter
{
    internal static QueryAgreementResponse FromInternal(PagedAgreementDto dtos)
    {
        return new QueryAgreementResponse
        {
            Cursor = dtos.Cursor,
            Agreements = { dtos.AgreementDtos.Select(AgreementDtoConverter.FromInternal) },
        };
    }
}