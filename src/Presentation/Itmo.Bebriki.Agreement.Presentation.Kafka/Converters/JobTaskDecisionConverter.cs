using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Events;
using Itmo.Bebriki.Agreement.Presentation.Kafka.Converters.Enums;
using Itmo.Bebriki.Tasks.Kafka.Contracts;

namespace Itmo.Bebriki.Agreement.Presentation.Kafka.Converters;

internal static class JobTaskDecisionConverter
{
    internal static JobTaskDecisionValue ToValue(UpdateDecisionEvent evt)
    {
        return new JobTaskDecisionValue
        {
            JobTaskId = evt.JobTaskId,
            State = JobTaskStateConverter.FromInternal(evt.State),
            ApprovedAssigneeId = evt.AssigneeId,
            ApprovedDeadline = evt.Deadline?.ToTimestamp(),
        };
    }
}