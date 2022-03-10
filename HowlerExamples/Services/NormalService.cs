using HowlerExamples.CrossCuttingConcerns;
using Microsoft.AspNetCore.Http.Extensions;

namespace HowlerExamples.Services;

public class NormalService : INormalService
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;

    public NormalService(IHttpContextAccessor accessor, IFakeLogger logger, IAuthProvider authProvider)
    {
        _accessor = accessor;
        _logger = logger;
        _authProvider = authProvider;
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
}