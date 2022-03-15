using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Structures.Dtos;
using ExamplesForWiseUp.Structures.Interfaces;
using Howler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace ExamplesForWiseUp.Structures.Implementations;

public class HttpStructure : HowlerStructureBuilder, IHttpStructure
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IHowlerRegistry _registry;
    private readonly IAuthProvider _authProvider;

    public HttpStructure(IFakeLogger logger, IAuthProvider authProvider, IHttpContextAccessor accessor, IHowlerRegistry registry)
    {
        _logger = logger;
        _authProvider = authProvider;
        _accessor = accessor;
        _registry = registry;
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
            _logger.Log($"The service  call to {_accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
            throw;
        };
    }

    public override void InvokeRegistrations()
    {
        _registry.AddStructure(StructureIds.Post, OnPostAsync);
    }
}