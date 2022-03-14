using System.Linq.Expressions;
using DelegateDecompiler;
using Utilities;
using Utilities.ExtensionMethods;

namespace Howler;

public partial class InTestHowler : IHowler
{
    private readonly Dictionary<Guid, Delegate> _records = new();

    public IServiceProvider GetProvider()
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T>(T data, Guid id) => InvokeInternal(null, id, data);

    public TResult Invoke<T, TResult>(T data, Guid id) => (TResult)InvokeInternal(null, id, data);

    public TResult Invoke<TResult>(Func<TResult> original, Guid id) => (TResult)InvokeInternal(original, id);

    public TResult Invoke<TData, TResult>(Func<TResult> original, Guid id, TData data) => (TResult)InvokeInternal(original, id, data);

    public void InvokeVoid(Action original, Guid id) => InvokeInternal(original, id);

    public void InvokeVoid<TData>(Action original, Guid id, TData data) => InvokeInternal(original, id, data);

    private object? InvokeInternal(Delegate original, Guid id, object? data = null)
    {
        if (_records.TryGetValue(id, out var sub))
        {
            sub.ThrowIfNull();
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
    public void Register<TResult>(Expression<Func<TResult>> original, Func<TResult> substitute, Guid structureId)
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

    private void RegisterInternal<TResult>(Expression<Func<TResult>> original, Delegate substitute, Guid key)
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