using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Repositories;
using Itmo.Bebriki.Agreement.Application.Models.Agreements;
using Itmo.Dev.Platform.Persistence.Abstractions.Commands;
using Itmo.Dev.Platform.Persistence.Abstractions.Connections;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Itmo.Bebriki.Agreement.Infrastructure.Persistence.Repositories;

internal sealed class AgreementRepository : IAgreementRepository
{
    private readonly IPersistenceConnectionProvider _connectionProvider;

    public AgreementRepository(IPersistenceConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async IAsyncEnumerable<JobAgreement> QueryAsync(
        AgreementQuery query,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql =
        """
        select
            a.agreement_id,
            a.job_task_id,
            a.state,
            a.assignee_id,
            a.dead_line,
            a.created_at
        from agreements as a 
        where (:cursor is null or a.agreement_id > :cursor)
            and (cardinality(:agreement_ids) = 0 or a.agreement_id = any(:agreement_ids))
            and (cardinality(:states) = 0 or a.state = any(:states))
            and (cardinality(:assignee_ids) = 0 or a.assignee_id = any(:assignee_ids))
            and (:from_deadline is null or a.dead_line >= :from_deadline)
            and (:to_deadline is null or a.dead_line <= :to_deadline)
            and (:from_created_at is null or a.created_at >= :from_created_at)
            and (:to_created_at is null or a.created_at <= :to_created_at)
        order by a.agreement_id
        limit :page_size;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("agreement_ids", query.AgreementIds)
            .AddParameter("states", query.JobTaskStates)
            .AddParameter("assignee_ids", query.AssigneeIds)
            .AddParameter("from_deadline", query.FromDeadline)
            .AddParameter("to_deadline", query.ToDeadline)
            .AddParameter("from_created_at", query.FromCreatedAt)
            .AddParameter("to_created_at", query.ToCreatedAt)
            .AddParameter("cursor", query.Cursor)
            .AddParameter("page_size", query.PageSize);

        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return JobAgreementFactory.CreateNew(
                id: reader.GetInt64("agreement_id"),
                jobTaskId: reader.GetInt64("job_task_id"),
                state: reader.GetFieldValue<JobTaskState>("state"),
                assigneeId: reader.GetInt64("assignee_id"),
                deadline: reader.GetFieldValue<DateTimeOffset>("dead_line"),
                createdAt: reader.GetFieldValue<DateTimeOffset>("created_at"));
        }
    }

    public async IAsyncEnumerable<long> AddAsync(
        IReadOnlyCollection<JobAgreement> agreements,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql =
        """
        insert into agreements as a 
            (job_task_id, state, assignee_id, dead_line, created_at)
        select
            source.job_task_id,
            source.state,
            source.assignee_id,
            source.dead_line,
            source.created_at
        from unnest (
            :job_task_ids,
            :states,
            :assignee_ids,
            :dead_lines,
            :created_ats
        ) as source (
            job_task_id,
            state,
            assignee_id,
            dead_line,
            created_at
        )
        returning a.agreement_id;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("job_task_ids", agreements.Select(a => a.JobTaskId))
            .AddParameter("states", agreements.Select(a => a.JobTaskState))
            .AddParameter("assignee_ids", agreements.Select(a => a.AssigneeId))
            .AddParameter("dead_lines", agreements.Select(a => a.Deadline))
            .AddParameter("created_ats", agreements.Select(a => a.CreatedAt));

        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return reader.GetInt64(0);
        }
    }

    public async Task UpdateAsync(IReadOnlyCollection<JobAgreement> agreements, CancellationToken cancellationToken)
    {
        const string sql =
        """
        update agreements as a
        set 
            job_task_id = source.job_task_id,
            state = source.state,
            assignee_id = source.assignee_id,
            dead_line = source.dead_line,
            created_at = source.created_at
        from unnest(
            :agreement_ids,
            :job_task_ids,
            :states,
            :assignee_ids,
            :dead_lines,
            :created_ats
        ) as source (
            agreement_id,
            job_task_id,
            state,
            assignee_id,
            dead_line,
            created_at
        )
        where a.agreement_id = source.agreement_id;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("agreement_ids", agreements.Select(a => a.Id))
            .AddParameter("job_task_ids", agreements.Select(a => a.JobTaskId))
            .AddParameter("states", agreements.Select(a => a.JobTaskState))
            .AddParameter("assignee_ids", agreements.Select(a => a.AssigneeId))
            .AddParameter("dead_lines", agreements.Select(a => a.Deadline))
            .AddParameter("created_ats", agreements.Select(a => a.CreatedAt));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}