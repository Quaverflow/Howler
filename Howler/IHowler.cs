namespace Howler;

public interface IHowler
{
    TResult Invoke<TResult>(Guid id, Func<TResult> method, params object?[]? args);
    void InvokeVoid(Guid id, Action method, params object?[]? args); 
    Task<TResult> InvokeAsync<TResult>(Guid id, Func<Task<TResult>> method, params object?[]? args);
    Task InvokeVoidAsync(Guid id, Func<Task> method, params object?[]? args);
    
    void TransmitVoid(Guid id, params object?[]? data);
    Task TransmitVoidAsync(Guid id, params object?[]? data);
    TResult Transmit<TResult>(Guid id, params object?[]? data);
    Task<TResult> TransmitAsync<TResult>(Guid id, params object?[]? data);
    TResult Whisper<T, TResult>(Func<T, TResult> whisper) where T : class, IHowlerWhisper;
    Task<TResult> Whisper<T, TResult>(Func<T, Task<TResult>> whisper) where T : class, IHowlerWhisper;
    void Whisper<T>(Action<T> whisper) where T : class, IHowlerWhisper;
    Task Whisper<T>(Func<T, Task> whisper) where T : class, IHowlerWhisper;
}