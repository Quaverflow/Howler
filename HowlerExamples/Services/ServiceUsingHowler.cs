using AutoMapper;
using Howler;
using HowlerExamples.Database;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Services.Repositories;
using HowlerExamples.Structures.Base;
using HowlerExamples.Structures.StructureDtos;
using HowlerExamples.Validators;
using Utilities;

namespace HowlerExamples.Services;

public class ServiceUsingHowler : IServiceUsingHowler
{
    private readonly IHowler _howler;
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Person> _repository;

    public ServiceUsingHowler(IHowler howler, IMapper mapper, IBaseRepository<Person> repository)
    {
        _howler = howler;
        _mapper = mapper;
        _repository = repository;
    }
    public string GetData() => "Hello!";
    public string GetMoreData() => "GoodBye!";
    public void PostData(Dto dto) => dto.ToJson();


    public async Task<string> PostDataAndNotify(DtoNotifiable dto)
    {
        _howler.InvokeVoid(new ValidationStructureData<DtoNotifiable, DtoNotifiableValidator>(dto), StructuresIds.Validate);

        var entity = _mapper.Map<Person>(dto);
        entity = await _repository.AddAndSaveAsync(entity);
        entity.ThrowIfNull();

        _howler.InvokeVoid(new EmailDto(dto.Email, "This is how you send an email with Je ne sais quoi!", "Sending notifications"), StructuresIds.SendEmail);
        _howler.InvokeVoid(new SmsDto(dto.PhoneNumber, "This is how you send a text with aplomb!"), StructuresIds.SendSms);

        return entity.ToJson();
    }
}
