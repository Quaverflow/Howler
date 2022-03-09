using Howler;
using Howler.Tests.Objects.StructureExamples;
using HowlerExamples.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Utilities;

namespace HowlerExamples.Structures;

public class HttpStructures : IHowlerStructureBuilder
{
    private readonly IHttpStructureContainer _httpStructureContainer;


    public HttpStructures(IServiceProvider provider)
    {
        _httpStructureContainer = provider.GetRequiredService<IHttpStructureContainer>();
    }

    public void InvokeRegistrations()
    {
        HowlerRegistration.AddStructure(StructuresIds.GetStructureId, x => _httpStructureContainer.GetStructure(x));
        HowlerRegistration.AddStructure(StructuresIds.PostStructureId, x => _httpStructureContainer.PostStructure(x));
    }
}

public class HttpStructureContainer : IHttpStructureContainer
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;

    public HttpStructureContainer(IFakeLogger logger, IHttpContextAccessor accessor, IAuthProvider authProvider)
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

    public object? PostStructure(Delegate method)
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
}
public interface IHttpStructureContainer
{
    object? GetStructure(Delegate method);
    object? PostStructure(Delegate method);
}