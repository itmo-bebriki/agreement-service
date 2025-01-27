using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Agreement.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250127170800, "enum job task state")]
public sealed class EnumJobTaskStateMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create type job_task_state as enum (
            'none',
            'pending_approval',
            'approved',
            'rejected'
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop type job_task_state;
        """;
    }
}