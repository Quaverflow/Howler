using AutoMapper;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Interfaces;

namespace ExamplesForWiseUp.Services.Implementations;

public class ExampleService : IExampleService
{
    private readonly IBaseRepository<Person> _personRepository;
    private readonly IMapper _mapper;
    public ExampleService(IBaseRepository<Person> personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<Dto> SavePerson(Dto dto)
    {
        var person = _mapper.Map<Person>(dto);

        await _personRepository.AddAndSaveAsync(person);
        return _mapper.Map<Dto>(person);
    }
}