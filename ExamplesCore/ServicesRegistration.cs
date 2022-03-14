using ExamplesCore.CrossCuttingConcerns;
using ExamplesCore.Database;
using ExamplesCore.Services;
using ExamplesCore.Services.Repositories;
using ExamplesCore.Structures;
using Howler;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ExamplesCore.Structures.Base;

namespace ExamplesCore;

public static class ServicesRegistration
{
    public static void RegisteredStructures(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IHttpStructure, HttpStructure>();
        builder.Services.AddScoped<INotificationStructure, NotificationStructure>();
        builder.Services.AddScoped<IValidationStructure, ValidationStructure>();
        builder.Services.AddScoped<IMicroServiceCommunicationStructure, MicroServiceCommunicationStructure>();
        builder.Services.RegisterHowler(Assembly.GetExecutingAssembly());
    }
    public static void RegisteredCrossCuttingConcerns(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IFakeLogger, FakeLogger>();
        builder.Services.AddScoped<IFakeSmsSender, FakeSmsSender>();
        builder.Services.AddSingleton<IFakeEmailSender, FakeEmailSender>();
        builder.Services.AddScoped<IAuthProvider, AuthProvider>();
    }

    public static void RegisteredInfrastructures(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ExampleDbContext>(options => options.UseInMemoryDatabase("FakeContext"));
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddHttpClient();
    }

    public static void RegisteredCoreServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IServiceUsingHowler, ServiceUsingHowler>();
        builder.Services.AddScoped<INormalService, NormalService>();
    }

    public static void RegisteredRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }
}