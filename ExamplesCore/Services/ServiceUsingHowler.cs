using AutoMapper;
using ExamplesCore.Database;
using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Services.Repositories;
using ExamplesCore.Structures.Base;
using ExamplesCore.Structures.StructureDtos;
using ExamplesCore.Validators;
using Howler;
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


    public async Task<IControllerResponse> PostDataAndNotify(DtoNotifiable dto)
    {
        await _howler.Transmit<IValidationStructureData, Task>(new ValidationStructureData<DtoNotifiable, DtoNotifiableValidator>(dto), StructuresIds.Validate);

        var entity = _mapper.Map<Person>(dto);
        entity = await _repository.AddAndSaveAsync(entity);
        entity.ThrowIfNull();

        var message = new MicroServiceCommunicationStructureData(entity, HttpMethod.Post, new Uri("https://localhost:7060/Example/Post"));
        
        var result = await _howler.Transmit<MicroServiceCommunicationStructureData, Task<MicroServiceResult>>(message, StructuresIds.NotifyMicroService) ;

        _howler.Transmit(new EmailDto(dto.Email, "This is how you send an email with Je ne sais quoi!", "Sending notifications"), StructuresIds.SendEmail);
        _howler.Transmit(new SmsDto(dto.PhoneNumber, "This is how you send a text with aplomb!"), StructuresIds.SendSms);

        return new PostRequestResponse<string>(result.Response ?? string.Empty);
    }
}
