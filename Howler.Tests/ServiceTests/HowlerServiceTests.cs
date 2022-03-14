using AutoMapper;
using ExamplesCore.Database;
using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Profiles;
using ExamplesCore.Services;
using ExamplesCore.Services.Repositories;
using ExamplesCore.Structures.Base;
using ExamplesCore.Structures.StructureDtos;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Howler.Tests.ServiceTests;

public class HowlerServiceTests
{
    private readonly ServiceUsingHowler _service;
    private readonly InTestHowler _howler;
    private readonly Proxy<IBaseRepository<Person>> _personRepository;
    private readonly Mapper _mapper;

    public HowlerServiceTests()
    {
        _howler = new InTestHowler();
        _mapper = new Mapper(new MapperConfiguration(x => x.AddMaps(Assembly.GetAssembly(typeof(PersonProfile)))));
        _personRepository = new Proxy<IBaseRepository<Person>>();
        _service = new ServiceUsingHowler(_howler, _mapper, _personRepository.Instance);
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
    
    [Fact]
    public async Task TestPostDataAndNotify()
    {
        var dto = new DtoNotifiable("Mirko", "Sangrigoli", 29, "hello@hello.com", "0123456789");
        var personId = Guid.NewGuid();
        var counter = 0;

        _howler.RegisterVoid(() => counter++, StructuresIds.Validate);
        _howler.RegisterVoid(() => counter++, StructuresIds.SendEmail);
        _howler.RegisterVoid(() => counter++, StructuresIds.SendSms);
        _howler.Register<MicroServiceCommunicationStructureData, Task<MicroServiceResult>>(
            x => Task.FromResult(new MicroServiceResult{Response = x.ToJson()}) ,StructuresIds.NotifyMicroService);

        _personRepository.Setup(x => x.AddAndSaveAsync(A<Person>.Value), invocation =>
        {
            var person = invocation.Arguments[0] as Person;
            person.Id = personId;

            return Task.FromResult(person);
        });

        var service = new ServiceUsingHowler(_howler, _mapper, _personRepository.Instance);

        var result = await service.PostDataAndNotify(dto);

        Assert.NotEmpty(result);
        Assert.Equal(3, counter);
    }
}