using AutoMapper;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Models;

namespace ExamplesForWiseUp.Profiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Dto, Person>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ReverseMap();
    }
}