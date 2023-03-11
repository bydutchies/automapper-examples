using AutoMapper;
using AutoMapperExamples.Models;

namespace AutoMapperExamples.Resolvers;

internal class ExpectedDeliveryDateResolver : IValueResolver<Order, OrderDto, DateTime>
{
  public ExpectedDeliveryDateResolver()
  {
  }

  /// <summary>
  /// Determine expected delivery date based on order lines
  /// </summary>
  public DateTime Resolve(Order source, OrderDto destination, DateTime destMember, ResolutionContext context)
  {
    var deliveryDate = DateTime.UtcNow;

    foreach (var item in source.OrderLines)
    {
      if (item.ExpectedDelivery > deliveryDate)
        deliveryDate = item.ExpectedDelivery;
    }

    return deliveryDate;
  }
}
