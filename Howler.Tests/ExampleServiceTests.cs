using AutoMapper;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Profiles;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Implementations;
using ExamplesForWiseUp.Structures;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using MonkeyPatcher.MonkeyPatch.Shared;
using System;
using System.Reflection;
using System.Threading.Tasks;
using ExamplesForWiseUp.Structures.HttpStructures;
using MonkeyPatcher.MonkeyPatch.Concrete;
using Newtonsoft.Json;
using Xunit;

namespace Howler.Tests;

public class ExampleServiceTests
{
    private readonly ExampleService _service;

    public ExampleServiceTests()
    {
        var howler = new Proxy<IHowler>();
        howler.Setup(x => x.InvokeVoidAsync(Any<Guid>.Value, Any<Func<Task>>.Value, Any<string>.Value),
            inv =>
            {
                var method = inv.Arguments[1] as Func<Task>;
                Assert.NotNull(method);
                return method.DynamicInvoke();
            });

        howler.Setup(x => x.TransmitVoidAsync(Any<Guid>.Value, Any<object?[]?>.Value), ()=> Task.CompletedTask);

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

        var instance = Assert.IsType<PostResponseDto<Dto>>(result);
        Assert.Equal(data.ToJson(), instance.Data.ToJson());

    }
}
