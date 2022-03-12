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
    {
        if (original.Body is MethodCallExpression method)
        {
            var key = method.Method.GetKey() + structureId;
            _records.Add(key, substitute);
        }
    }




    public TResult Invoke<TResult>(Func<TResult> original)
    {

        var expr = original.Decompile();
        if (expr.Body is MethodCallExpression method)
        {
            var key = method.Method.GetKey();
            if(_records.TryGetValue(key, out var sub))
            {
                var substitute = sub as Func<TResult>;
                substitute.ThrowIfNull();

                return substitute.Invoke();
            }
        }

        return original.Invoke();
    }

    public TResult Invoke<TResult>(Func<TResult> original, Guid id) => original.Invoke();
    public TResult InvokeGeneric<TData, TResult>(Func<TResult> original, Guid id, TData data) => original.Invoke();
    public TResult Invoke<TResult>(Func<TResult> original, Guid id, object data) => original.Invoke();
    public void InvokeVoid(Action original, Guid? id = null) => original.Invoke();
}