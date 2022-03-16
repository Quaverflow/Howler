namespace Howler;

public sealed class HowlerRegistry 
{
    internal static readonly Dictionary<Guid, Delegate> Registrations = new();
    public static void AddStructure(Guid id, Delegate func) => Registrations.Add(id, func);
}
