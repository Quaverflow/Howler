using System.Linq.Expressions;

namespace Howler;

public class InTestHowler : IHowler
{
    public TResult Invoke<TResult>(Func<TResult> original, Guid? id = null) => original.Invoke();
    public TResult Invoke<TResult>(Func<TResult> original)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<TResult>(Func<TResult> original, Guid id)
    {
        throw new NotImplementedException();
    }

    public TResult Invoke<TResult>(Func<TResult> original, Guid id, IHowlerData data)
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid(Action original, Guid? id = null) => original.Invoke();
}