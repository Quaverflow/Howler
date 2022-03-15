namespace Howler;

public interface IHowler
{
    TResult Invoke<TResult>(Guid id, Func<TResult> method, params object?[]? args);
    void InvokeVoid(Guid id, Action method, params object?[]? args);

    void Transmit<TData>(TData data, Guid id);

    TResult Transmit<TData, TResult>(TData data, Guid id);
}