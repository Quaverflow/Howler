using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Howler;

public static class HowlerRegistrationExtensions
{
    private static bool _servicesRegistered;

    /// <summary>
    /// Registers all the <see cref="IHowlerStructure"/> and the <see cref="IHowler"/>.
    /// All loaded assemblies from the provided assembly will be scanned for registration.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="executingAssembly"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterHowler(this IServiceCollection services, Assembly executingAssembly)
    {
        var assemblies = executingAssembly.GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(executingAssembly);
        RegisterHowler(services, assemblies.ToArray());

        return services;
    }

    /// <summary>
    /// Registers all the <see cref="IHowlerStructure"/> and the <see cref="IHowler"/> for the provided assemblies.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterHowler(this IServiceCollection services, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(x => x.GetTypes().Where(type =>
            (typeof(IHowlerStructure).IsAssignableFrom(type) || typeof(IHowlerWhisper).IsAssignableFrom(type)) 
            && !type.IsInterface 
            && !type.IsAbstract)
            .ToList());

        foreach (var service in types)
        {
            if (typeof(IHowlerStructure).IsAssignableFrom(service) && typeof(IHowlerWhisper).IsAssignableFrom(service))
            {
                services.AddTransient(typeof(IHowlerStructure), service);
                services.AddTransient(typeof(IHowlerWhisper), service);

            }
            else
            {
                var howlerInterface = typeof(IHowlerStructure).IsAssignableFrom(service)
                    ? typeof(IHowlerStructure)
                    : typeof(IHowlerWhisper);

                services.AddTransient(howlerInterface, service);
            }
        }
        services.AddTransient<IHowler, Howler>();
   

        return services;
    }

    /// <summary>
    /// Registers all the structures in the <see cref="HowlerRegistry"/> and makes them available throughout the application.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder RegisterHowlerMiddleware(this IApplicationBuilder app)
    {
        if (!_servicesRegistered)
        {
            var scope = app.ApplicationServices.CreateScope(); 
            var services = scope.ServiceProvider.GetServices<IHowlerStructure>();
            foreach (var service in services)
            {
                service.InvokeRegistrations();
            }

            _servicesRegistered = true;
        }

        return app;
    }
}