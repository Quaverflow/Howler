namespace Howler;

public class HowlerRegistration
{
    //internal static Dictionary<Guid, Func<Delegate, object?>> Structures = new();
    //internal static Dictionary<Guid, Action<Delegate>> VoidStructures = new();
    internal static readonly Dictionary<Guid, Delegate> Registrations = new();

   
    /// <summary>
    /// Register a structure
    /// </summary>
    /// <param name="func"></param>
    /// <param name="id"></param>
    public static void AddStructure(Guid id, Func<Delegate, object?> func)
    {
        Registrations.Add(id, func);
    }

    public static void AddStructure(Guid id, Action<Delegate> func)
    {
        Registrations.Add(id, func);
    }
}

public class HowlerRegistration<T> : HowlerRegistration
{
    //internal static Dictionary<Guid, Func<Delegate, T, object?>> TStructures = new();
    //internal static Dictionary<Guid, Action<Delegate, T>> TVoidStructures = new();
    //internal static Dictionary<Guid, Func<T, object?>> TDataTransferStructures = new();
    //internal static Dictionary<Guid, Action<T>> TDataTransferVoidStructures = new();
    
    /// <summary>
    /// Register a structure that accepts data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="func"></param>
    public static void AddStructure(Guid id, Func<Delegate, T, object?> func)
    {
        Registrations.Add(id, func);
    }

    public static void AddVoidStructure(Guid id, Action<Delegate, T> func)
    {
        Registrations.Add(id, func);
    }

    public static void AddDataTransferStructure(Guid id, Func<T, object?> func)
    {
        Registrations.Add(id, func);
    }

    public static void AddDataTransferVoidStructure(Guid id, Action<T> func)
    {
        Registrations.Add(id, func);
    }
}
