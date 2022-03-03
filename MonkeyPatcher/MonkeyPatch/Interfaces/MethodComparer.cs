namespace MonkeyPatcher.MonkeyPatch.Interfaces;

internal class MethodComparer<T> : IEqualityComparer<T> where T : Interceptor
{
    public bool Equals(T? x, T? y)
    {
        return x.Original.Method.Name == y.Original.Method.Name;
    }

    public int GetHashCode(T obj)
    {
        return obj.Original.GetHashCode();
    }
}