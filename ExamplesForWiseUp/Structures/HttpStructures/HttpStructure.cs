using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using Howler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace ExamplesForWiseUp.Structures.HttpStructures;

public class HttpStructure : IHowlerStructure
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;

    public HttpStructure(IFakeLogger logger, IAuthProvider authProvider, IHttpContextAccessor accessor)
    {
        _logger = logger;
        _authProvider = authProvider;
        _accessor = accessor;
    }

    public async Task<IHttpStructureDto> OnGet(Func<Task<IHttpStructureDto>> invocation)
    {
        _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            var result = await invocation.Invoke();

            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }

    public async Task<IHttpStructureDto> OnPostAsync(Func<Task<IHttpStructureDto>> invocation, Guid userId)
    {
        _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} has started");
        try
        {
            await _authProvider.HasAccess(userId);
            
            var result = await invocation.Invoke();
            
            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }

    public void InvokeRegistrations()
    {
        HowlerRegistry.AddStructure(StructureIds.Post, OnPostAsync);
        HowlerRegistry.AddStructure(StructureIds.Get, OnGet);
    }
}