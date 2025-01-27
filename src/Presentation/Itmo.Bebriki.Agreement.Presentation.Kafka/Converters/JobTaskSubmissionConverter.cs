using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using JobTaskState = Itmo.Bebriki.Agreement.Application.Models.Agreements.JobTaskState;

namespace Itmo.Bebriki.Agreement.Presentation.Kafka.Converters;

internal static class JobTaskSubmissionConverter
{
    internal static AddAgreementCommand ToInternal(JobTaskSubmissionKey key, JobTaskSubmissionValue value)
    {
        return new AddAgreementCommand(
            JobTaskId: key.JobTaskId,
            JobTaskState: JobTaskState.PendingApproval,
            AssigneeId: value.NewAssigneeId,
            Deadline: value.NewDeadline?.ToDateTimeOffset());
    }
}