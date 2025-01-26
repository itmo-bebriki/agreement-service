using Itmo.Bebriki.Agreement.Application.Models.Agreements.Contexts;

namespace Itmo.Bebriki.Agreement.Application.Models.Agreements;

public static class AgreementFactory
{
    public static Agreement CreateNew(
        long id,
        long jobTaskId,
        JobTaskState state,
        long? assigneeId,
        DateTimeOffset? deadline,
        DateTimeOffset createdAt)
    {
        return new Agreement
        {
            Id = id,
            JobTaskId = jobTaskId,
            JobTaskState = state,
            AssigneeId = assigneeId,
            Deadline = deadline,
            CreatedAt = createdAt,
        };
    }

    public static Agreement CreateFromCreateContext(CreateAgreementContext context)
    {
        return new Agreement
        {
            JobTaskId = context.JobTaskId,
            JobTaskState = context.JobTaskState,
            AssigneeId = context.AssigneeId,
            Deadline = context.Deadline,
            CreatedAt = context.CreatedAt,
        };
    }
}