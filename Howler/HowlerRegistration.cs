using Microsoft.Extensions.DependencyInjection;

namespace Howler;

public static class HowlerRegistration
{
    internal static Dictionary<Guid, Func<Delegate, object?>> Structures = new();

    /// <summary>
    /// Manually add structures to the dictionary
    /// </summary>
    /// <param name="structures"></param>
    public static void AddStructures(params (Guid, Func<Delegate, object?>)[] structures)
    {
        foreach (var (id, func) in structures)
        {
            Structures.Add(id, func);
        }
    }

    /// <summary>
    /// Manually add structure to the dictionary
    /// </summary>
    /// <param name="structures"></param>
    public static void AddStructure(Guid id, Func<Delegate, object?> func)
    {
        Structures.Add(id, func);
    }

    /// <summary>
    /// Inject the howler and optional structures to wrap around your method.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="structures"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterHowler(this IServiceCollection services, params (Guid, Func<Delegate, object?>)[] structures)
    {
        services.AddTransient<IHowler, Howler>();
        AddStructures(structures);
        return services;
    }
}