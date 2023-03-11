using AutoMapper;
using AutoMapperExamples.Models;
using AutoMapperExamples.Resolvers;

namespace AutoMapperExamples.Mapping;

internal class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<Order, OrderDto>()
        .ForMember(dest => dest.Number, opt => opt.MapFrom<OrderNumberResolver>())
        .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow))
        .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
        .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => GetAddressString(src.Customer.DeliveryAddress)))
        .ForMember(dest => dest.AmountTotal, opt => opt.MapFrom<TotalAmountResolver>())
        .ForMember(dest => dest.ExpectedDeliveryDate, opt => opt.MapFrom<ExpectedDeliveryDateResolver>())
        .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));
    CreateMap<OrderLine, OrderLineDto>();
  }

  #region Private
  private string GetAddressString(Address address)
  {
    return $"{address.Street} {address.Number}, {address.PostalCode} {address.City}";
  }
  #endregion Private
}
