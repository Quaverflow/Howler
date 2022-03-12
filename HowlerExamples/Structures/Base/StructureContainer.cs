using Howler;
using HowlerExamples.Models;

namespace HowlerExamples.Structures.Base;

public class StructureContainer : IHowlerStructureBuilder
{
    private readonly IHttpStructure _httpStructureContainer;


    public StructureContainer(IServiceProvider provider)
    {
        _httpStructureContainer = provider.GetRequiredService<IHttpStructure>();
    }

    public void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.GetStructureId, method => _httpStructureContainer.GetStructure(method));
        HowlerRegistration<Dto>.AddStructure(StructuresIds.PostStructureId, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration<object>.AddStructure(StructuresIds.PostStructureId, (method, data) => _httpStructureContainer.PostStructure(method, data));
    }
}