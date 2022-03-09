namespace HowlerExamples.Structures;

public class HumanCounter : IObserver<Dto>
{
    private IDisposable? _unsubscriber;
    private static readonly HumanCounter _counter = new();

    public static HumanCounter GetSingleton()
    {
        return _counter;
    }

    internal HumanCounter()
    {
        
    }

    public virtual void Subscribe(IObservable<Dto>? provider)
    {
        if (provider != null)
        {
            _unsubscriber = provider.Subscribe(this);
        }
    }

    public virtual void OnCompleted()
    {
        QueueSystem.HumansCreated.Enqueue($"All humans have been processed.");
        Unsubscribe();
    }

    public virtual void OnError(Exception e)
    {
        QueueSystem.HumansCreated.Enqueue($"there was an issue with getting the human. {e.Message}");
    }

    public virtual void OnNext(Dto dto)
    {
        QueueSystem.HumansCreated.Enqueue($"{dto.Name} {dto.Surname}, {dto.Age}");

    }

    public virtual void Unsubscribe()
    {
        _unsubscriber?.Dispose();
    }
}