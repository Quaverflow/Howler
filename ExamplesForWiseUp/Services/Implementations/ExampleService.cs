using AutoMapper;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Repositories;
using ExamplesForWiseUp.Services.Interfaces;
using ExamplesForWiseUp.Structures;
using Howler;
using ExamplesForWiseUp.Structures.HttpStructures;
using ExamplesForWiseUp.Structures.MicroServiceMessaging;
using ExamplesForWiseUp.Structures.Notifications;

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

        await _howler.InvokeVoidAsync(StructureIds.NotifyItIsChris, async () =>
        {
            var email = new EmailDto(person.Email, "hello my friend!", "feeling nice and cute");
            await _howler.TransmitVoidAsync(StructureIds.SendEmail, email);

            var sms = new SmsDto(person.PhoneNumber, "Beautiful Day!");
            await _howler.TransmitVoidAsync(StructureIds.SendSms, sms);

            var message = new MicroserviceMessage("https://localhost:7060/Example/Post", HttpMethod.Post, person);
            await _howler.TransmitVoidAsync(StructureIds.NotifyMicroService, message);

        }, person.Name);

        return new PostResponseDto<Dto>(_mapper.Map<Dto>(person));
    }
}