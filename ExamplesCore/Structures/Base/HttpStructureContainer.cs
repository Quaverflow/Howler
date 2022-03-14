using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;
using Howler;
using Microsoft.Extensions.DependencyInjection;

namespace ExamplesCore.Structures.Base;

public class HttpStructureContainer : HowlerStructureBuilder
{
    public override void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.Get, method 
            => Provider.GetRequiredService<IHttpStructure>().GetStructure(method));
        HowlerRegistration.AddStructure<Dto>(StructuresIds.Post, (method, data)
            => Provider.GetRequiredService<IHttpStructure>().PostStructure(method, data));
        HowlerRegistration.AddStructure<DtoNotifiable>(StructuresIds.PostAndNotify, (method, data)
            => Provider.GetRequiredService<IHttpStructure>().PostStructure(method, data));
    }
}

public class InfrastructureStructureContainer : HowlerStructureBuilder
{
    public override void InvokeRegistrations()
    {
        HowlerRegistration.AddDataTransferStructure<EmailDto>(StructuresIds.SendEmail, data 
            => Provider.GetRequiredService<INotificationStructure>().SendEmail(data));
        HowlerRegistration.AddDataTransferStructure<SmsDto>(StructuresIds.SendSms, data 
            => Provider.GetRequiredService<INotificationStructure>().SendSms(data));

        HowlerRegistration.AddDataTransferStructure<IValidationStructureData>(StructuresIds.Validate, data
            => Provider.GetRequiredService<IValidationStructure>().ValidateAsync(data));

        HowlerRegistration.AddDataTransferStructure<MicroServiceCommunicationStructureData>(StructuresIds.NotifyMicroService, data
            => Provider.GetRequiredService<IMicroServiceCommunicationStructure>().SendToMicroService(data));
    }
}

