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
        HowlerRegistration.AddStructure(StructuresIds.GetStructureId, method => _httpStructureContainer.GetStructure(method));
        HowlerRegistration<Dto>.AddStructure(StructuresIds.PostStructureId, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration<object>.AddStructure(StructuresIds.PostStructureId, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration<EmailDto>.AddDataTransferVoidStructure(StructuresIds.SendEmailId, data => _notificationStructure.SendEmail(data));
        HowlerRegistration<SmsDto>.AddDataTransferVoidStructure(StructuresIds.SendSmsId, data => _notificationStructure.SendSms(data));
        HowlerRegistration<NotificationDto>.AddDataTransferVoidStructure(StructuresIds.SendEmailAndSmsId, data => _notificationStructure.SendNotification(data));
    }
}