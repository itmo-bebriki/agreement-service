using Itmo.Bebriki.Agreement.Application.Models.Agreements;

namespace Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;

public sealed record QueryAgreementCommand(
    long[] AgreementIds,
    long[] JobTaskIds,
    JobTaskState[] JobTaskStates,
    long[] AssigneeIds,
    DateTimeOffset? FromDeadline,
    DateTimeOffset? ToDeadline,
    DateTimeOffset? FromCreatedAt,
    DateTimeOffset? ToCreatedAt,
    long? Cursor,
    int PageSize);