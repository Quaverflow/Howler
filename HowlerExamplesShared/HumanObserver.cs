namespace HowlerExamples.Structures;

public class HumanObserverFactory
{
    public static HumanObserver Observer { get; } = new ();

    public class HumanObserver : IObservable<Dto>
    {
        internal HumanObserver() { }

        private readonly List<IObserver<Dto>> _observers = new();

        public void HumanAdded(Dto dto)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        public IDisposable Subscribe(IObserver<Dto> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<Dto>> _observers;
            private readonly IObserver<Dto> _observer;

            public Unsubscriber(List<IObserver<Dto>> observers, IObserver<Dto> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}