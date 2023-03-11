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
    
    ExampleWithConditionalMapping();

    BiDirectionalMappingExample();

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
  /// An example with conditional mapping. AutoMapper allows adding conditions to properties 
  /// that must be met before that property will be mapped. See PersonMapping class.
  /// </summary>
  private void ExampleWithConditionalMapping()
  {
    var person = GetPerson(16); // child

    PersonDto result = _mapper.Map<Person, PersonDto>(person);

    Console.WriteLine($"ExampleWithConditionalMapping first result: {result.CanVote}");

    person = GetPerson(32); // adult
    
    result = _mapper.Map<Person, PersonDto>(person);

    Console.WriteLine($"ExampleWithConditionalMapping second result: {result.CanVote}");
  }

  /// <summary>
  /// An example with bi-directional mapping. AutoMapper allows reversed mapping. Once the reverse map is configured, 
  /// we can map back from destination to source type. See ProductMapping class.
  /// </summary>
  private void BiDirectionalMappingExample()
  {
    var product = GetProduct();

    ProductDto result = _mapper.Map<Product, ProductDto>(product);

    Console.WriteLine($"BiDirectionalMappingExample first result: {result.Name}");
    Console.WriteLine($"BiDirectionalMappingExample first result: {result.Description}");
    Console.WriteLine($"BiDirectionalMappingExample first result: {result.Price}");

    Product reversedResult = _mapper.Map<ProductDto, Product>(result);

    Console.WriteLine($"BiDirectionalMappingExample first result: {reversedResult.Name}");
    Console.WriteLine($"BiDirectionalMappingExample first result: {reversedResult.Description}");
    Console.WriteLine($"BiDirectionalMappingExample first result: {reversedResult.Price}");
  }

  /// <summary>
  /// Complex example using resolvers, parameters and dependency injection. See OrderMapping class.
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

  private Person GetPerson(int age)
  {
    return new Person
    {
      Name = "Wilco",
      Gender = "male",
      Age = age
    };
  }

  private Product GetProduct()
  {
    return new Product
    {
      Name = "Iphone",
      Description = "Best phone ever",
      Price = 900
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
