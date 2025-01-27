using Itmo.Bebriki.Agreement.Application.Models.Agreements;
using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Events;

public sealed record UpdateDecisionEvent(
    long JobTaskId,
    JobTaskState State,
    long? AssigneeId,
    DateTimeOffset? Deadline) : IEvent;