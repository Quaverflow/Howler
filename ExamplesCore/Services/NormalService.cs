using System.Text;
using AutoMapper;
using ExamplesCore.CrossCuttingConcerns;
using ExamplesCore.Database;
using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Services.Repositories;
using ExamplesCore.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Utilities;

namespace ExamplesCore.Services;

public class NormalService : INormalService
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;
    private readonly IFakeSmsSender _smsSender;
    private readonly IFakeEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IBaseRepository<Person> _repository;
    public NormalService(IHttpContextAccessor accessor, IFakeLogger logger, IAuthProvider authProvider,
        IFakeSmsSender smsSender, IFakeEmailSender emailSender, IMapper mapper, IBaseRepository<Person> repository,
        IHttpClientFactory httpClientFactory)
    {
        _accessor = accessor;
        _logger = logger;
        _authProvider = authProvider;
        _smsSender = smsSender;
        _emailSender = emailSender;
        _mapper = mapper;
        _repository = repository;
        _httpClientFactory = httpClientFactory;
    }

    public string GetData()
    {
        _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            var result = "Hello!";

            _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }


    public async Task<string> PostDataAndNotify(DtoNotifiable dto)
    {
        _logger.Log($"received successfully {dto.ToJson()}");
        _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            var validator = new DtoNotifiableValidator();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entity = _mapper.Map<Person>(dto);
            entity = await _repository.AddAndSaveAsync(entity);
            entity.ThrowIfNull();

            _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");

            try
            {
                _logger.Log("Begin notifying MicroService");

                var response = await _httpClientFactory.CreateClient().PostAsync("https://localhost:7060/Example/Post", new StringContent(entity.ToJson(), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                _logger.Log("MicroService notified");
                var result = JsonConvert.DeserializeObject<MicroServiceResult>(await response.Content.ReadAsStringAsync());

                _emailSender.Send(new EmailDto(dto.Email, "This is how you send an email with Je ne sais quoi!", "Sending notifications"));
                _smsSender.Send(new SmsDto(dto.PhoneNumber, "This is how you send a text with aplomb!"));
                return result?.Result ?? string.Empty;
            }
            catch (Exception e)
            {
                _logger.Log($"MicroService notification failed with error {e.Message}");
                throw;
            }
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }
}