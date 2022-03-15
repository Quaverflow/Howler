namespace Howler;

public class Howler : IHowler
{
    internal static IServiceProvider Provider = null!;

    public Howler(IServiceProvider provider)
    {
        Provider = provider;
    }

    private static object? InternalInvoke(Delegate? original, Guid id, params object?[]? data)
    {
        if (HowlerRegistration.Registrations.TryGetValue(id, out var structure))
        {
            try
            {
                if (original == null)
                {
                    return data != null && data.Any()
                        ? data.Length == 1
                            ? structure.DynamicInvoke(data[0])
                            : structure.DynamicInvoke(data)
                        : structure.DynamicInvoke();
                }

                return data != null && data.Any() 
                    ? data.Length == 1 
                        ? structure.DynamicInvoke(original, data[0]) 
                        : structure.DynamicInvoke(original, data) 
                    : structure.DynamicInvoke(original);
            }

            catch (Exception ex)
            {
                // this is to make sure we display the real exception rather than the DynamicInvoke wrapper exception.
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                throw;
            }
        }
        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
    }
    
    public void Transmit<TData>(TData data, Guid id) 
        => InternalInvoke(null, id, data);

    public TResult Transmit<TData, TResult>(TData data, Guid id)
        => InternalInvoke(null, id, data) is TResult result ? result : default!;


    #region Returning
    public TResult Invoke<TResult>(Func<TResult> original, Guid id)
        => InternalInvoke(original, id) is TResult result ? result : default!;

    public TResult Invoke<T1, TResult>
    (Func<TResult> original, Guid id, T1 arg1)
        => InternalInvoke(original, id, arg1)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2)
        => InternalInvoke(original, id, arg1, arg2)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3)
        => InternalInvoke(original, id, arg1, arg2, arg3)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)
            is TResult result ? result : default!;

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<TResult> original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)
            is TResult result ? result : default!;

    #endregion

    #region Void
    public void InvokeVoid(Action original, Guid id)
        => InternalInvoke(original, id);

    public void InvokeVoid<T1>
        (Action original, Guid id,
            T1 arg1)
        => InternalInvoke(original, id, arg1);

    public void InvokeVoid<T1, T2>(Action original, Guid id,
            T1 arg1, T2 arg2)
        => InternalInvoke(original, id, arg1, arg2);

    public void InvokeVoid<T1, T2, T3>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3)
        => InternalInvoke(original, id, arg1, arg2, arg3);

    public void InvokeVoid<T1, T2, T3, T4>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4);

    public void InvokeVoid<T1, T2, T3, T4, T5>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action original, Guid id,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        => InternalInvoke(original, id, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

    #endregion
}
