using Itmo.Bebriki.Agreement.Presentation.Kafka.ConsumerHandlers;
using Itmo.Bebriki.Tasks.Kafka.Contracts;
using Itmo.Dev.Platform.Kafka.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Agreement.Presentation.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationKafka(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        const string configurationSection = "Presentation:Kafka";
        const string consumerKey = "Presentation:Kafka:Consumers";
        const string producerKey = "Presentation:Kafka:Producers";

        collection.AddPlatformKafka(builder => builder
            .ConfigureOptions(configuration.GetSection(configurationSection))
            .AddConsumer(consumer => consumer
                .WithKey<JobTaskSubmissionKey>()
                .WithValue<JobTaskSubmissionValue>()
                .WithConfiguration(configuration.GetSection($"{consumerKey}:JobTaskSubmission"))
                .DeserializeKeyWithProto()
                .DeserializeValueWithProto()
                .HandleWith<JobTaskSubmissionConsumerHandler>())
            .AddProducer(producer => producer
                .WithKey<JobTaskDecisionKey>()
                .WithValue<JobTaskDecisionValue>()
                .WithConfiguration(configuration.GetSection($"{producerKey}:JobTaskDecision"))
                .SerializeKeyWithProto()
                .SerializeValueWithProto()));

        return collection;
    }
}