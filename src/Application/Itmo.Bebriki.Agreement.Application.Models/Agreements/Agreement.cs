namespace Itmo.Bebriki.Agreement.Application.Models.Agreements;

public sealed record Agreement
{
    internal Agreement() { }

    public long Id { get; init; }

    public long JobTaskId { get; init; }

    public JobTaskState JobTaskState { get; init; }

    public long? AssigneeId { get; init; }

    public DateTimeOffset? Deadline { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}