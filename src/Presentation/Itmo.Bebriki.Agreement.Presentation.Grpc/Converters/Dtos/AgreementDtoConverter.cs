using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;
using Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Dtos.Enums;

namespace Itmo.Bebriki.Agreement.Presentation.Grpc.Converters.Dtos;

internal static class AgreementDtoConverter
{
    internal static Contracts.AgreementDto FromInternal(AgreementDto dto)
    {
        return new Contracts.AgreementDto
        {
            AgreementId = dto.Id,
            JobTaskId = dto.JobTaskId,
            State = JobTaskStateConverter.FromInternal(dto.JobTaskState),
            AssigneeId = dto.AssigneeId,
            Deadline = dto.Deadline?.ToTimestamp(),
            CreatedAt = dto.CreatedAt.ToTimestamp(),
        };
    }
}