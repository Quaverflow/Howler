namespace Howler;

public class HowlerRegistration
{
    internal static readonly Dictionary<Guid, Delegate> Registrations = new();

    public static void AddStructure(Guid id, Func<Delegate, object?> func) => Registrations.Add(id, func);

    public static void AddStructure(Guid id, Action<Delegate> func) => Registrations.Add(id, func);

    public static void AddStructure<T>(Guid id, Func<Delegate, T, object?> func) => Registrations.Add(id, func);

    public static void AddVoidStructure<T>(Guid id, Action<Delegate, T> func) => Registrations.Add(id, func);

    public static void AddDataTransferStructure<T>(Guid id, Func<T, object?> func) => Registrations.Add(id, func);

    public static void AddDataTransferVoidStructure<T>(Guid id, Action<T> func) => Registrations.Add(id, func);
}
