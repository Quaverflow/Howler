using Howler;
using HowlerExamples.Models;

namespace HowlerExamples.Structures;

public class HttpStructureContainer : IHowlerStructureBuilder
{
    private readonly IHttpStructure _httpStructureContainer;


    public HttpStructureContainer(IServiceProvider provider)
    {
        _httpStructureContainer = provider.GetRequiredService<IHttpStructure>();
    }

    public void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.GetStructureId, method => _httpStructureContainer.GetStructure(method));
        HowlerRegistration<Dto>.AddStructure(StructuresIds.PostStructureId, (method, data) => _httpStructureContainer.PostStructure(method, data));
    }
}