using ExamplesForWiseUp.Structures.Dtos;

namespace ExamplesForWiseUp.Structures.Interfaces;

public interface IHttpStructure
{
     Task<IHttpStructureDto> OnPostAsync(Func<Task<IHttpStructureDto>> invocation, Guid id);
}

