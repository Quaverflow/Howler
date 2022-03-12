using HowlerExamples.Services;
using Xunit;

namespace Howler.Tests.ServiceTests;

public class HowlerServiceTests
{
    private readonly ServiceUsingHowler _service;

    public HowlerServiceTests()
    {
        _service = new ServiceUsingHowler();
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