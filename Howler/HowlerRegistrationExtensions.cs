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

        var registrations = types.Where(type => typeof(IHowlerStructureBuilder).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract).ToList();

        services.AddTransient<IHowler, Howler>();
        services.AddTransient<IHowlerRegistry, HowlerRegistry>();
        
        foreach (var service in registrations)
        {
            //services.AddTransient(typeof(HowlerStructureBuilder), service);
            var ctor = service.GetConstructors().OrderBy(x => x.GetParameters().Length).First();
            services.AddTransient(ctx =>
            {
                var dependencies = ctor.GetParameters().Select(x => ctx.GetRequiredService(x.ParameterType)).ToArray();
                var instance = Activator.CreateInstance(service, dependencies);
                var serviceInstance = instance as HowlerStructureBuilder;
                serviceInstance.ThrowIfNull();
                return serviceInstance;
            });
        }


        return services;
    }

    public static IApplicationBuilder RegisterHowlerMiddleware(this IApplicationBuilder app)
    {
        if (!_servicesRegistered)
        {
            var scope = app.ApplicationServices.CreateScope(); 
            var services = scope.ServiceProvider.GetServices<HowlerStructureBuilder>();
            foreach (var service in services)
            {
                service.InvokeRegistrations();
            }

            _servicesRegistered = true;
        }

        return app;
    }
}