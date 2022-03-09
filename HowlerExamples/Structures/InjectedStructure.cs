using Howler;
using Howler.Tests.Objects.StructureExamples;
using HowlerExamples.Services;
using Microsoft.AspNetCore.Http.Extensions;

namespace HowlerExamples.Structures;

public class InjectedStructure : IHowlerStructureBuilder
{
    private readonly IFakeLogger _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAuthProvider _authProvider;

    public InjectedStructure(IFakeLogger logger, IHttpContextAccessor accessor, IAuthProvider authProvider)
    {
        _logger = logger;
        _accessor = accessor;
        _authProvider = authProvider;
    }

    private void RegisterGetStructure()
    {
        HowlerRegistration.AddStructure(StructuresIds.GetStructureId, x =>
        {
            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} has started");
            try
            {
                _authProvider.HasAccess(true);

                var result = x.DynamicInvoke();

                _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
                return result;
            }
            catch (Exception e)
            {
                _logger.Log($"The service  call to {_accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
                throw;
            }
        });
    }

    private void RegisterPostStructure()
    {
        HowlerRegistration.AddStructure(StructuresIds.PostStructureId, x =>
        {
            var humanCounter =  HumanCounter.GetSingleton();
            humanCounter.Subscribe(HumanObserverFactory.Observer);

            _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} has started");
            try
            {
                _authProvider.HasAccess(true);

                var result = x.DynamicInvoke();

                _logger.Log($"The service call to {_accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
                return result;
            }
            catch (Exception e)
            {
                _logger.Log(
                    $"The service  call to {_accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
                throw;
            }
            finally
            {

            }
        });

    }



    public void InvokeRegistrations()
    {
        RegisterGetStructure();
        RegisterPostStructure();
    }
}