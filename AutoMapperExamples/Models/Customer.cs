namespace AutoMapperExamples.Models;

internal class Customer
{
  public string Name { get; set; }
  public Address Address { get; set; }
  public Address DeliveryAddress { get; set; }
  public string Email { get; set; }
}
