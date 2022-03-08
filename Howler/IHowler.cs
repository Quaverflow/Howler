using System.Linq.Expressions;

namespace Howler;

public interface IHowler
{
    TResult Invoke<TResult>(Expression<Func<TResult>> original, Guid? id = null);
    void InvokeVoid(Expression<Action> original, Guid? id = null);
}