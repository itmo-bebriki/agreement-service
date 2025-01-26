using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence;
using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Queries;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Events;
using Itmo.Bebriki.Agreement.Application.Contracts.Exceptions;
using Itmo.Bebriki.Agreement.Application.Converters.Commands;
using Itmo.Bebriki.Agreement.Application.Converters.Dtos;
using Itmo.Bebriki.Agreement.Application.Models.Agreements;
using Itmo.Bebriki.Agreement.Application.Models.Agreements.Contexts;
using Itmo.Dev.Platform.Common.DateTime;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Persistence.Abstractions.Transactions;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Itmo.Bebriki.Agreement.Application.Agreements;

internal sealed class AgreementService : IAgreementService
{
    private readonly IPersistenceContext _persistenceContext;
    private readonly IPersistenceTransactionProvider _transactionProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<AgreementService> _logger;

    public AgreementService(
        IPersistenceContext persistenceContext,
        IPersistenceTransactionProvider transactionProvider,
        IDateTimeProvider dateTimeProvider,
        IEventPublisher eventPublisher,
        ILogger<AgreementService> logger)
    {
        _persistenceContext = persistenceContext;
        _transactionProvider = transactionProvider;
        _dateTimeProvider = dateTimeProvider;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task<long> AddAgreementAsync(
        AddAgreementCommand command,
        CancellationToken cancellationToken)
    {
        CreateAgreementContext context = AddAgreementCommandConverter.ToContext(command, _dateTimeProvider.Current);
        Models.Agreements.Agreement agreement = AgreementFactory.CreateFromCreateContext(context);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            long agreementId = await _persistenceContext.AgreementRepository
                .AddAsync([agreement], cancellationToken)
                .FirstAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return agreementId;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to create agreement. JobTaskId: {JobTaskId}, JobTaskState: {JobTaskState}, AssigneeId: {AssigneeId}, Deadline: {Deadline}",
                agreement.JobTaskId,
                agreement.JobTaskState,
                agreement.AssigneeId,
                agreement.Deadline);

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<PagedAgreementDto> QueryAgreementsAsync(
        QueryAgreementCommand command,
        CancellationToken cancellationToken)
    {
        AgreementQuery agreementQuery = QueryAgreementCommandConverter.ToQuery(command);

        HashSet<AgreementDto> agreements = await _persistenceContext.AgreementRepository
            .QueryAsync(agreementQuery, cancellationToken)
            .Select(AgreementDtoConverter.ToDto)
            .ToHashSetAsync(cancellationToken);

        long? cursor = agreements.Count == command.PageSize && agreements.Count > 0
            ? agreements.Last().Id
            : null;

        return new PagedAgreementDto(cursor, agreements);
    }

    public async Task ApproveAgreementAsync(
        ApproveAgreementCommand command,
        CancellationToken cancellationToken)
    {
        Models.Agreements.Agreement agreement =
            await CheckForExistingAgreementAsync(command.AgreementId, cancellationToken);

        agreement = agreement with { JobTaskState = JobTaskState.Approved };

        UpdateDecisionEvent updateDecisionEvent = UpdateDecisionEventConverter.ToEvent(command, agreement);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.AgreementRepository.UpdateAsync([agreement], cancellationToken);

            await _eventPublisher.PublishAsync(updateDecisionEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to approve agreement with id: {}",
                agreement.Id);

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RejectAgreementAsync(
        RejectAgreementCommand command,
        CancellationToken cancellationToken)
    {
        Models.Agreements.Agreement agreement =
            await CheckForExistingAgreementAsync(command.AgreementId, cancellationToken);

        agreement = agreement with { JobTaskState = JobTaskState.Rejected };

        UpdateDecisionEvent updateDecisionEvent = UpdateDecisionEventConverter.ToEvent(command, agreement);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.AgreementRepository.UpdateAsync([agreement], cancellationToken);

            await _eventPublisher.PublishAsync(updateDecisionEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to approve agreement with id: {}",
                agreement.Id);

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private async Task<Models.Agreements.Agreement> CheckForExistingAgreementAsync(
        long agreementId,
        CancellationToken cancellationToken)
    {
        var agreementQuery = AgreementQuery.Build(builder => builder
            .WithAgreementId(agreementId)
            .WithPageSize(1));

        Models.Agreements.Agreement? agreement = await _persistenceContext.AgreementRepository
            .QueryAsync(agreementQuery, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (agreement is null)
        {
            _logger.LogWarning("Agreement with id: {Id} not found", agreementId);
            throw new AgreementNotFoundException($"Agreement with id: {agreementId} not found");
        }

        return agreement;
    }
}