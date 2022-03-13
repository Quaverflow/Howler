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
        //_logger.Log($"received successfully {dto.ToJson()}");
        //_logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} has started");
        //try
        //{
        //    _authProvider.HasAccess(true);

        //  var validator = new DtoNotifiableValidator();
        //  var validationResult = await validator.ValidateAsync(dto);
        //  if (!validationResult.IsValid)
        //  {
        //      throw new ValidationException(validationResult.Errors);
        //  }
        await _howler.Invoke<IValidationStructureData, Task>(new ValidationStructureData<DtoNotifiable, DtoNotifiableValidator>(dto), StructuresIds.Validate);

        var entity = _mapper.Map<Person>(dto);
        entity = await _repository.AddAndSaveAsync(entity);
        entity.ThrowIfNull();


        //  _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");

        //  try
        //  {
        //      _logger.Log("Begin notifying MicroService");

        var message = new MicroServiceCommunicationStructureData(entity, HttpMethod.Post, new Uri("https://localhost:7060/Example/Post"));
        var response = await _howler.Invoke<MicroServiceCommunicationStructureData, Task<HttpResponseMessage>>(message, StructuresIds.NotifyMicroService);
        var result = JsonConvert.DeserializeObject<MicroServiceResult>(await response.Content.ReadAsStringAsync());

        _howler.InvokeVoid(new EmailDto(dto.Email, "This is how you send an email with Je ne sais quoi!", "Sending notifications"), StructuresIds.SendEmail);
        _howler.InvokeVoid(new SmsDto(dto.PhoneNumber, "This is how you send a text with aplomb!"), StructuresIds.SendSms);

        return result?.Result ?? string.Empty;

        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Log($"MicroService notification failed with error {e.Message}");
        //        throw;
        //    }
        //}
        //catch (Exception e)
        //{
        //    _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
        //    throw;
        //};
    }
}
