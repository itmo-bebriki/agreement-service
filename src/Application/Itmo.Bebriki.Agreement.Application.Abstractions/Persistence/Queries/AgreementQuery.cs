using Itmo.Bebriki.Agreement.Application.Models.Agreements;
using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public sealed partial record AgreementQuery(
    long[] AgreementIds,
    long[] JobTaskIds,
    JobTaskState[] JobTaskStates,
    long[] AssigneeIds,
    DateTimeOffset? FromDeadline,
    DateTimeOffset? ToDeadline,
    DateTimeOffset? FromCreatedAt,
    DateTimeOffset? ToCreatedAt,
    long? Cursor,
    [RequiredValue] int PageSize);