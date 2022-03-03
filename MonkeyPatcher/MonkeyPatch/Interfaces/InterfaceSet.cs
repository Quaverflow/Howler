using System.Collections;

namespace MonkeyPatcher.MonkeyPatch.Interfaces;

internal class InterfaceSet<T> : IEnumerable<T> where T : Interceptor
{
    private readonly HashSet<T> set = new(new MethodComparer<Interceptor>());

    public void Add(T value)
    {
        if (set.Contains(value))
        {
            set.Remove(value);
        }

        set.Add(value);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return set.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}