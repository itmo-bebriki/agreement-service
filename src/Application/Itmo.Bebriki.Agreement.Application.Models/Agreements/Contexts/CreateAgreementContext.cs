namespace Itmo.Bebriki.Agreement.Application.Models.Agreements.Contexts;

public sealed record CreateAgreementContext(
    long JobTaskId,
    JobTaskState JobTaskState,
    long? AssigneeId,
    DateTimeOffset? Deadline,
    DateTimeOffset CreatedAt);