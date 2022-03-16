using System;
using System.Threading.Tasks;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Services.Interfaces;
using ExamplesForWiseUp.Structures.HttpStructures;
using HowlerExamples.Controllers;
using Microsoft.AspNetCore.Mvc;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using MonkeyPatcher.MonkeyPatch.Shared;
using Xunit;

namespace Howler.Tests;

public class ExampleControllerTests
{
    private readonly PostResponseDto<Dto> _dto = new(new Dto("Mary", "Joseph", 32, "abc@gma.com", "12345"));

    private readonly ExampleController _controller;

    public ExampleControllerTests()
    {
        var howler = new Proxy<IHowler>();
        howler.Setup(
            x => x.InvokeAsync(Any<Guid>.Value, Any<Func<Task<IHttpStructureDto>>>.Value, Any<object?[]?>.Value),
            () => Task.FromResult(_dto as IHttpStructureDto));

        _controller = new ExampleController(howler.Instance, new Proxy<IExampleService>().Instance);
    }

    [Fact]
    public async Task ControllerShouldWork()
    {
        var result = await _controller.SavePerson(_dto.Data);
        Assert.NotNull(result);

        var okResult = result as OkObjectResult; 
        Assert.NotNull(okResult);
        Assert.NotNull(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);

        Assert.Contains("Mary", okResult.Value.ToString());
    }
}