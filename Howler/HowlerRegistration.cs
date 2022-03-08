using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Howler;

public class HowlerRegistration
{
    internal static Dictionary<Guid, Func<Delegate, object?>> Structures = new();
    internal static bool ServicesRegistered;

    /// <summary>
    /// Manually add structures to the dictionary
    /// </summary>
    /// <param name="structures"></param>
    public void AddStructures(params (Guid, Func<Delegate, object?>)[] structures)
    {
        foreach (var (id, func) in structures)
        {
            Structures.TryAdd(id, func);
        }
    }

    /// <summary>
    /// Manually add structure to the dictionary
    /// </summary>
    /// <param name="func"></param>
    /// <param name="id"></param>
    public void AddStructure(Guid id, Func<Delegate, object?> func) => Structures.TryAdd(id, func);

    public void RemoveStructure(Guid id) => Structures.Remove(id);

    public void UpdateStructure(Guid id, Func<Delegate, object?> func) => Structures[id] = func;
}

public static class HowlerRegistrationExtensions
{
    /// <summary>
    /// Inject the howler and optional structures to wrap around your method.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterHowler(this IServiceCollection services)
    {
        services.AddTransient<IHowler, Howler>();
        return services;
    }

    public static IApplicationBuilder RegisterHowlerMiddleware(this IApplicationBuilder app)
    {
        if (!HowlerRegistration.ServicesRegistered)
        {
            var registrations = Assembly.GetExecutingAssembly()
                .GetTypes().Where(type => typeof(IHowlerStructureBuilder).IsAssignableFrom(type) && !type.IsInterface);

            var serviceProvider = app.ApplicationServices;

            foreach (var registration in registrations)
            {
                var service = serviceProvider.GetService(registration) as IHowlerStructureBuilder;
                service?.InvokeRegistrations();
            }

            HowlerRegistration.ServicesRegistered = true;
        }

        return app;
    }
}

public interface IHowlerStructureBuilder
{
    void InvokeRegistrations();
}