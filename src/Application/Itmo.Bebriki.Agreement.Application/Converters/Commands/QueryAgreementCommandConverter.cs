using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;

namespace Itmo.Bebriki.Agreement.Application.Converters.Commands;

internal static class QueryAgreementCommandConverter
{
    internal static AgreementQuery ToQuery(QueryAgreementCommand command)
    {
        return AgreementQuery.Build(builder => builder
            .WithAgreementIds(command.AgreementIds)
            .WithJobTaskIds(command.JobTaskIds)
            .WithJobTaskStates(command.JobTaskStates)
            .WithAssigneeIds(command.AssigneeIds)
            .WithFromDeadline(command.FromDeadline)
            .WithToDeadline(command.ToDeadline)
            .WithFromCreatedAt(command.FromCreatedAt)
            .WithToCreatedAt(command.ToCreatedAt)
            .WithCursor(command.Cursor)
            .WithPageSize(command.PageSize));
    }
}