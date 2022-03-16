//using DelegateDecompiler;
//using System.Linq.Expressions;
//using Microsoft.Extensions.DependencyInjection;
//using Utilities;

//namespace Howler;

//public partial class InTestHowler : IHowler
//{
//    private readonly Dictionary<Guid, Delegate?> _records = new();

//    public TResult Invoke<TResult>(Guid id, Func<TResult> method, params object?[]? args)
//        => InternalInvoke(method, id, args) is TResult result ? result : default!;

//    public void InvokeVoid(Guid id, Action method, params object?[]? args)
//        => InternalInvoke(method, id, args);

//    private object? InternalInvoke(Delegate? original, Guid id, params object?[]? data)
//    {

//        if (HowlerRegistry.Registrations.TryGetValue(id, out var structure))
//        {
//            try
//            {
//                var declaringObject = _serviceProvider.GetRequiredService<IHowlerStructure>();


//                if (original == null)
//                {
//                    return data != null && data.Any()
//                        ? data.Length == 1
//                            ? structure.Method.Invoke(declaringObject, new[] { data[0] })
//                            : structure.Method.Invoke(declaringObject, new object[] { data })
//                        : structure.Method.Invoke(declaringObject, null);
//                }

//                return data != null && data.Any()
//                    ? data.Length == 1
//                        ? structure.Method.Invoke(declaringObject, new[] { original, data[0] })
//                        : structure.Method.Invoke(declaringObject, new object[] { original, data })
//                    : structure.Method.Invoke(declaringObject, new object[] { original });
//            }

//            catch (Exception ex)
//            {
//                // this is to make sure we display the real exception rather than the DynamicInvoke wrapper exception.
//                if (ex.InnerException != null)
//                {
//                    throw ex.InnerException;
//                }
//                throw;
//            }
//        }
//        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
//    }

//    public async Task<TResult> InvokeAsync<TResult>(Guid id, Func<Task<TResult>> method, params object?[]? args)
//        => await InternalInvokeAsync(method, id, args);

//    public async Task InvokeVoidAsync(Guid id, Func<Task> method, params object?[]? args)
//        => await InternalInvokeVoidAsync(method, id, args);

//    private async Task InternalInvokeVoidAsync(Func<Task>? original, Guid id, params object?[]? data)
//    {
//        if (HowlerRegistry.Registrations.TryGetValue(id, out var structure))
//        {
//            try
//            {
//                var declaringObject = _serviceProvider.GetRequiredService<IHowlerStructure>();

//                if (original == null)
//                {
//                    object? task;
//                    if (data != null && data.Any())
//                    {
//                        if (data.Length == 1)
//                        {
//                            task = structure.DynamicInvoke(data[0]);
//                        }
//                        else
//                        {
//                            task = structure.DynamicInvoke(data);
//                        }
//                    }
//                    else
//                    {
//                        task =  structure.DynamicInvoke();
//                    }


//                    var asTask = task as Task;
//                    asTask.ThrowIfNull();

//                    await asTask;
//                }
//                else
//                {
//                    var dataTask = data != null && data.Any()
//                        ? data.Length == 1
//                            ? structure.Method.Invoke(declaringObject, new[] { original, data[0] })
//                            : structure.Method.Invoke(declaringObject, new object[] { original, data })
//                        : structure.Method.Invoke(declaringObject, new object[] { original });

//                    var asDataTask = dataTask as Task;
//                    asDataTask.ThrowIfNull();

//                    await asDataTask;
//                }

//                return;
//            }

//            catch (Exception ex)
//            {
//                // this is to make sure we display the real exception rather than the DynamicInvoke wrapper exception.
//                if (ex.InnerException != null)
//                {
//                    throw ex.InnerException;
//                }
//                throw;
//            }
//        }
//        throw new InvalidOperationException($"The requested structure was not found for id: {id}");
//    }

//    private async Task<TResult> InternalInvokeAsync<TResult>(Func<Task<TResult>>? original, Guid id, params object?[]? data)
//    {
//        if (HowlerRegistry.Registrations.TryGetValue(id, out var structure))
//        {
//            try
//            {
//                var declaringObject = _serviceProvider.GetRequiredService<IHowlerStructure>();
//                if (original == null)
//                {
//                    object? task;
//                    if (data != null && data.Any())
//                    {
//                        if (data.Length == 1)
//                        {
//                            task = structure.DynamicInvoke(data[0]);
//                        }
//                        else
//                        {
//                            task = structure.DynamicInvoke(data);
//                        }
//                    }
//                    else
//                    {
//                        task =  structure.DynamicInvoke();
//                    }

//                    var asTask = task as Task<TResult>;
//                    asTask.ThrowIfNull();

//                    return await asTask;
//                }

//                var dataTask = data != null && data.Any()
//                    ? data.Length == 1
//                        ? structure.Method.Invoke(declaringObject, new[] { original, data[0] })
//                        : structure.Method.Invoke(declaringObject, new object[] { original, data })
//                    : structure.Method.Invoke(declaringObject, new object[] { original });

//                var asDataTask = dataTask as Task<TResult>;
//                asDataTask.ThrowIfNull();

//                return await asDataTask;
//            }

//            catch (Exception ex)
//            {
//                // this is to make sure we display the real exception rather than the DynamicInvoke wrapper exception.
//                if (ex.InnerException != null)
//                {
//                    throw ex.InnerException;
//                }
//                throw;
//            }
//        }
//        throw new InvalidOperationException($"The requested structure was not found for id: {id}");

//    }

//    public void TransmitVoid(Guid id, params object?[]? data)
//        => InternalInvoke(null, id, data);

//    public TResult Transmit<TResult>(Guid id, params object?[]? data)
//        => InternalInvoke(null, id, data) is TResult result ? result : default!;

//    public async Task TransmitVoidAsync(Guid id, params object?[]? data)
//        => await InternalInvokeVoidAsync(null, id, data);

//    public async Task<TResult> TransmitAsync<TResult>(Guid id, params object?[]? data)
//        => await InternalInvokeAsync<TResult>(null, id, data) is { } result ? result : default!;
//}
//}

//public partial class InTestHowler
//{
//    public void Register(Guid structureId)
//       => RegisterInternal(null, structureId);


//    public void RegisterVoid(Action? substitute, Guid structureId)
//       => RegisterInternal(substitute, structureId);

//    public void Register<TResult>(Func<TResult>? substitute, Guid structureId)
//       => RegisterInternal(substitute, structureId);

//    public void Register<T, TResult>
//        (Func<T, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, TResult>
//        (Func<T1, T2, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, TResult>
//        (Func<T1, T2, T3, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, TResult>
//        (Func<T1, T2, T3, T4, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, TResult>
//        (Func<T1, T2, T3, T4, T5, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);
//    public void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>
//        (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>? substitute, Guid structureId)
//        => RegisterInternal(substitute, structureId);

//    private void RegisterInternal(Delegate? substitute, Guid key)
//    {
//        if (_records.ContainsKey(key))
//        {
//            _records[key] = substitute;
//        }
//        else
//        {
//            _records.Add(key, substitute);
//        }
//    }

//    public Task<TResult> InvokeAsync<TResult>(Guid id, Func<Task<TResult>> method, params object?[]? args)
//    {
//        throw new NotImplementedException();
//    }
//}