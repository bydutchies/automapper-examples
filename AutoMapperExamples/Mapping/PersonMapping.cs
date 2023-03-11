using AutoMapper;
using AutoMapperExamples.Models;

namespace AutoMapperExamples.Mapping;

internal class PersonProfile : Profile
{
  public PersonProfile()
  {
    CreateMap<Person, PersonDto>()
      //.ForMember(dest => dest.CanVote, opt => opt.Condition(src => src.Age > 18 ? true : false))
      .ForMember(dest => dest.CanVote, opt =>
      {
        opt.PreCondition(src => src.Age > 18);
        opt.MapFrom(src => "This person is eligible to vote");
      });
  }
}
