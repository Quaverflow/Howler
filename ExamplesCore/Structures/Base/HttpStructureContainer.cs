using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;
using Howler;
using Microsoft.Extensions.DependencyInjection;

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

public class InfrastructureStructureContainer : HowlerStructureBuilder
{
    public override void InvokeRegistrations()
    {
        HowlerRegistry.AddDataTransferStructure<EmailDto>(StructuresIds.SendEmail, 
            data => Provider.GetRequiredService<INotificationStructure>().SendEmail(data));

        HowlerRegistry.AddDataTransferStructure<SmsDto>(StructuresIds.SendSms, 
            data => Provider.GetRequiredService<INotificationStructure>().SendSms(data));

        HowlerRegistry.AddDataTransferStructure<IValidationStructureData, Task>(StructuresIds.Validate, 
            async data => await Provider.GetRequiredService<IValidationStructure>().ValidateAsync(data));

        HowlerRegistry.AddDataTransferStructure<MicroServiceCommunicationStructureData, Task<MicroServiceResult?>>(StructuresIds.NotifyMicroService,
            async data => await Provider.GetRequiredService<IMicroServiceCommunicationStructure>().SendToMicroService(data));
    }
}

