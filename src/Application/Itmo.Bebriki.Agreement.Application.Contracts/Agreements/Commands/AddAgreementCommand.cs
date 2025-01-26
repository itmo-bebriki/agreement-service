using Itmo.Bebriki.Agreement.Application.Models.Agreements;

namespace Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;

public sealed record AddAgreementCommand(
    long JobTaskId,
    JobTaskState JobTaskState,
    long? AssigneeId,
    DateTimeOffset? Deadline);