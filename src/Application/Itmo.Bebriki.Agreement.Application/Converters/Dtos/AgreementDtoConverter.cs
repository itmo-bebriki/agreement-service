using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;

namespace Itmo.Bebriki.Agreement.Application.Converters.Dtos;

internal static class AgreementDtoConverter
{
    internal static AgreementDto ToDto(Models.Agreements.Agreement agreement)
    {
        return new AgreementDto(
            Id: agreement.Id,
            JobTaskId: agreement.JobTaskId,
            JobTaskState: agreement.JobTaskState,
            AssigneeId: agreement.AssigneeId,
            Deadline: agreement.Deadline,
            CreatedAt: agreement.CreatedAt);
    }
}