using AutoMapper;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Interfaces;
using ExamplesForWiseUp.Structures;
using Howler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Json;
using ExamplesForWiseUp.Structures.HttpStructures;
using ExamplesForWiseUp.Structures.MicroServiceMessaging;
using ExamplesForWiseUp.Structures.Notifications;

namespace ExamplesForWiseUp.Services.Implementations;

#region ServiceWithHowler

public class ExampleService : IExampleService
{
    private readonly IBaseRepository<Person> _personRepository;
    private readonly IHowler _howler;
    private readonly IMapper _mapper;
    public ExampleService(IBaseRepository<Person> personRepository, IMapper mapper, IHowler howler)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _howler = howler;
    }

    public async Task<IHttpStructureDto> SavePerson(Dto dto)
    {
        var person = _mapper.Map<Person>(dto);

        await _personRepository.AddAndSaveAsync(person);

        await _howler.TransmitVoidAsync(StructureIds.SendEmail, new EmailDto(person.Email, "hello my friend!", "feeling nice and cute"));
        await _howler.TransmitVoidAsync(StructureIds.SendSms, new SmsDto(person.PhoneNumber, "Beautiful Day!"));

        var message = new MicroserviceMessage("https://localhost:7060/Example/Post", HttpMethod.Post, person);
        await _howler.TransmitVoidAsync(StructureIds.NotifyMicroService, message);
        
        return new PostResponseDto<Dto>(_mapper.Map<Dto>(person));
    }
}

#endregion

#region Service without Howler, for comparisson
public class ServiceWithoutHowler
{
    private readonly HttpClient _client;
    private readonly IFakeSmsSender _smsSender;
    private readonly IFakeEmailSender _emailSender;
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;
    private readonly IBaseRepository<Person> _personRepository;
    private readonly IMapper _mapper;

    public ServiceWithoutHowler(IFakeSmsSender smsSender, IFakeEmailSender emailSender, IFakeLogger logger,
        IHttpContextAccessor accessor, IAuthProvider authProvider, IBaseRepository<Person> personRepository,
        IMapper mapper, IHttpClientFactory httpClientFactory)
    {
        _smsSender = smsSender;
        _emailSender = emailSender;
        _logger = logger;
        _accessor = accessor;
        _authProvider = authProvider;
        _personRepository = personRepository;
        _mapper = mapper;
        _client = httpClientFactory.CreateClient();
    }

    public async Task<IHttpStructureDto> NormalSavePerson(Dto dto)
    {
        try
        {
            await _authProvider.HasAccess(ExampleDbContext.AuthorizedPersonId);

            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} has started");

            var person = _mapper.Map<Person>(dto);
            await _personRepository.AddAndSaveAsync(person);
            var result = new PostResponseDto<Dto>(_mapper.Map<Dto>(person));
           
            await _emailSender.Send(new EmailDto(person.Email, "hello my friend!", "feeling nice and cute"));
            await _smsSender.Send(new SmsDto(person.PhoneNumber, "Beautiful Day!"));

            try
            {
                _logger.Log("Messaging the Micro Service");

                var response = await _client.PostAsJsonAsync("https://localhost:7060/Example/Post", dto.ToJson());
                response.EnsureSuccessStatusCode();

                _logger.Log("Micro service responded correctly");

            }
            catch (Exception e)
            {
                _logger.Log($"Micro service failed to response with exception{e.Message}");
            }

            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }
}
#endregion