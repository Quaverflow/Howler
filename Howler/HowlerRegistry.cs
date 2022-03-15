using Utilities;

namespace Howler;

public interface IHowlerRegistry
{
    void AddStructure(Guid id, Delegate func);
}

public class HowlerRegistry 
{
    internal static readonly Dictionary<Guid, (Type, Delegate)> Registrations = new();

    public static void AddStructure(Guid id, Delegate func)
    {
        func.Target.ThrowIfNull();
        var target = func.Target.GetType();
        Registrations.Add(id, (target, func));
    }

    //public static void AddDataTransferStructure<T, TResult>(Guid id, Func<T, TResult?> func) => Registrations.Add(id, func);
    //public static void AddDataTransferStructure<T>(Guid id, Action<T> func) => Registrations.Add(id, func);

}
