using System.Net.Http.Json;
using AutoMapper;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Structures.HttpStructures;
using ExamplesForWiseUp.Structures.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace ExamplesForWiseUp.Services.Implementations;

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


            if (person.Name.Equals("Chris", StringComparison.InvariantCultureIgnoreCase))
            {
                var email = new EmailDto(person.Email, "hello my friend!", "feeling nice and cute");
                await _emailSender.Send(email);

                var sms = new SmsDto(person.PhoneNumber, "Beautiful Day!");
                await _smsSender.Send(sms);

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