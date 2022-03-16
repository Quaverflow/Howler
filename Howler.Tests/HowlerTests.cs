using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Structures.HttpStructures;
using Microsoft.AspNetCore.Http;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using MonkeyPatcher.MonkeyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExamplesForWiseUp.Structures.MicroServiceMessaging;
using MonkeyPatcher.MonkeyPatch.Concrete;
using Utilities;
using Xunit;

namespace Howler.Tests
{
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
                ()=>
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
    }   
    
    public class HowlerTests
    {

    }

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
            var result = await Assert.ThrowsAsync<InvalidOperationException>(()=> _httpStructure.OnPostAsync(() => Task.FromResult(data), Guid.Empty));
            Assert.True(_fakeLogs.Count == 2);
            Assert.Equal("The service call to :// has started", _fakeLogs[0]);
            Assert.Equal("The service call to :// failed with exception The assumption was false.", _fakeLogs[1]);
            Assert.Equal("The assumption was false.", result.Message);
        }
    }
}