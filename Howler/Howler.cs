using System.Diagnostics.CodeAnalysis;

namespace Howler;

public class Howler : IHowler
{
    internal static IServiceProvider Provider = null!;
    public Howler(IServiceProvider provider)
    {
        Provider = provider;
    }

    private static object? InternalInvoke(Delegate? original, Guid id, object? data = null)
    {
        if (HowlerRegistration.Registrations.TryGetValue(id, out var structure))
        {
            try
            {
                if (original == null)
                {
                    return structure.DynamicInvoke(data);
                }

                return data != null
                    ? structure.DynamicInvoke(original, data)
                    : structure.DynamicInvoke(original);
            }

            catch (Exception ex)
            {
                // this is to make sure we display the real exception rather than the DynamicInvoke wrapper exception.
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                throw;
            }
        }
        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
    }

    public TResult Invoke<TResult>(Func<TResult> original) => original.Invoke();

    public TResult Invoke<TResult>(Func<TResult> original, Guid id)
    {
        var result = InternalInvoke(original, id);
        return result == null ? default : (TResult)result;
    }

    public TResult Invoke<TData, TResult>(Func<TResult> original, Guid id, TData data)
    {
        var result = InternalInvoke(original, id, data);
        return result == null ? default : (TResult)result;
    }

    public void InvokeVoid(Action original) => original.Invoke();

    public void InvokeVoid(Action original, Guid id) => InternalInvoke(original, id);

    public void InvokeVoid<TData>(Action original, Guid id, TData data) => InternalInvoke(original, id, data);

    public IServiceProvider GetProvider()
    {
        throw new NotImplementedException();
    }

    public void InvokeVoid<T>(T data, Guid id) => InternalInvoke(null, id, data);

    public TResult Invoke<T, TResult>(T data, Guid id)
    {
        var result = InternalInvoke(null, id, data);
        return result == null ? default : (TResult)result;
    }
}
