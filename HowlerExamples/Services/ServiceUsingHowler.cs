using Howler;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Structures;

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
    public string PostData(Dto dto) => _howler.Invoke(dto.ToJson, StructuresIds.PostStructureId, dto);
    public string PostDataGenerics(DtoGeneric dto) => _howler.InvokeGeneric(dto.ToJson, StructuresIds.PostStructureOfTId, dto);
}
