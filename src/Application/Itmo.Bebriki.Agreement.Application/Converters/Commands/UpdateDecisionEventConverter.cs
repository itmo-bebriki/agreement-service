using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Events;
using Itmo.Bebriki.Agreement.Application.Models.Agreements;

namespace Itmo.Bebriki.Agreement.Application.Converters.Commands;

internal static class UpdateDecisionEventConverter
{
    internal static UpdateDecisionEvent ToEvent(RejectAgreementCommand command, Models.Agreements.Agreement agreement)
    {
        return new UpdateDecisionEvent(
            agreement.JobTaskId,
            JobTaskState.Rejected,
            null,
            null);
    }

    internal static UpdateDecisionEvent ToEvent(ApproveAgreementCommand command, Models.Agreements.Agreement agreement)
    {
        return new UpdateDecisionEvent(
            agreement.JobTaskId,
            JobTaskState.Approved,
            agreement.AssigneeId,
            agreement.Deadline);
    }
}