using DelegateDecompiler;
using System.Linq.Expressions;

namespace Howler;

public partial class InTestHowler : IHowler
{
    private readonly Dictionary<Guid, Delegate?> _records = new();

    public void InvokeVoid<T>(T data, Guid id)
    {
        if (_records.TryGetValue(id, out var sub))
        {
            sub?.DynamicInvoke();
        }
    }

    public TResult Invoke<T, TResult>(T data, Guid id)
    {
        if (_records.TryGetValue(id, out var sub))
        {
            if (sub is Action)
            {
                sub.DynamicInvoke();
            }

            if (typeof(TResult) == typeof(Task))
            {
                if (sub is not Action)
                {
                    sub?.DynamicInvoke(data);
                }
                return (TResult)(Task.CompletedTask as object);
            }

            var result = (TResult)sub?.DynamicInvoke(data);

            if (result == null && typeof(TResult).BaseType == typeof(Task))
            {
                throw new NotImplementedException("Invocations requiring a Task<T> as result require a registration");
            }

            return result;
        }

        if (typeof(TResult) == typeof(Task))
        {
            return (TResult)(Task.CompletedTask as object);
        }
        return default;
    }


    public TResult Invoke<TResult>(Guid id, Func<TResult> method, params object?[]? args)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid(Guid id, Action method, params object?[]? args)
    {
        throw new NotImplementedException();
    }

    public void Transmit<TData>(TData data, Guid id)
    {
        throw new NotImplementedException();
    }

    public TResult Transmit<TData, TResult>(TData data, Guid id)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<TResult>(Func<TResult> original, Guid id) => (TResult)InvokeInternal(original, id);
    public TResult Invoke<T1, TResult>(Func<TResult> original, Guid id, T1 arg1)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
        T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
        T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3,
        T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2, T3 arg3,
        T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<TResult> original, Guid id, T1 arg1, T2 arg2,
        T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<TResult> original, Guid id, T1 arg1,
        T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<TResult> original, Guid id, T1 arg1,
        T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13,
        T14 arg14)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<TResult> original, Guid id,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12,
        T13 arg13, T14 arg14, T15 arg15)
    {
        throw new NotImplementedException();
    }

    public TResult Transmit<TData, TResult>(Func<TResult> original, Guid id, TData data) => (TResult)InvokeInternal(original, id, data);

    public void InvokeVoid(Action original, Guid id) => InvokeInternal(original, id);

    public void InvokeVoid<T1, T2>(Action original, Guid id, T1 arg1, T2 arg2)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        T6 arg6, T7 arg7, T8 arg8)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
        T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
        T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3,
        T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action original, Guid id, T1 arg1, T2 arg2, T3 arg3,
        T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action original, Guid id, T1 arg1, T2 arg2,
        T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action original, Guid id, T1 arg1, T2 arg2,
        T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13,
        T14 arg14)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action original, Guid id, T1 arg1,
        T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13,
        T14 arg14, T15 arg15)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<TData>(Action original, Guid id, TData data) => InvokeInternal(original, id, data);


    private object? InvokeInternal(Delegate original, Guid id, object? data = null)
    {
        if (_records.TryGetValue(id, out var sub))
        {
            if (sub == null)
            {
                return null;
            }

            var parameters = sub.Decompile().Parameters;
            if (!parameters.Any())
            {
                return sub.DynamicInvoke();
            }

            var subLambda = sub.Decompile();
            var subExp = subLambda.Body;
            if (subExp is MethodCallExpression subMethod)
            {
                var args = subMethod.Arguments;
                return sub.DynamicInvoke(args);
            }
            if (subExp is BinaryExpression)
            {
                var originalExp = original.Decompile();
                if (originalExp.Body is MethodCallExpression originalMethod)
                {
                    var args = originalMethod.Arguments.Select(x => ((ConstantExpression)x).Value).ToArray();
                    return sub.DynamicInvoke(args);
                }
            }
        }

        var originalExpression = original.Decompile();
        if (originalExpression.Body is MethodCallExpression method)
        {
            var args = method.Arguments.Select(x => ((ConstantExpression)x).Value).ToArray();
            return original.DynamicInvoke(args);
        }

        return original.DynamicInvoke();
    }
}

public partial class InTestHowler
{
    public void Register(Guid structureId)
       => RegisterInternal(null, structureId);


    public void RegisterVoid(Action? substitute, Guid structureId)
       => RegisterInternal(substitute, structureId);

    public void Register<TResult>(Func<TResult>? substitute, Guid structureId)
       => RegisterInternal(substitute, structureId);

    public void Register<T, TResult>
        (Func<T, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, TResult>
        (Func<T1, T2, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, TResult>
        (Func<T1, T2, T3, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, TResult>
        (Func<T1, T2, T3, T4, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, TResult>
        (Func<T1, T2, T3, T4, T5, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, TResult>
        (Func<T1, T2, T3, T4, T5, T6, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);
    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>
        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>? substitute, Guid structureId)
        => RegisterInternal(substitute, structureId);

    private void RegisterInternal(Delegate? substitute, Guid key)
    {
        if (_records.ContainsKey(key))
        {
            _records[key] = substitute;
        }
        else
        {
            _records.Add(key, substitute);
        }
    }
}