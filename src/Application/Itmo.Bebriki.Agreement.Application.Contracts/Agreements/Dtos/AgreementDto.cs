using Itmo.Bebriki.Agreement.Application.Models.Agreements;

namespace Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;

public sealed record AgreementDto(
    long Id,
    long JobTaskId,
    JobTaskState JobTaskState,
    long? AssigneeId,
    DateTimeOffset? Deadline,
    DateTimeOffset CreatedAt);