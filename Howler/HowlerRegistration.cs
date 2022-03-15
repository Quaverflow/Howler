namespace Howler;

public class HowlerRegistration
{
    internal static readonly Dictionary<Guid, Delegate> Registrations = new();

    //public static void AddStructure(Guid id, Func<Delegate, object?> func) => Registrations.Add(id, func);


    #region Void Structures

    public static void AddVoidStructure<T1>
        (Guid id, Action<Action, T1> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2>
        (Guid id, Action<Action, T1, T2> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3>
        (Guid id, Action<Action, T1, T2, T3> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4>
        (Guid id, Action<Action, T1, T2, T3, T4> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5>
        (Guid id, Action<Action, T1, T2, T3, T4, T5> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8, T9> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> func) => Registrations.Add(id, func);
    public static void AddVoidStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        (Guid id, Action<Action, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> func) => Registrations.Add(id, func);

    #endregion

    #region Return Structures

    public static void AddStructure<TResult>
        (Guid id, Func<Func<TResult>, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, TResult>
        (Guid id, Func<Func<TResult>, T1, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, T8, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult?> func) => Registrations.Add(id, func);
    public static void AddStructure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>
        (Guid id, Func<Func<TResult>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult?> func) => Registrations.Add(id, func);

    #endregion

    public static void AddDataTransferStructure<T, TResult>(Guid id, Func<T, TResult?> func) => Registrations.Add(id, func);
    public static void AddDataTransferStructure<T>(Guid id, Action<T> func) => Registrations.Add(id, func);

}
