using System.Linq.Expressions;
using FastExpressionCompiler;

namespace Howler;

public class Howler : IHowler
{
    public TResult Invoke<TResult>(Expression<Func<TResult>> original, Guid? id = null)
    {
        if (id == null)
        {
            return original.CompileFast().Invoke();
        }

        if (HowlerRegistration.Structures.TryGetValue(id.Value, out var func))
        {
            var result = func.Invoke(original.CompileFast());
            return result != null ? (TResult)result : default;
        }

        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
    }

    public void InvokeVoid(Expression<Action> original, Guid? id = null)
    {
        if (id == null)
        {
            original.CompileFast().Invoke();
            return;
        }

        if (HowlerRegistration.Structures.TryGetValue(id.Value, out var func))
        {
            func.Invoke(original.CompileFast());
        }

        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
    }
}