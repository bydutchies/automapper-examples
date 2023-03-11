using AutoMapper;
using AutoMapperExamples.Models;

namespace AutoMapperExamples.Mapping;

internal class AddressProfile : Profile
{
  public AddressProfile()
  {
    CreateMap<Address, AddressDto>();
  }
}
