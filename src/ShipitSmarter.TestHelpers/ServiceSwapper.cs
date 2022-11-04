using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ShipitSmarter.TestHelpers;

public static class ServiceSwapper
{
    public static void Swap<TService>(this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory)
    {
        if (implementationFactory == null)
        {
            throw new ArgumentNullException(nameof(implementationFactory));
        }

        var serviceDescriptor = services.FirstOrDefault(x => x.ServiceType == typeof(TService));
        if (serviceDescriptor == null)
        {
            throw new ArgumentException($"Service with type {typeof(TService)} not registered");
        }

        var lifetime = serviceDescriptor.Lifetime;
        services.Remove(serviceDescriptor);
        switch (lifetime)
        {
            case ServiceLifetime.Transient:
                services.AddTransient(typeof(TService), sp => implementationFactory(sp)!);
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped(typeof(TService), sp => implementationFactory(sp)!);
                break;
            case ServiceLifetime.Singleton:
            default:
                services.AddSingleton(typeof(TService), sp => implementationFactory(sp)!);
                break;
        }
    }
}