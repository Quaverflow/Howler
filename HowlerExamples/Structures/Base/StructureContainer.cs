using Howler;
using HowlerExamples.Models;
using HowlerExamples.Structures.StructureDtos;

namespace HowlerExamples.Structures.Base;

public class StructureContainer : HowlerStructureBuilder
{
    public override void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.Get, method 
            => Provider.GetRequiredService<IHttpStructure>().GetStructure(method));
        HowlerRegistration.AddStructure<Dto>(StructuresIds.Post, (method, data)
            => Provider.GetRequiredService<IHttpStructure>().PostStructure(method, data));
        HowlerRegistration.AddStructure<DtoNotifiable>(StructuresIds.PostAndNotify, (method, data)
            => Provider.GetRequiredService<IHttpStructure>().PostStructure(method, data));

        HowlerRegistration.AddDataTransferStructure<EmailDto>(StructuresIds.SendEmail, data 
            => Provider.GetRequiredService<INotificationStructure>().SendEmail(data));
        HowlerRegistration.AddDataTransferStructure<SmsDto>(StructuresIds.SendSms, data 
            => Provider.GetRequiredService<INotificationStructure>().SendSms(data));

        HowlerRegistration.AddDataTransferStructure<IValidationStructureData>(StructuresIds.Validate, data
            => Provider.GetRequiredService<IValidationStructure>().Validate(data));
    }
}