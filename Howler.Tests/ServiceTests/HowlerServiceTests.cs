using System.Reflection;
using AutoMapper;
using HowlerExamples.Database;
using HowlerExamples.Profiles;
using HowlerExamples.Services;
using HowlerExamples.Services.Repositories;
using HowlerExamples.Validators;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using Xunit;

namespace Howler.Tests.ServiceTests;

public class HowlerServiceTests
{
    private readonly ServiceUsingHowler _service;

    public HowlerServiceTests()
    {
        var howler = new InTestHowler();
        var mapper = new Mapper(new MapperConfiguration(x => x.AddMaps(Assembly.GetAssembly(typeof(PersonProfile)))));
        var personRepository = new Proxy<IBaseRepository<Person>>();
        _service = new ServiceUsingHowler(howler, mapper, personRepository.Instance);
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