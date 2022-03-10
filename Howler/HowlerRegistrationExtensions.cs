using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Howler;

public static class HowlerRegistrationExtensions
{
    private static bool _servicesRegistered;

    /// <summary>
    /// Registers all the <see cref="IHowlerStructureBuilder"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="executingAssembly"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterHowler(this IServiceCollection services, Assembly executingAssembly)
    {
        var registrations = executingAssembly.GetTypes()
            .Where(type => typeof(IHowlerStructureBuilder).IsAssignableFrom(type) && !type.IsInterface).ToList();

        foreach (var service in registrations)
        {
            services.AddTransient(typeof(IHowlerStructureBuilder), service);
        }

        services.AddTransient<IHowler, Howler>();
        return services;
    }

    public static IApplicationBuilder RegisterHowlerMiddleware(this IApplicationBuilder app)
    {
        if (!_servicesRegistered)
        {
            var scope = app.ApplicationServices.CreateScope(); 
            var services = scope.ServiceProvider.GetServices<IHowlerStructureBuilder>();
            foreach (var service in services)
            {
                service.InvokeRegistrations();
            }

            _servicesRegistered = true;
        }

        return app;
    }
}