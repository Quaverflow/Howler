using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Structures.HttpStructures;
using Microsoft.AspNetCore.Http;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using MonkeyPatcher.MonkeyPatch.Shared;
using Utilities;
using Xunit;

namespace Howler.Tests;

public class TestHttpStructure
{
    private readonly List<string> _fakeLogs = new();
    private readonly HttpStructure _httpStructure;

    public TestHttpStructure()
    {
        FakesRepository.Cleanup();
        var logger = new Proxy<IFakeLogger>();
        logger.SetupVoid(x => x.Log(Any<string>.Value),
            inv => _fakeLogs.Add((string)inv.Arguments[0]));

        var accessor = new Proxy<IHttpContextAccessor>();
        accessor.Setup(x => x.HttpContext, () => new DefaultHttpContext());

        var auth = new Proxy<IAuthProvider>();
        auth.Setup(x => x.HasAccess(Any<Guid>.Value),
            inv =>
            {
                ((Guid)inv.Arguments[0] == ExampleDbContext.AuthorizedPersonId).ThrowIfAssumptionFailed();
                return Task.CompletedTask;
            });

        _httpStructure = new HttpStructure(logger.Instance, auth.Instance, accessor.Instance);
    }

    [Fact]
    public async Task PostStructureShould_SucceedAndLog()
    {
        var data = new PostResponseDto<Dto>(new Dto("Mary", "Joseph", 32, "abc@gma.com", "12345")) as IHttpStructureDto;
        var result = await _httpStructure.OnPostAsync(() => Task.FromResult(data), ExampleDbContext.AuthorizedPersonId);
        Assert.True(_fakeLogs.Count == 2);
        Assert.Equal("The service call to :// has started", _fakeLogs[0]);
        Assert.Equal("The service call to :// succeeded", _fakeLogs[1]);
        Assert.Equal(data.ToJson(), result.ToJson());
    }

    [Fact]
    public async Task PostStructureShould_ThrowsAndLog()
    {
        var data = new PostResponseDto<Dto>(new Dto("Mary", "Joseph", 32, "abc@gma.com", "12345")) as IHttpStructureDto;
        var result = await Assert.ThrowsAsync<InvalidOperationException>(() => _httpStructure.OnPostAsync(() => Task.FromResult(data), Guid.Empty));
        Assert.True(_fakeLogs.Count == 2);
        Assert.Equal("The service call to :// has started", _fakeLogs[0]);
        Assert.Equal("The service call to :// failed with exception The assumption was false.", _fakeLogs[1]);
        Assert.Equal("The assumption was false.", result.Message);
    }
}