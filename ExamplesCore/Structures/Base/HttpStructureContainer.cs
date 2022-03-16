using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;
using Howler;

namespace ExamplesCore.Structures.Base;

public class HttpStructureContainer : HowlerStructureBuilder
{
    public override void InvokeRegistrations()
    {
        HowlerRegistry.AddStructure<IControllerResponse?>(StructuresIds.Get,
            method => Provider.GetRequiredService<IHttpStructure>().GetStructure(method));     
        
        HowlerRegistry.AddVoidStructure<Dto?>(StructuresIds.Post, 
            (method, data) => Provider.GetRequiredService<IHttpStructure>().PostStructure(method, data));

        HowlerRegistry.AddStructure<DtoNotifiable, Task<IControllerResponse?>>(StructuresIds.PostAndNotify, 
            async (method, data) => await Provider.GetRequiredService<IHttpStructure>().PostNotifiableStructure(method, data));
    }
}