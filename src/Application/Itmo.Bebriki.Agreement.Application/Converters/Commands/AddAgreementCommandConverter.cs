using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Application.Models.Agreements.Contexts;

namespace Itmo.Bebriki.Agreement.Application.Converters.Commands;

internal static class AddAgreementCommandConverter
{
    internal static CreateAgreementContext ToContext(AddAgreementCommand command, DateTimeOffset createdAt)
    {
        return new CreateAgreementContext(
            JobTaskId: command.JobTaskId,
            JobTaskState: command.JobTaskState,
            AssigneeId: command.AssigneeId,
            Deadline: command.Deadline,
            CreatedAt: createdAt);
    }
}