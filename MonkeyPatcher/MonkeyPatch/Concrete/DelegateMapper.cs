namespace MonkeyPatcher.MonkeyPatch.Concrete;

internal class DelegateMapper
{
    public DelegateMapper(Delegate? @delegate, string key)
    {
        Delegate = @delegate;
        Key = key;
    }

    public Delegate? Delegate { get; set; }
    public string Key { get; }
}