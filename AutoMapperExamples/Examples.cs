using AutoMapper;
using AutoMapperExamples.Models;

namespace AutoMapperExamples;

internal class Examples
{
  private readonly IMapper _mapper;

  /// <summary>
  /// Added constructor to support DI
  /// </summary>
  public Examples(IMapper mapper)
  {
    _mapper = mapper;
  }

  /// <summary>
  /// Application starting point 
  /// </summary>
  public async Task RunAsync()
  {
    SimpleExample();
    ListExample();
    ExampleWithExtendedMapping();
    ExampleWithResolvers();

    Console.ReadKey();
  }

  #region Private

  #region Examples
  /// <summary>
  /// The most basic example mapping two objects with the same properties. See AddressMapping class.
  /// </summary>
  private void SimpleExample()
  {
    var address = GetAddress();

    AddressDto result = _mapper.Map<Address, AddressDto>(address);

    Console.WriteLine($"SimpleExample result: {result.Street} {result.Number}, {result.PostalCode} {result.City}");
  }

  /// <summary>
  /// A simple example mapping a list of objects.
  /// </summary>
  private void ListExample()
  {
    var addressList = new List<Address> { 
      GetAddress(),
      GetDeliveryAddress()
    };

    List<AddressDto> results = _mapper.Map<List<Address>, List<AddressDto>>(addressList);

    foreach (var result in results)
    {
      Console.WriteLine($"ListExample result: {result.Street} {result.Number}, {result.PostalCode} {result.City}");
    }
  }

  /// <summary>
  /// An example with extended mapping. Properties naming differ. See CustomerMapping class.
  /// </summary>
  private void ExampleWithExtendedMapping()
  {
    var customer = GetCustomer();

    CustomerDto result = _mapper.Map<Customer, CustomerDto>(customer);

    Console.WriteLine($"ExampleWithExtendedMapping result: {result.FullName}");
    Console.WriteLine($"ExampleWithExtendedMapping result: {result.Address}");
    Console.WriteLine($"ExampleWithExtendedMapping result: {result.EmailAddress}");
  }

  /// <summary>
  /// Complex example using resolvers. See OrderMapping class.
  /// </summary>
  private void ExampleWithResolvers()
  {
    var uniqueIdentifier = "20230001";

    var order = GetOrder();

    // Pass parameter to resolver
    var result = _mapper.Map<Order, OrderDto>(order, opt => opt.Items["UniqueIdentifier"] = uniqueIdentifier);

    Console.WriteLine($"ExampleWithResolvers result: {result.Number}");
    Console.WriteLine($"ExampleWithResolvers result: {result.OrderDate}");
    Console.WriteLine($"ExampleWithResolvers result: {result.CustomerName}");
    Console.WriteLine($"ExampleWithResolvers result: {result.CustomerAddress}");
    Console.WriteLine($"ExampleWithResolvers result: {result.AmountTotal}");
    Console.WriteLine($"ExampleWithResolvers result: {result.ExpectedDeliveryDate}");

    foreach (var line in result.OrderLines)
    {
      Console.WriteLine($"ExampleWithResolvers result: {line.Product}");
      Console.WriteLine($"ExampleWithResolvers result: {line.Quantity}");
    }
}
#endregion Examples

private Address GetAddress()
  {
    return new Address
    {
      Street = "Dorpsplein",
      Number = 1,
      PostalCode = "2513AM",
      City = "Den Haag",
      Country = "Nederland",
      State = "Zuid-Holland"
    };
  }

  private Address GetDeliveryAddress()
  {
    return new Address
    {
      Street = "Stationsplein",
      Number = 1,
      PostalCode = "2514AB",
      City = "Den Haag",
      Country = "Nederland",
      State = "Zuid-Holland"
    };
  }

  private Customer GetCustomer()
  {
    return new Customer
    {
      Name = "Wilco",
      Address = GetAddress(),
      DeliveryAddress = GetDeliveryAddress(),
      Email = "example@test.com"
    };
  }

  private Order GetOrder()
  {
    return new Order
    {
      Customer = GetCustomer(),
      OrderLines = new List<OrderLine>
      {
        new OrderLine { Product = "iphone", Price = 900, Quantity = 5, ExpectedDelivery = DateTime.UtcNow.AddDays(1) },
        new OrderLine { Product = "macbook", Price = 2250, Quantity = 1, ExpectedDelivery = DateTime.UtcNow.AddDays(3) },
        new OrderLine { Product = "ipad", Price = 1200, Quantity = 2, ExpectedDelivery = DateTime.UtcNow.AddDays(2) },
      }
    };
  }
  #endregion Private
}
