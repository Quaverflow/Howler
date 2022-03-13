using System.Reflection;
using Howler;
using HowlerExamples.CrossCuttingConcerns;
using HowlerExamples.Database;
using HowlerExamples.Services;
using HowlerExamples.Services.Repositories;
using HowlerExamples.Structures;
using HowlerExamples.Structures.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Howler
builder.Services.AddScoped<StructureContainer>();
builder.Services.AddScoped<IHttpStructure, HttpStructure>();
builder.Services.AddScoped<INotificationStructure, NotificationStructure>();
builder.Services.AddScoped<IValidationStructure, ValidationStructure>();
builder.Services.RegisterHowler(Assembly.GetExecutingAssembly());

// Cross cutting concerns
builder.Services.AddSingleton<IFakeLogger, FakeLogger>();
builder.Services.AddSingleton<IFakeSmsSender, FakeSmsSender>();
builder.Services.AddSingleton<IFakeEmailSender, FakeEmailSender>();
builder.Services.AddScoped<IAuthProvider, AuthProvider>();

// Infrastructure
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ExampleDbContext>(options => options.UseInMemoryDatabase("FakeContext"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Services
builder.Services.AddScoped<IServiceUsingHowler, ServiceUsingHowler>();
builder.Services.AddScoped<INormalService, NormalService>();

// Repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterHowlerMiddleware();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
