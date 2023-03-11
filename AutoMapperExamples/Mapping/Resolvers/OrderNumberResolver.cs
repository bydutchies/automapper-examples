using AutoMapper;
using AutoMapperExamples.Models;

namespace AutoMapperExamples.Resolvers;

internal class OrderNumberResolver : IValueResolver<Order, OrderDto, int>
{
  public OrderNumberResolver()
  {
  }

  /// <summary>
  /// Make order number equal to unique identifier passed as parameter 
  /// </summary>
  public int Resolve(Order source, OrderDto destination, int destMember, ResolutionContext context)
  {
    return Convert.ToInt32(context.Items["UniqueIdentifier"]);
  }
}
