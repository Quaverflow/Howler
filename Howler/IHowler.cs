using System.Linq.Expressions;

namespace Howler;

public interface IHowler
{
    TResult Invoke<TResult>(Func<TResult> original);
    TResult Invoke<TResult>(Func<TResult> original, Guid id);
    TResult Invoke<TResult>(Func<TResult> original, Guid id, IHowlerData data);
    void InvokeVoid(Action original, Guid? id = null);
}