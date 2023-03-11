using AutoMapper;
using AutoMapperExamples.Models;
using AutoMapperExamples.Options;
using Microsoft.Extensions.Options;

namespace AutoMapperExamples.Resolvers;

internal class TotalAmountResolver : IValueResolver<Order, OrderDto, double>
{
  private readonly VatOptions _options;

  public TotalAmountResolver(IOptions<VatOptions> options)
  {
    _options = options.Value;
  }

  /// <summary>
  /// Calculate total amount including VAT
  /// </summary>
  public double Resolve(Order source, OrderDto destination, double destMember, ResolutionContext context)
  {
    double amount = 0;

    foreach (var item in source.OrderLines)
      amount += item.Quantity * item.Price;

    return amount * (1 + _options.Percentage / 100);
  }
}
