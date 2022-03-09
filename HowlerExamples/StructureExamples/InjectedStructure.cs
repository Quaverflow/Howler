using Howler;
using Howler.Tests.Objects.StructureExamples;

namespace HowlerExamples.StructureExamples;

public class InjectedStructure : IHowlerStructureBuilder
{
    private readonly IFakeLogger _logger;
    public InjectedStructure(IFakeLogger logger)
    {
        _logger = logger;
    }

    private void RegisterLoggerStructure()
    {
        HowlerRegistration.AddStructure(StructuresIds.LoggerStructureId, x =>
        {
            _logger.Log("hello");
            return x.DynamicInvoke();
        });
    }

    public void InvokeRegistrations()
    {
        RegisterLoggerStructure();
    }
}