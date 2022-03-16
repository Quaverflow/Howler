using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Structures.HttpStructures;

namespace ExamplesForWiseUp.Services.Interfaces;

public interface IExampleService
{
    Task<IHttpStructureDto> SavePerson(Dto dto);
}