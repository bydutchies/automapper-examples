using AutoMapper;
using AutoMapperExamples.Models;

namespace AutoMapperExamples.Mapping;

internal class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<Product, ProductDto>()
      .ReverseMap();
  }
}
