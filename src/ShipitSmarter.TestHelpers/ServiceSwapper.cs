using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ShipitSmarter.TestHelpers;

/// <summary>
/// A static class that provides a way to swap services during integration testing along with a WebApplicationFactory
/// </summary>
public static class ServiceSwapper
{
    /// <summary>
    /// Swaps the service registered in Program.cs with a custom implementation
    /// E.G. services => services.Swap(_ => mock.Object) 
    /// </summary>
    /// <param name="services">Service Collection that would be provided by a WebApplicationFactory</param>
    /// <param name="implementationFactory">factory used to provide new service</param>
    /// <typeparam name="TService">Type of service to be swapped</typeparam>
    /// <exception cref="ArgumentNullException">Thrown if we can't find a matching injected service</exception>
    /// <exception cref="ArgumentException">Thrown if no implementation factory is provided</exception>
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