namespace AutoMapperExamples.Models;

internal class OrderLine
{
  public string Product { get; set; }
  public int Quantity { get; set; }
  public double Price { get; set; }
  public DateTime ExpectedDelivery { get; set; }
}
