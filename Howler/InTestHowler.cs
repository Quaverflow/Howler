using System.Linq.Expressions;
using DelegateDecompiler;
using Utilities;
using Utilities.ExtensionMethods;

namespace Howler;

public partial class InTestHowler : IHowler
{
    private readonly Dictionary<string, Delegate> _records = new();

    public void InvokeVoid<T>(T data, Guid id)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<T, TResult>(T data, Guid id)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<TResult>(Func<TResult> original)
    {
        var expr = original.Decompile();
        if (expr.Body is MethodCallExpression method)
        {
            var key = method.Method.GetKey();
            if (_records.TryGetValue(key, out var sub))
            {
                var substitute = sub as Func<TResult>;
                substitute.ThrowIfNull();
                return substitute.Invoke();
            }
        }
        return original.Invoke();
    }

    public TResult Invoke<TResult>(Func<TResult> original, Guid id)
    {
        var expr = original.Decompile();
        if (expr.Body is MethodCallExpression method)
        {
            var key = method.Method.GetKey() + id;
            if (_records.TryGetValue(key, out var sub))
            {
                if (sub is Func<TResult> substitute1)
                {
                    substitute1.ThrowIfNull();
                    return substitute1.Invoke();
                }

                var args = method.Arguments.Select(x => ((ConstantExpression)x).Value).ToArray();
                return (TResult)sub.DynamicInvoke(args);
            }
        }

        return original.Invoke();
    }

    public TResult Invoke<TData, TResult>(Func<TResult> original, Guid id, TData data) => original.Invoke();

    public void InvokeVoid(Action original)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid(Action original, Guid id)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<TData>(Action original, Guid id, TData data)
    {
        throw new NotImplementedException();
    }

}


public partial class InTestHowler
{
    public void Register<TResult>(Expression<Func<TResult>> original, Func<TResult> substitute, Guid? structureId = null)
       => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T>
        (Expression<Func<TResult>> original, Func<T, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2>
        (Expression<Func<TResult>> original, Func<T1, T2, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);
    public void Register<TResult, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
        (Expression<Func<TResult>> original, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> substitute, Guid structureId)
        => RegisterInternal(original, substitute, structureId);

    private void RegisterInternal<TResult>(Expression<Func<TResult>> original, Delegate substitute, Guid? structureId = null)
    {
        if (original.Body is MethodCallExpression method)
        {
            var key = method.Method.GetKey() + structureId;
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
}