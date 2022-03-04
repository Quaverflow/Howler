using System.Linq.Expressions;

namespace Howler;

public class Howler : IHowler
{
    public TResult Invoke<TResult>(Expression<Func<TResult>> original) => original.Compile().Invoke();
    public void InvokeVoid(Expression<Action> original) => original.Compile().Invoke();
}