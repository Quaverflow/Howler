using AutoMapper;
using ExamplesCore.Database;
using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Services.Repositories;
using ExamplesCore.Structures.Base;
using ExamplesCore.Structures.StructureDtos;
using ExamplesCore.Validators;
using Howler;
using Newtonsoft.Json;
using Utilities;

namespace ExamplesCore.Services;

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
        await _howler.Invoke<IValidationStructureData, Task>(new ValidationStructureData<DtoNotifiable, DtoNotifiableValidator>(dto), StructuresIds.Validate);

        var entity = _mapper.Map<Person>(dto);
        entity = await _repository.AddAndSaveAsync(entity);
        entity.ThrowIfNull();

        var message = new MicroServiceCommunicationStructureData(entity, HttpMethod.Post, new Uri("https://localhost:7060/Example/Post"));
        var response = await _howler.Invoke<MicroServiceCommunicationStructureData, Task<HttpResponseMessage>>(message, StructuresIds.NotifyMicroService);
        var result = JsonConvert.DeserializeObject<MicroServiceResult>(await response.Content.ReadAsStringAsync());

        _howler.InvokeVoid(new EmailDto(dto.Email, "This is how you send an email with Je ne sais quoi!", "Sending notifications"), StructuresIds.SendEmail);
        _howler.InvokeVoid(new SmsDto(dto.PhoneNumber, "This is how you send a text with aplomb!"), StructuresIds.SendSms);

        return result?.Result ?? string.Empty;
    }
}
