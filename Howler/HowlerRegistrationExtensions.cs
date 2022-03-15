using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Utilities;

namespace Howler;

public static class HowlerRegistrationExtensions
{
    private static bool _servicesRegistered;

    /// <summary>
    /// Registers all the <see cref="HowlerStructureBuilder"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="executingAssembly"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterHowler(this IServiceCollection services, Assembly executingAssembly)
    {
        var assemblies = executingAssembly.GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(executingAssembly);

        var types = assemblies.SelectMany(x => x.GetTypes());

        var registrations = types.Where(type => typeof(IHowlerStructure).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract).ToList();

        services.AddTransient<IHowler, Howler>();
        
        foreach (var service in registrations)
        {
            services.AddTransient(typeof(IHowlerStructure), service);
        }

        return services;
    }

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