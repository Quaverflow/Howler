using System.Linq.Expressions;
using Utilities.ExtensionMethods;

namespace Howler;

public class InTestHowler : IHowler
{
    private readonly Dictionary<string, Delegate> _setups = new();

    public void Register<TResult>(Expression<Func<TResult>> method, Func<TResult> substitute)
    {
        if (method.Body.NodeType == ExpressionType.Call)
        {
            _setups.Add(((MethodCallExpression)method.Body).Method.GetKey(), substitute);
        }
    }

    public void Register<TCaller, TResult>(Expression<Func<TCaller, TResult>> method, Func<TResult> substitute)
    {
        if (method.Body is MethodCallExpression methodCall)
        {
            _setups.Add(methodCall.Method.GetKey(), substitute);
        }
    }

    public TResult Invoke<TResult>(Expression<Func<TResult>> original, Guid? id = null)
    {
        if (original.Body is MethodCallExpression methodCall && _setups.TryGetValue(methodCall.Method.GetKey(), out var substitute))
        {
            if (substitute.DynamicInvoke() is TResult result)
            {
                return result;
            }
        }

        return original.Compile().Invoke();
    }

    public void InvokeVoid(Expression<Action> original, Guid? id = null)
    {
        if (_setups.TryGetValue(((MethodCallExpression)original.Body).Method.GetKey(), out var substitute))
        {
            substitute.DynamicInvoke();
        }

        original.Compile().Invoke();
    }
}