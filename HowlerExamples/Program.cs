using System.Reflection;
using Howler;
using HowlerExamples.CrossCuttingConcerns;
using HowlerExamples.Services;
using HowlerExamples.Structures;
using HowlerExamples.Structures.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<StructureContainer>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IFakeLogger, FakeLogger>();
builder.Services.AddScoped<IAuthProvider, AuthProvider>();
builder.Services.AddScoped<IServiceUsingHowler, ServiceUsingHowler>();
builder.Services.AddScoped<INormalService, NormalService>();
builder.Services.AddScoped<IHttpStructure, HttpStructure>();
builder.Services.AddScoped<INotificationStructure, NotificationStructure>();
builder.Services.RegisterHowler(Assembly.GetExecutingAssembly());



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
