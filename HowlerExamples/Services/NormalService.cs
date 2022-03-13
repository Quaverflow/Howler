using AutoMapper;
using HowlerExamples.CrossCuttingConcerns;
using HowlerExamples.Database;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Services.Repositories;
using Microsoft.AspNetCore.Http.Extensions;
using Utilities;

namespace HowlerExamples.Services;

public class NormalService : INormalService
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;
    private readonly IFakeSmsSender _smsSender;
    private readonly IFakeEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Person> _repository;
    public NormalService(IHttpContextAccessor accessor, IFakeLogger logger, IAuthProvider authProvider, IFakeSmsSender smsSender, IFakeEmailSender emailSender, IMapper mapper, IBaseRepository<Person> repository)
    {
        _accessor = accessor;
        _logger = logger;
        _authProvider = authProvider;
        _smsSender = smsSender;
        _emailSender = emailSender;
        _mapper = mapper;
        _repository = repository;
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

            var entity = _mapper.Map<Person>(dto);
            entity = await _repository.AddAndSaveAsync(entity);
            entity.ThrowIfNull();

            _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");

            _emailSender.Send(new EmailDto(dto.Email, "This is how you send an email with Je ne sais quoi!", "Sending notifications"));
            _smsSender.Send(new SmsDto(dto.PhoneNumber, "This is how you send a text with aplomb!"));

            return entity.ToJson();
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }
}