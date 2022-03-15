
using ExamplesForWiseUp;
using Howler;


var builder = WebApplication.CreateBuilder(args);

builder.RegisteredCoreServices();
builder.RegisteredCrossCuttingConcerns();
builder.RegisteredInfrastructures();
builder.RegisteredRepositories();
builder.RegisteredStructures();

builder.Services.AddSwaggerGen();

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
