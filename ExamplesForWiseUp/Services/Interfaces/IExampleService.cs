using ExamplesForWiseUp.Models;

namespace ExamplesForWiseUp.Services.Interfaces;

public interface IExampleService
{
    Task<Dto> SavePerson(Dto dto);
}