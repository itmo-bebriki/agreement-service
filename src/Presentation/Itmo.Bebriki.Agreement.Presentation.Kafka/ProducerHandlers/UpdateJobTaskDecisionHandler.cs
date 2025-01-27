using Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Events;
using Itmo.Bebriki.Agreement.Presentation.Kafka.Converters;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Itmo.Bebriki.Agreement.Presentation.Kafka.ProducerHandlers;

internal sealed class UpdateJobTaskDecisionHandler : IEventHandler<UpdateDecisionEvent>
{
    private readonly IKafkaMessageProducer<JobTaskDecisionKey, JobTaskDecisionValue> _producer;

    public UpdateJobTaskDecisionHandler(IKafkaMessageProducer<JobTaskDecisionKey, JobTaskDecisionValue> producer)
    {
        _producer = producer;
    }

    public async ValueTask HandleAsync(UpdateDecisionEvent evt, CancellationToken cancellationToken)
    {
        var key = new JobTaskDecisionKey { JobTaskId = evt.JobTaskId };
        JobTaskDecisionValue value = JobTaskDecisionConverter.ToValue(evt);

        var message = new KafkaProducerMessage<JobTaskDecisionKey, JobTaskDecisionValue>(key, value);
        await _producer.ProduceAsync(message, cancellationToken);
    }
}