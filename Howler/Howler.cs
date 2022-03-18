using Microsoft.Extensions.DependencyInjection;

namespace Howler;

internal class Howler : IHowler
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

        if (HowlerRegistry.Registrations.TryGetValue(id, out var structure))
        {
            try
            {
                var declaringObject = ResolveService<IHowlerStructure>(structure.Method.DeclaringType);

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

    public async Task<TResult> InvokeAsync<TResult>(Guid id, Func<Task<TResult>> method, params object?[]? args)
        => await InternalInvokeAsync(method, id, args);

    public async Task InvokeVoidAsync(Guid id, Func<Task> method, params object?[]? args)
        => await InternalInvokeVoidAsync(method, id, args);

    private async Task InternalInvokeVoidAsync(Func<Task>? original, Guid id, params object?[]? data)
    {
        if (HowlerRegistry.Registrations.TryGetValue(id, out var structure))
        {
            try
            {
                if (original == null)
                {
                    object? task;
                    if (data != null && data.Any())
                    {
                        if (data.Length == 1)
                        {
                            task = structure.DynamicInvoke(data[0]);
                        }
                        else
                        {
                            task = structure.DynamicInvoke(data);
                        }
                    }
                    else
                    {
                        task =  structure.DynamicInvoke();
                    }
                 
                    var asTask = task as Task;
                    await asTask.ThrowIfNull();

                    await asTask;
                }
                else
                {
                    var declaringObject = ResolveService<IHowlerStructure>(structure.Method.DeclaringType);
                    var dataTask = data != null && data.Any()
                        ? data.Length == 1
                            ? structure.Method.Invoke(declaringObject, new[] { original, data[0] })
                            : structure.Method.Invoke(declaringObject, new object[] { original, data })
                        : structure.Method.Invoke(declaringObject, new object[] { original });

                    var asTask = dataTask as Task;
                    await asTask.ThrowIfNull();
                }

                return;
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

    private async Task<TResult> InternalInvokeAsync<TResult>(Func<Task<TResult>>? original, Guid id, params object?[]? data)
    {
        if (HowlerRegistry.Registrations.TryGetValue(id, out var structure))
        {
            try
            {
                if (original == null)
                {
                    object? task;
                    if (data != null && data.Any())
                    {
                        if (data.Length == 1)
                        {
                            task = structure.DynamicInvoke(data[0]);
                        }
                        else
                        {
                            task = structure.DynamicInvoke(data);
                        }
                    }
                    else
                    {
                        task =  structure.DynamicInvoke();
                    }

                    var asTask = task as Task<TResult>;
                    var resultDataTransfer = await asTask.ThrowIfNull();

                    return resultDataTransfer;
                }

                var declaringObject = ResolveService<IHowlerStructure>(structure.Method.DeclaringType);

                var dataTask = data != null && data.Any()
                    ? data.Length == 1
                        ? structure.Method.Invoke(declaringObject, new[] { original, data[0] })
                        : structure.Method.Invoke(declaringObject, new object[] { original, data })
                    : structure.Method.Invoke(declaringObject, new object[] { original });

                var asDataTask = dataTask as Task<TResult>;
                var result = await asDataTask.ThrowIfNull();

                return result;
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

    public void TransmitVoid(Guid id, params object?[]? data)
        => InternalInvoke(null, id, data);

    public TResult Transmit<TResult>(Guid id, params object?[]? data)
        => InternalInvoke(null, id, data) is TResult result ? result : default!;

    public async Task TransmitVoidAsync(Guid id, params object?[]? data)
        => await InternalInvokeVoidAsync(null, id, data);

    public async Task<TResult> TransmitAsync<TResult>(Guid id, params object?[]? data)
        => await InternalInvokeAsync<TResult>(null, id, data) is { } result ? result : default!;

    public TResult Whisper<T, TResult>(Func<T, TResult> whisper) where T : class, IHowlerWhisper 
        => whisper.Invoke(ResolveService<T, IHowlerWhisper>());

    public async Task<TResult> Whisper<T, TResult>(Func<T, Task<TResult>> whisper) where T : class, IHowlerWhisper 
        => await whisper.Invoke(ResolveService<T, IHowlerWhisper>());

    public void Whisper<T>(Action<T> whisper) where T : class, IHowlerWhisper 
        => whisper.Invoke(ResolveService<T, IHowlerWhisper>());

    public async Task Whisper<T>(Func<T, Task> whisper) where T : class, IHowlerWhisper 
        => await whisper.Invoke(ResolveService<T, IHowlerWhisper>());

    private TService ResolveService<TService, TInterface>() where TService : class, TInterface where TInterface : class
    {
        return (TService) _serviceProvider.GetServices<TInterface>()
            .First(x => x.GetType() == typeof(TService));
    }

    private TInterface ResolveService<TInterface>(Type? type) where TInterface : class
    {
        return  _serviceProvider.GetServices<TInterface>()
            .First(x => x.GetType() == type);
    }
}

public interface IHowlerWhisper
{

}