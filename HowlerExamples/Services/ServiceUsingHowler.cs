using Howler;
using Howler.Tests.Objects.StructureExamples;
using HowlerExamples.Models;
using HowlerExamples.Structures;
using Utilities;

namespace HowlerExamples.Services;

public class ServiceUsingHowler : IServiceUsingHowler
{
    private readonly IHowler _howler;

    public ServiceUsingHowler(IHowler howler)
    {
        _howler = howler;
    }

    public string GetData() => _howler.Invoke(() => "Hello!", StructuresIds.GetStructureId);
    public string GetMoreData() => _howler.Invoke(() => "GoodBye!", StructuresIds.GetStructureId);
    public Dto PostData(Dto dto) => _howler.Invoke(() => dto, StructuresIds.PostStructureId);

}

public class AuthProvider : IAuthProvider
{
    public bool HasAccess(bool yesNo)
    {
        yesNo.ThrowIfAssumptionFailed("You're not authorized to see this.");
        return yesNo;
    }
}