using Howler;
using HowlerExamples.Models;

namespace HowlerExamples.Structures.Base;

public class StructureContainer : IHowlerStructureBuilder
{
    private readonly IHttpStructure _httpStructureContainer;
    private readonly INotificationStructure _notificationStructure;


    public StructureContainer(IServiceProvider provider, INotificationStructure notificationStructure)
    {
        _notificationStructure = notificationStructure;
        _httpStructureContainer = provider.GetRequiredService<IHttpStructure>();
    }

    public void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.Get, method => _httpStructureContainer.GetStructure(method));
        HowlerRegistration.AddStructure<Dto>(StructuresIds.Post, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration.AddStructure<DtoNotifiable>(StructuresIds.PostAndNotify, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration.AddDataTransferStructure<EmailDto>(StructuresIds.SendEmail, data => _notificationStructure.SendEmail(data));
        HowlerRegistration.AddDataTransferStructure<SmsDto>(StructuresIds.SendSms, data => _notificationStructure.SendSms(data));
    }
}