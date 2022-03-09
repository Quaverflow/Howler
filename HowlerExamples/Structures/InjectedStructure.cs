using Howler;
using Howler.Tests.Objects.StructureExamples;
using HowlerExamples.Services;

namespace HowlerExamples.Structures;

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
            _logger.Log("The service was called");
            try
            {
                var result = x.DynamicInvoke();

                _logger.Log("The service call succeeded");
                return result;
            }
            catch (Exception e)
            {
                _logger.Log($"The service failed with exception {e.Message}");
                throw;
            }
        });
    }

    public void InvokeRegistrations()
    {
        RegisterLoggerStructure();
    }
}