using HowlerExamples.Services;
using Microsoft.AspNetCore.Http;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using System.Collections.Generic;
using HowlerExamples.CrossCuttingConcerns;
using Xunit;

namespace Howler.Tests.ServiceTests;

public class NormalServiceTest
{
    private readonly List<string> _generatedLogs = new ();
    private readonly NormalService _service;

    public NormalServiceTest()
    {
        var logger = new Proxy<IFakeLogger>();
        logger.SetupVoid(x => x.Log(A<string>.Value), inv => _generatedLogs.Add((string) inv.Arguments[0]));

        var auth = new Proxy<IAuthProvider>();
        auth.Setup(x => x.HasAccess(A<bool>.Value), ()=> true);

        var httpContextAccessor = new Proxy<IHttpContextAccessor>();
        httpContextAccessor.Setup(x => x.HttpContext, () => null);

        _service = new NormalService(httpContextAccessor.Instance, logger.Instance, auth.Instance);
    }

    [Fact]
    public void TestRuns()
    {
        var result = _service.GetData();
        Assert.Equal("Hello!", result);
    }

    [Fact]
    public void TestLogger()
    {
        _service.GetData();
        Assert.Equal(2, _generatedLogs.Count);
    }

}

public class HowlerServiceTests
{
    private readonly ServiceUsingHowler _service;

    public HowlerServiceTests()
    {
        var howler = new InTestHowler();
        _service = new ServiceUsingHowler(howler);
    }

    [Fact]
    public void TestGetData()
    {
        var result = _service.GetData();
        Assert.Equal("Hello!", result);
    } 
    
    [Fact]
    public void TestGetMoreData()
    {
        var result = _service.GetMoreData();
        Assert.Equal("GoodBye!", result);
    }
}

public class HttpStructuresTests
{
    public HttpStructuresTests()
    {
        
        
    }
}