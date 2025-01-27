using FluentMigrator;
using Itmo.Dev.Platform.Persistence.Postgres.Migrations;

namespace Itmo.Bebriki.Agreement.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(20250127170800, "table agreements")]
public sealed class TableAgreementsMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return
        """
        create table agreements (
            agreement_id bigint primary key generated always as identity,
            job_task_id bigint not null,
            state job_task_state not null,
            assignee_id bigint,
            dead_line timestamp with time zone,
            created_at timestamp with time zone not null
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return
        """
        drop table agreements;
        """;
    }
}