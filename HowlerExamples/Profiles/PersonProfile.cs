using AutoMapper;
using HowlerExamples.Database;
using HowlerExamples.Models;

namespace HowlerExamples.Profiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Dto, Person>();
        CreateMap<DtoNotifiable, Person>()
            .ForMember(x => x.Age, y => y.MapFrom(z => z.Age))
            .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
            .ForMember(x => x.Surname, y => y.MapFrom(z => z.Surname))
            .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
            .ForMember(x => x.PhoneNumber, y => y.MapFrom(z => z.PhoneNumber));
    }
}