﻿using Howler;
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
        HowlerRegistration.AddStructure<Dto>(StructuresIds.PostStructureId, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration.AddStructure<DtoNotifiable>(StructuresIds.PostAndNotifyStructureId, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration.AddDataTransferVoidStructure<EmailDto>(StructuresIds.SendEmailId, data => _notificationStructure.SendEmail(data));
        HowlerRegistration.AddDataTransferVoidStructure<SmsDto>(StructuresIds.SendSmsId, data => _notificationStructure.SendSms(data));
        HowlerRegistration.AddDataTransferVoidStructure<NotificationDto>(StructuresIds.SendEmailAndSmsId, data => _notificationStructure.SendNotification(data));
    }
}