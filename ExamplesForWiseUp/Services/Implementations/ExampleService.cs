using AutoMapper;
using ExamplesForWiseUp.CrossCuttingConcerns.Implementations;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Interfaces;
using ExamplesForWiseUp.Structures;
using ExamplesForWiseUp.Structures.Dtos;
using Howler;

namespace ExamplesForWiseUp.Services.Implementations;

public class ExampleService : IExampleService
{
    private readonly IBaseRepository<Person> _personRepository;
    private readonly IHowler _howler;
    private readonly IMapper _mapper;
    public ExampleService(IBaseRepository<Person> personRepository, IMapper mapper, IHowler howler)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _howler = howler;
    }

    public async Task<IHttpStructureDto> SavePerson(Dto dto)
    {
        var person = _mapper.Map<Person>(dto);

        await _personRepository.AddAndSaveAsync(person);

        await _howler.TransmitVoidAsync(StructureIds.SendEmail, new EmailDto(person.Email, "hello my friend!", "feeling nice and cute"));
        await _howler.TransmitVoidAsync(StructureIds.SendSms, new SmsDto(person.PhoneNumber, "Beautiful Day!"));
;
        return new PostResponseDto<Dto>(_mapper.Map<Dto>(person));
    }    

}