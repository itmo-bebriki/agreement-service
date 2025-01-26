using Itmo.Bebriki.Agreement.Application.Agreements;
using Itmo.Bebriki.Agreement.Application.Contracts.Agreements;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Agreement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAgreementService, AgreementService>();

        return collection;
    }
}