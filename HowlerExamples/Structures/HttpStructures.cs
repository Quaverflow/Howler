using Howler;

namespace HowlerExamples.Structures;

public class HttpStructures : IHowlerStructureBuilder
{
    private readonly IHttpStructureContainer _httpStructureContainer;


    public HttpStructures(IServiceProvider provider)
    {
        _httpStructureContainer = provider.GetRequiredService<IHttpStructureContainer>();
    }

    public void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.GetStructureId, method => _httpStructureContainer.GetStructure(method));
        HowlerRegistration.AddStructure(StructuresIds.PostStructureId, method => _httpStructureContainer.PostStructure(method));
    }
}