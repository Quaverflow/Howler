using System;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Profiles;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Implementations;
using ExamplesForWiseUp.Structures;
using ExamplesForWiseUp.Structures.HttpStructures;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using MonkeyPatcher.MonkeyPatch.Shared;
using Xunit;

namespace Howler.Tests;

public class HowlerTests
{
    private readonly ExampleService _service;
    private bool _smsCalled;
    private bool _emailCalled;
    private bool _microServiceCalled;

    public HowlerTests()
    {
        var howler = new Proxy<IHowler>();
        howler.Setup(x => x.TransmitVoidAsync(Any<Guid>.Value, Any<object?[]?>.Value), inv =>
        {
            var id = (Guid) inv.Arguments[0];
            if (id == StructureIds.SendEmail)
            {
                _emailCalled = true;
            }
            else if (id == StructureIds.SendSms)
            {
                _smsCalled = true;
            }
            else if (id == StructureIds.NotifyMicroService)
            {
                _microServiceCalled = true;
            }

            return Task.CompletedTask;
        });

        var mapper = new MapperConfiguration(x => x.AddMaps(Assembly.GetAssembly(typeof(PersonProfile)))).CreateMapper();

        var repository = new Proxy<IBaseRepository<Person>>();
        repository.Setup(x => x.AddAndSaveAsync(Any<Person>.Value), inv => Task.FromResult((Person)inv.Arguments[0]));

        _service = new ExampleService(repository.Instance, mapper, howler.Instance);
    }

    [Fact]
    public async Task Succeed()
    {
        var data = new Dto("Mary", "Joseph", 32, "abc@gma.com", "12345");
        var result = await _service.SavePerson(data);

        Assert.True(_microServiceCalled);
        Assert.True(_emailCalled);
        Assert.True(_smsCalled);
        var instance = Assert.IsType<PostResponseDto<Dto>>(result);
        Assert.Equal(data.ToJson(), instance.Data.ToJson());

    }
}