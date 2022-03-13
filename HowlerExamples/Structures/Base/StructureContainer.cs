using Howler;
using HowlerExamples.Models;
using HowlerExamples.Structures.StructureDtos;

namespace HowlerExamples.Structures.Base;

public class StructureContainer : IHowlerStructureBuilder
{
    private readonly IHttpStructure _httpStructureContainer;
    private readonly INotificationStructure _notificationStructure;
    private readonly IValidationStructure _validationStructure;


    public StructureContainer(INotificationStructure notificationStructure, IValidationStructure validationStructure, IHttpStructure httpStructureContainer)
    {
        _notificationStructure = notificationStructure;
        _validationStructure = validationStructure;
        _httpStructureContainer = httpStructureContainer;
    }

    public void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.Get, method => _httpStructureContainer.GetStructure(method));
        HowlerRegistration.AddStructure<Dto>(StructuresIds.Post, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration.AddStructure<DtoNotifiable>(StructuresIds.PostAndNotify, (method, data) => _httpStructureContainer.PostStructure(method, data));
        HowlerRegistration.AddDataTransferStructure<EmailDto>(StructuresIds.SendEmail, data => _notificationStructure.SendEmail(data));
        HowlerRegistration.AddDataTransferStructure<SmsDto>(StructuresIds.SendSms, data => _notificationStructure.SendSms(data));
        HowlerRegistration.AddDataTransferStructure<IValidationStructureData>(StructuresIds.Validate, data => _validationStructure.Validate(data));
    }
}