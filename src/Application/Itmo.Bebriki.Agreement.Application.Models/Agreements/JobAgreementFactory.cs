using Itmo.Bebriki.Agreement.Application.Models.Agreements.Contexts;

namespace Itmo.Bebriki.Agreement.Application.Models.Agreements;

public static class JobAgreementFactory
{
    public static JobAgreement CreateNew(
        long id,
        long jobTaskId,
        JobTaskState state,
        long? assigneeId,
        DateTimeOffset? deadline,
        DateTimeOffset createdAt)
    {
        return new JobAgreement
        {
            Id = id,
            JobTaskId = jobTaskId,
            JobTaskState = state,
            AssigneeId = assigneeId,
            Deadline = deadline,
            CreatedAt = createdAt,
        };
    }

    public static JobAgreement CreateFromCreateContext(CreateAgreementContext context)
    {
        return new JobAgreement
        {
            JobTaskId = context.JobTaskId,
            JobTaskState = context.JobTaskState,
            AssigneeId = context.AssigneeId,
            Deadline = context.Deadline,
            CreatedAt = context.CreatedAt,
        };
    }
}