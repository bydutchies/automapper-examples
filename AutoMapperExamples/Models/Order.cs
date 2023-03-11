namespace AutoMapperExamples.Models;

internal class Order
{
  public Customer Customer { get; set; }
  public List<OrderLine> OrderLines { get; set; }
}
