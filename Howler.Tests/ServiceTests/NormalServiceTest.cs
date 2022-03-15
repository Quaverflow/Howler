//using AutoMapper;
//using ExamplesCore.CrossCuttingConcerns;
//using ExamplesCore.Database;
//using ExamplesCore.Services;
//using ExamplesCore.Services.Repositories;
//using Microsoft.AspNetCore.Http;
//using MonkeyPatcher.MonkeyPatch.Interfaces;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Reflection;
//using ExamplesCore.Profiles;
//using Xunit;

//namespace Howler.Tests.ServiceTests;

//public class NormalServiceTest
//{
//    private readonly List<string> _generatedLogs = new ();
//    private readonly NormalService _service;

//    public NormalServiceTest()
//    {
//        var logger = new Proxy<IFakeLogger>();
//        logger.SetupVoid(x => x.Log(A<string>.Value), inv => _generatedLogs.Add((string) inv.Arguments[0]));

//        var auth = new Proxy<IAuthProvider>();
//        auth.Setup(x => x.HasAccess(A<bool>.Value), ()=> true);

//        var httpContextAccessor = new Proxy<IHttpContextAccessor>();
//        httpContextAccessor.Setup(x => x.HttpContext, () => null);

//        var emailSender = new Proxy<IFakeEmailSender>();
//        var smsSender = new Proxy<IFakeSmsSender>();
//        var httpClient = new Proxy<IHttpClientFactory>();
//        var mapper = new Mapper(new MapperConfiguration(x => x.AddMaps(Assembly.GetAssembly(typeof(PersonProfile)))));
//        var personRepository = new Proxy<IBaseRepository<Person>>();
//        _service = new NormalService(httpContextAccessor.Instance, logger.Instance, auth.Instance, smsSender.Instance,
//            emailSender.Instance, mapper, personRepository.Instance, httpClient.Instance);
//    }

//    [Fact]
//    public void TestRuns()
//    {
//        var result = _service.GetData();
//        Assert.Equal("Hello!", result);
//    }

//    [Fact]
//    public void TestLogger()
//    {
//        _service.GetData();
//        Assert.Equal(2, _generatedLogs.Count);
//    }

//}