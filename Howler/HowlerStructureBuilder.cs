namespace Howler;

public abstract class HowlerStructureBuilder
{
    protected IServiceProvider Provider => Howler.Provider;

    /// <summary>
    /// Call all of your registrations here. This method will be invoked by the middleware at runtime to register the structures
    /// </summary>
    public abstract void InvokeRegistrations();
}