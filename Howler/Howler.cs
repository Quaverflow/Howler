using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Utilities;

namespace Howler;

public class Howler : IHowler
{
    private readonly IServiceProvider _serviceProvider;

    public Howler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TResult Invoke<TResult>(Guid id, Func<TResult> method, params object?[]? args) 
        => InternalInvoke(method, id, args) is TResult result ? result : default!;

    public void InvokeVoid(Guid id, Action method, params object?[]? args)
        => InternalInvoke(method, id, args);

    private object? InternalInvoke(Delegate? original, Guid id, params object?[]? data)
    {

        if (HowlerRegistry.Registrations.TryGetValue(id, out var value))
        {
            var structure = value.Item2;
            var type = value.Item1;
            type.BaseType.ThrowIfNull();

            try
            {
                var declaringObject = _serviceProvider.GetRequiredService(type.BaseType);

                if (original == null)
                {
                    return data != null && data.Any()
                        ? data.Length == 1
                            ? structure.Method.Invoke(declaringObject, new[] { data[0] })
                            : structure.Method.Invoke(declaringObject, new object[] { data })
                        : structure.Method.Invoke(declaringObject, null);
                }

                return data != null && data.Any()
                    ? data.Length == 1
                        ? structure.Method.Invoke(declaringObject, new[] { original, data[0] })
                        : structure.Method.Invoke(declaringObject, new object[] { original, data })
                    : structure.Method.Invoke(declaringObject, new object[] { original });
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

    public void Transmit<TData>(TData data, Guid id)
        => InternalInvoke(null, id, data);

    public TResult Transmit<TData, TResult>(TData data, Guid id)
        => InternalInvoke(null, id, data) is TResult result ? result : default!;
}
