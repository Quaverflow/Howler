using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;
using Howler;

namespace ExamplesCore.Structures.Base;

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