using Howler;
using Howler.Tests.Objects.StructureExamples;
using HowlerExamples.Services;
using Microsoft.AspNetCore.Http.Extensions;

namespace HowlerExamples.Structures;

public class InjectedStructure : IHowlerStructureBuilder
{
    private readonly IServiceProvider _provider;

    public InjectedStructure(IServiceProvider provider)
    {
        _provider = provider;
    }

    private void RegisterGetStructure()
    {
        HowlerRegistration.AddStructure(StructuresIds.GetStructureId, x =>
        {
           var logger = _provider.GetService<IFakeLogger>();
           var accessor = _provider.GetService<IHttpContextAccessor>();
           var authProvider = _provider.GetService<IAuthProvider>();

            logger.Log($"The service call to {accessor.HttpContext?.Request.GetDisplayUrl()} has started");
            try
            {
                authProvider.HasAccess(true);

                var result = x.DynamicInvoke();

                logger.Log($"The service call to {accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
                return result;
            }
            catch (Exception e)
            {
                logger.Log($"The service  call to {accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
                throw;
            }
        });
    }

    private void RegisterPostStructure()
    {
        HowlerRegistration.AddStructure(StructuresIds.PostStructureId, x =>
        {
            var logger = _provider.GetService<IFakeLogger>();
            var accessor = _provider.GetService<IHttpContextAccessor>();
            var authProvider = _provider.GetService<IAuthProvider>();
            var humanCounter =  HumanCounter.GetSingleton();
            humanCounter.Subscribe(HumanObserverFactory.Observer);

            logger.Log($"The service call to {accessor.HttpContext?.Request.GetDisplayUrl()} has started");
            try
            {
                authProvider.HasAccess(true);

                var result = x.DynamicInvoke();

                logger.Log($"The service call to {accessor.HttpContext?.Request.GetDisplayUrl()} succeeded");
                return result;
            }
            catch (Exception e)
            {
                logger.Log(
                    $"The service  call to {accessor.HttpContext?.Request.GetDisplayUrl()} failed with exception {e.Message}");
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