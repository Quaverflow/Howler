namespace Howler;

public class HowlerRegistration
{
    internal static Dictionary<Guid, Func<Delegate, object?>> Structures = new();
    internal static Dictionary<Guid, Func<Delegate, object, object?>> StructuresWithHowlerData = new();

    /// <summary>
    /// Register a structure
    /// </summary>
    /// <param name="func"></param>
    /// <param name="id"></param>
    public static void AddStructure(Guid id, Func<Delegate, object?> func) => Structures.Add(id, func);
    public static void AddStructure(Guid id, Func<Delegate, object, object?> func) => StructuresWithHowlerData.Add(id, func);
}

public class HowlerRegistration<T>
{
    internal static Dictionary<Guid, Func<Delegate, T, object?>> Structures = new();
    
    /// <summary>
    /// Register a structure that accepts data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="func"></param>
    public static void AddStructure(Guid id, Func<Delegate, T, object?> func) => Structures.Add(id, func);
}

