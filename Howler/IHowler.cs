using System.Linq.Expressions;

namespace Howler;

public interface IHowler
{
    TResult Invoke<TResult>(Expression<Func<TResult>> original);
    void InvokeVoid(Expression<Action> original);
}