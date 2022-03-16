using DelegateDecompiler;
using System.Linq.Expressions;

namespace Howler;

public partial class InTestHowler : IHowler
{
    private readonly Dictionary<Guid, Delegate?> _records = new();

    public TResult Invoke<TResult>(Guid id, Func<TResult> method, params object?[]? args)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid(Guid id, Action method, params object?[]? args)
    {
        throw new NotImplementedException();
    }

    public Task InvokeVoidAsync(Guid id, Func<Task> method, params object?[]? args)
    {
        throw new NotImplementedException();
    }

    public void TransmitVoid(Guid id, params object?[]? data)
    {
        throw new NotImplementedException();
    }

    public Task TransmitVoidAsync(Guid id, params object?[]? data)
    {
        throw new NotImplementedException();
    }

    public TResult Transmit<TResult>(Guid id, params object?[]? data)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> TransmitAsync<TResult>(Guid id, params object?[]? data)
    {
        throw new NotImplementedException();
    }



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

    public Task<TResult> InvokeAsync<TResult>(Guid id, Func<Task<TResult>> method, params object?[]? args)
    {
        throw new NotImplementedException();
    }
}