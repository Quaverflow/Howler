namespace Howler;

public static class HowlerRegistration
{
    internal static Dictionary<Guid, Func<Delegate, object?>> Structures = new();
    internal static Dictionary<Guid, Func<Delegate, IHowlerData, object?>> StructuresWithData = new();
    internal static bool ServicesRegistered;

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
    /// <param name="func"></param>
    /// <param name="id"></param>
    public static void AddStructure(Guid id, Func<Delegate, object?> func) => Structures.Add(id, func);
    public static void AddStructure(Guid id, Func<Delegate, IHowlerData, object?> func) => StructuresWithData.Add(id, func);

    public static void RemoveStructure(Guid id) => Structures.Remove(id);

    public static void UpdateStructure(Guid id, Func<Delegate, object?> func) => Structures[id] = func;
}

public interface IHowlerData
{

}