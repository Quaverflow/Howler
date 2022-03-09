namespace Howler;

public interface IHowlerStructureBuilder
{
    /// <summary>
    /// Call all of your registrations here. This method will be invoked by the middleware at runtime to register the structures
    /// </summary>
    void InvokeRegistrations();
}