using System.Diagnostics;
using System.Linq.Expressions;
using DelegateDecompiler;
using Utilities;
using Utilities.ExtensionMethods;

namespace Howler;

internal class InTestRegistrationRecord
{
    public InTestRegistrationRecord(string key, Delegate substitute)
    {
        Key = key;
        Substitute = substitute;
    }

    public string Key { get; }
    public Delegate Substitute { get; }
}

public class InTestHowler : IHowler
{
    private readonly Dictionary<string, Delegate> _records = new();
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

    public void Register<TResult, T1, T2, T3, T4, T5,T6>
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

    public TResult InvokeGeneric<TData, TResult>(Func<TResult> original, Guid id, TData data) => original.Invoke();
    public TResult Invoke<TResult>(Func<TResult> original, Guid id, object data) => original.Invoke();
    public void InvokeVoid(Action original, Guid? id = null) => original.Invoke();
}