using Itmo.Bebriki.Agreement.Application.Contracts.Agreements;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Commands;
using Itmo.Bebriki.Agreement.Presentation.Kafka.Converters;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Dev.Platform.Kafka.Consumer;
using Microsoft.Extensions.Logging;

namespace Itmo.Bebriki.Agreement.Presentation.Kafka.ConsumerHandlers;

internal sealed class JobTaskSubmissionConsumerHandler
    : IKafkaConsumerHandler<JobTaskSubmissionKey, JobTaskSubmissionValue>
{
    private readonly IAgreementService _agreementService;
    private readonly ILogger<JobTaskSubmissionConsumerHandler> _logger;

    public JobTaskSubmissionConsumerHandler(
        IAgreementService agreementService,
        ILogger<JobTaskSubmissionConsumerHandler> logger)
    {
        _agreementService = agreementService;
        _logger = logger;
    }

    public async ValueTask HandleAsync(
        IEnumerable<IKafkaConsumerMessage<JobTaskSubmissionKey, JobTaskSubmissionValue>> messages,
        CancellationToken cancellationToken)
    {
        foreach (IKafkaConsumerMessage<JobTaskSubmissionKey, JobTaskSubmissionValue> message in messages)
        {
            AddAgreementCommand internalCommand = JobTaskSubmissionConverter.ToInternal(message.Key, message.Value);

            try
            {
                await _agreementService.AddAgreementAsync(internalCommand, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to process Kafka message. Topic: {Topic}, Key: {Key}, Value: {Value}",
                    message.Topic,
                    message.Key,
                    message.Value);
            }
        }
    }
}