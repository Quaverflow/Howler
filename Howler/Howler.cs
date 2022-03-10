using System.Linq.Expressions;

namespace Howler;

public class Howler : IHowler
{
    //private readonly IServiceProvider _serviceProvider;

    //public Howler(IServiceProvider serviceProvider)
    //{
    //    _serviceProvider = serviceProvider;
    //}

    public TResult Invoke<TResult>(Func<TResult> original, Guid? id = null)
    {
        if (id == null)
        {
            return original.Invoke();
        }

        if (HowlerRegistration.Structures.TryGetValue(id.Value, out var func))
        {
            var result = func.Invoke(original);
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