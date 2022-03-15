using ExamplesCore.CrossCuttingConcerns;
using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace ExamplesCore.Structures;

public class HttpStructure : IHttpStructure
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;

    public HttpStructure(IFakeLogger logger, IHttpContextAccessor accessor, IAuthProvider authProvider)
    {
        _logger = logger;
        _accessor = accessor;
        _authProvider = authProvider;
    }

    public IControllerResponse? GetStructure(Func<IControllerResponse?> method)
    {
        _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            var result = method.Invoke();

            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }

    public void PostStructure(Action method, object data)
    {
        _logger.Log($"received successfully from IHowlerData {data.ToJson()}");

        _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            method.Invoke();

            _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }
    public async Task<IControllerResponse?> PostNotifiableStructure(Func<Task<IControllerResponse?>> method,
        DtoNotifiable data)
    {
        _logger.Log($"received successfully {data.ToJson()}");
        _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            var result = await method.Invoke();

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
