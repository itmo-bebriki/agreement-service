using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;

namespace Itmo.Bebriki.Agreement.Application.Converters.Dtos;

internal static class AgreementDtoConverter
{
    internal static AgreementDto ToDto(Models.Agreements.JobAgreement jobJobAgreement)
    {
        return new AgreementDto(
            Id: jobJobAgreement.Id,
            JobTaskId: jobJobAgreement.JobTaskId,
            JobTaskState: jobJobAgreement.JobTaskState,
            AssigneeId: jobJobAgreement.AssigneeId,
            Deadline: jobJobAgreement.Deadline,
            CreatedAt: jobJobAgreement.CreatedAt);
    }
}