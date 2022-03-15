using AutoMapper;
using ExamplesForWiseUp.CrossCuttingConcerns.Implementations;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Interfaces;
using ExamplesForWiseUp.Structures.Dtos;

namespace ExamplesForWiseUp.Services.Implementations;

public class ExampleService : IExampleService
{
    private readonly IBaseRepository<Person> _personRepository;
    private readonly IMapper _mapper;
    private readonly IAuthProvider _authProvider;
    public ExampleService(IBaseRepository<Person> personRepository, IMapper mapper, IAuthProvider authProvider)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _authProvider = authProvider;
    }

    public async Task<IHttpStructureDto> SavePerson(Dto dto)
    {
        var person = _mapper.Map<Person>(dto);

        await _personRepository.AddAndSaveAsync(person);

        return new PostResponseDto<Dto>(_mapper.Map<Dto>(person));
    }    



    public async Task<IHttpStructureDto> SavePersonNormal(Dto dto)
    {
        var pro = _authProvider as AuthProvider;
        pro.Counter++;
        if (pro.Counter > 1)
        {
            throw new Exception(pro.Counter.ToString());
        }
        var person = _mapper.Map<Person>(dto);

        await _personRepository.AddAndSaveAsync(person);

        return new PostResponseDto<Dto>(_mapper.Map<Dto>(person));
    }
}