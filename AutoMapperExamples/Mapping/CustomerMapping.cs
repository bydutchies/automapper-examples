using AutoMapper;
using AutoMapperExamples.Models;

namespace AutoMapperExamples.Mapping;

internal class CustomerProfile : Profile
{
  public CustomerProfile()
  {
    CreateMap<Customer, CustomerDto>()
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetAddressString(src.Address)))
        .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email));
  }

  #region Private
  private string GetAddressString(Address address)
  {
    return $"{address.Street} {address.Number}, {address.PostalCode} {address.City}";
  }
  #endregion Private
}
