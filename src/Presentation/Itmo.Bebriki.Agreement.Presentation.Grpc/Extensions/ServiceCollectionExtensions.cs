using Itmo.Bebriki.Agreement.Presentation.Grpc.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Agreement.Presentation.Grpc.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationGrpc(this IServiceCollection collection)
    {
        collection.AddGrpc(grpc =>
        {
            grpc.Interceptors.Add<ValidationInterceptor>();
            grpc.Interceptors.Add<ExceptionHandlingInterceptor>();
        });
        collection.AddGrpcReflection();

        return collection;
    }
}