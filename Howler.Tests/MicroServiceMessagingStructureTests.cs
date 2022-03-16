using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Structures.MicroServiceMessaging;
using MonkeyPatcher.MonkeyPatch.Concrete;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using MonkeyPatcher.MonkeyPatch.Shared;
using Xunit;

namespace Howler.Tests;

public class MicroServiceMessagingStructureTests
{
    private readonly MicroServiceMessagingStructure _service;
    private readonly List<string> _fakeLogs = new();
    public MicroServiceMessagingStructureTests()
    {
        var logger = new Proxy<IFakeLogger>();
        logger.SetupVoid(x => x.Log(Any<string>.Value),
            inv => _fakeLogs.Add((string)inv.Arguments[0]));

        var clientFactory = new Proxy<IHttpClientFactory>();
        clientFactory.Setup(x => x.CreateClient(Any<string>.Value), () => new HttpClient());

        _service = new MicroServiceMessagingStructure(clientFactory.Instance, logger.Instance);
    }

    [Fact]
    public async Task ShouldSucceedAndLog()
    {
        using var mp = MonkeyPatcherFactory.GetMonkeyPatch(_service.MessageMicroService);

        var outgoingRequestStarted = false;
        mp.Override<HttpClient, Task<HttpResponseMessage>>(x => x.SendAsync(Any<HttpRequestMessage>.Value),
            () =>
            {
                outgoingRequestStarted = true;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted));
            });

        var person = new Person();
        var message = new MicroserviceMessage("https://localhost:7060/Example/Post", HttpMethod.Post, person);
        await _service.MessageMicroService(message);
        Assert.True(_fakeLogs.Count == 2);
        Assert.Equal("Messaging the Micro Service", _fakeLogs[0]);
        Assert.Equal("Micro service responded correctly", _fakeLogs[1]);
        Assert.True(outgoingRequestStarted);
    }

    [Fact]
    public async Task ShouldFailsAndLog()
    {
        using var mp = MonkeyPatcherFactory.GetMonkeyPatch(_service.MessageMicroService);
        mp.Override<HttpClient, Task<HttpResponseMessage>>(x => x.SendAsync(Any<HttpRequestMessage>.Value),
            () => throw new Exception("ups!"));

        var person = new Person();
        var message = new MicroserviceMessage("https://localhost:7060/Example/Post", HttpMethod.Post, person);
        await _service.MessageMicroService(message);
        Assert.True(_fakeLogs.Count == 2);
        Assert.Equal("Messaging the Micro Service", _fakeLogs[0]);
        Assert.Equal("Micro service failed to response with exception ups!", _fakeLogs[1]);
    }
}