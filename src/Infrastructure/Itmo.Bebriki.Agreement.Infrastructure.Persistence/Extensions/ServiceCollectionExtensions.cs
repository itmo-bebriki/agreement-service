using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence;
using Itmo.Bebriki.Agreement.Application.Abstractions.Persistence.Repositories;
using Itmo.Bebriki.Agreement.Infrastructure.Persistence.Plugins;
using Itmo.Bebriki.Agreement.Infrastructure.Persistence.Repositories;
using Itmo.Dev.Platform.Persistence.Abstractions.Extensions;
using Itmo.Dev.Platform.Persistence.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Agreement.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection collection)
    {
        collection.AddPlatformPersistence(persistence => persistence
            .UsePostgres(postgres => postgres
                .WithConnectionOptions(b => b.BindConfiguration("Infrastructure:Persistence:Postgres"))
                .WithMigrationsFrom(typeof(IAssemblyMarker).Assembly)
                .WithDataSourcePlugin<MappingPlugin>()));

        collection.AddScoped<IPersistenceContext, PersistenceContext>();

        collection.AddSingleton<IAgreementRepository, AgreementRepository>();

        return collection;
    }
}