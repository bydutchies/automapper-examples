namespace AutoMapperExamples.Models;

internal class OrderDto
{
  public int Number { get; set; }
  public DateTime OrderDate { get; set; }
  public string CustomerName { get; set; }
  public string CustomerAddress { get; set; }
  public double AmountTotal { get; set; }
  public DateTime ExpectedDeliveryDate { get; set; }
  public List<OrderLineDto> OrderLines { get; set; }
}
