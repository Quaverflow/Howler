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

    public TResult Invoke<TResult>(Expression<Func<TResult>> original)
    {
        if (_setups.TryGetValue(((MethodCallExpression)original.Body).Method.GetKey(), out var substitute))
        {
            if (substitute.DynamicInvoke() is TResult result)
            {
                return result;
            }
        }

        return original.Compile().Invoke();
    }

    public void InvokeVoid(Expression<Action> original)
    {
        if (_setups.TryGetValue(((MethodCallExpression)original.Body).Method.GetKey(), out var substitute))
        {
            substitute.DynamicInvoke();
        }

        original.Compile().Invoke();
    }

    public Task<TResult> InvokeAsync<TResult>(Expression<Func<Task<TResult>>> original)
    {
        if (_setups.TryGetValue(((MethodCallExpression)original.Body).Method.GetKey(), out var substitute))
        {
            if (substitute.DynamicInvoke() is Task<TResult> result)
            {
                return result;
            }
        }

        return original.Compile().Invoke();
    }

    public Task InvokeTask(Expression<Func<Task>> original)
    {
        if (_setups.TryGetValue(((MethodCallExpression)original.Body).Method.GetKey(), out var substitute))
        {
            if (substitute.DynamicInvoke() is Task result)
            {
                return result;
            }
        }

        return original.Compile().Invoke();
    }
}