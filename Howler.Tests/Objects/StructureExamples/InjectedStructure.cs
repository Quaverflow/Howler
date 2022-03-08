namespace Howler.Tests.Objects.StructureExamples;

public class InjectedStructure : IInjectedStructure
{
    private readonly IFakeLogger _logger;
    public InjectedStructure(IFakeLogger logger)
    {
        _logger = logger;
    }

    public void RegisterLoggerStructure()
    {
        HowlerRegistration.AddStructure(StructuresIds.LoggerStructureId, x =>
        {
            _logger.Log("hello");
            return x.DynamicInvoke();
        });
    }
}