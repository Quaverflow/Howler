using ExamplesCore.CrossCuttingConcerns;
using ExamplesCore.Helpers;
using ExamplesCore.Models;
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

    public object? GetStructure(Delegate method)
    {
        _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            var result = method.DynamicInvoke();

            _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }

    public object? PostStructure(Delegate method, object data)
    {
        _logger.Log($"received successfully from IHowlerData {data.ToJson()}");

        _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            var result = method.DynamicInvoke();

            _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }
    public object? PostStructure(Delegate method, DtoNotifiable data)
    {
        _logger.Log($"received successfully {data.ToJson()}");
        _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            _authProvider.HasAccess(true);

            var result = method.DynamicInvoke();

            _logger.Log($"The service call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service  call to {_accessor?.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }
}
