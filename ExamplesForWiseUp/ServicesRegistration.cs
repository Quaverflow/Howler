using System.Reflection;
using ExamplesForWiseUp.CrossCuttingConcerns;
using ExamplesForWiseUp.CrossCuttingConcerns.Implementations;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Implementations;
using ExamplesForWiseUp.Services.Interfaces;
using ExamplesForWiseUp.Structures.Implementations;
using ExamplesForWiseUp.Structures.Interfaces;
using Howler;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExamplesForWiseUp;

public static class ServicesRegistration
{
    public static void RegisteredStructures(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterHowler(Assembly.GetExecutingAssembly());
    }
    public static void RegisteredCrossCuttingConcerns(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IFakeLogger, FakeLogger>();
        builder.Services.AddScoped<IFakeSmsSender, FakeSmsSender>();
        builder.Services.AddScoped<IFakeEmailSender, FakeEmailSender>();
        builder.Services.AddTransient<IAuthProvider, AuthProvider>();
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
        builder.Services.AddScoped<IExampleService, ExampleService>();
    }

    public static void RegisteredRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }
}