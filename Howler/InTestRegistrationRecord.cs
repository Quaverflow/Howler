namespace Howler;

internal class InTestRegistrationRecord
{
    public InTestRegistrationRecord(string key, Delegate substitute)
    {
        Key = key;
        Substitute = substitute;
    }

    public string Key { get; }
    public Delegate Substitute { get; }
}