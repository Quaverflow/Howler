using System.Linq.Expressions;

namespace Howler;

public interface IHowler
{
    TResult Invoke<TResult>(Func<TResult> original, Guid? id = null);
    void InvokeVoid(Action original, Guid? id = null);
}