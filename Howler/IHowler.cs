namespace Howler;

public interface IHowler
{
    void Transmit<TData>(TData data, Guid id);

    TResult Transmit<TData, TResult>(TData data, Guid id);


    #region Returning

    TResult Invoke<TResult>(Func<TResult> original, Guid id);

    TResult Invoke<T1, TResult>(Func<TResult> original, Guid id, T1 arg1);

    TResult Invoke<T1, T2, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2);

    TResult Invoke<T1, T2, T3, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3);

    TResult Invoke<T1, T2, T3, T4, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    TResult Invoke<T1, T2, T3, T4, T5, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);

    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);


    TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<TResult> original, Guid id,
       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);

    #endregion

    #region Void

    void InvokeVoid(Action original, Guid id);

    void InvokeVoid<T1>
        (Action original, Guid id, T1 arg1);

    void InvokeVoid<T1, T2>(Action original, Guid id, T1 arg1, T2 arg2);

    void InvokeVoid<T1, T2, T3>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3);

    void InvokeVoid<T1, T2, T3, T4>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    void InvokeVoid<T1, T2, T3, T4, T5>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    void InvokeVoid<T1, T2, T3, T4, T5, T6>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8>(Action original, Guid id,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action original, Guid id,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action original, Guid id,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action original, Guid id,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action original, Guid id,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action original, Guid id,
           T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action original, Guid id,
           T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);

    void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action original, Guid id,
           T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);

    #endregion

}