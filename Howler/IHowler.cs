namespace Howler;

public interface IHowler
{
    IServiceProvider GetProvider();
    void InvokeVoid<T>(T data, Guid id);
    TResult Invoke<T, TResult>(T data, Guid id);

    TResult Invoke<TResult>(Func<TResult> original);
    TResult Invoke<TResult>(Func<TResult> original, Guid id);
    TResult Invoke<TData, TResult>(Func<TResult> original, Guid id, TData data);

    void InvokeVoid(Action original);
    void InvokeVoid(Action original, Guid id);
    void InvokeVoid<TData>(Action original, Guid id, TData data);
}