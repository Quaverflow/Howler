using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Structures.Dtos;

namespace ExamplesForWiseUp.Services.Interfaces;

public interface IExampleService
{
    Task<IHttpStructureDto> SavePerson(Dto dto);
}