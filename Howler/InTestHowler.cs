using System.Diagnostics;

namespace Howler;

public class InTestHowler : IHowler
{
    public TResult Invoke<TResult>(Func<TResult> original) => original.Invoke();

    public TResult Invoke<TResult>(Func<TResult> original, Guid id) => original.Invoke();
    public TResult InvokeGeneric<TData, TResult>(Func<TResult> original, Guid id, TData data) => original.Invoke();
    public TResult Invoke<TResult>(Func<TResult> original, Guid id, object data) => original.Invoke();
    public void InvokeVoid(Action original, Guid? id = null) => original.Invoke();
}