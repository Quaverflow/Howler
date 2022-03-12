namespace Howler;

public class Howler : IHowler
{
    public TResult Invoke<TResult>(Func<TResult> original) => original.Invoke();

    public TResult? Invoke<TResult>(Func<TResult> original, Guid id)
    {
        if (HowlerRegistration.Structures.TryGetValue(id, out var func))
        {
            var result = func.Invoke(original);
            return result != null ? (TResult)result : default;
        }

        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
    }

    public TResult? Invoke<TData, TResult>(Func<TResult> original, Guid id, TData data)
    {
        if (HowlerRegistration<TData>.Structures.TryGetValue(id, out var func))
        {
            var result = func.Invoke(original, data);
            return result != null ? (TResult)result : default;

        }

        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
    }

    public void InvokeVoid(Action original, Guid? id = null)
    {
        if (id == null)
        {
            original.Invoke();
            return;
        }

        if (HowlerRegistration.Structures.TryGetValue(id.Value, out var func))
        {
            func.Invoke(original);
        }

        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
    }
}
