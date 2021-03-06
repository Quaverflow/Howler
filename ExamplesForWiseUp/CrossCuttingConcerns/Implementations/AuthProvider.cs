using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Repositories;
using Utilities;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Implementations;

public class AuthProvider : IAuthProvider
{
    public int Counter = 0;
    private readonly IBaseRepository<Person> _personRepository;

    public AuthProvider(IBaseRepository<Person> personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task HasAccess(Guid id)
    {
        Counter++;
        (Counter < 2).ThrowIfAssumptionFailed(Counter.ToString());
        var exists = await _personRepository.GetByIdAsync(id);
        (exists != null).ThrowIfAssumptionFailed("You're not authorized to see this.");
    }
}