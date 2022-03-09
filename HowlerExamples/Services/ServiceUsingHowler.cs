using Howler;
using Howler.Tests.Objects.StructureExamples;

namespace HowlerExamples.Services;

public class ServiceUsingHowler : IServiceUsingHowler
{
    private readonly IHowler _howler;

    public ServiceUsingHowler(IHowler howler)
    {
        _howler = howler;
    }

    public string GetData() => _howler.Invoke(() => "Hello!", StructuresIds.LoggerStructureId);
    public string GetMoreData() => _howler.Invoke(() => "GoodBye!", StructuresIds.LoggerStructureId);
}