using AutoMapperExamples;
using AutoMapperExamples.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Automapper;

class Program
{
  static async Task Main(string[] args)
  {
    // Create service collection and configure our services
    var services = ConfigureServices();

    // Generate a provider
    var serviceProvider = services.BuildServiceProvider();

    // Kick off our actual code
    await serviceProvider.GetService<Examples>().RunAsync();
  }

  private static IServiceCollection ConfigureServices()
  {
    IServiceCollection services = new ServiceCollection();

    // Add configuration file
    var appSettings = new VatOptions() { Percentage = 20 };
    IOptions<VatOptions> options = Options.Create(appSettings);
    services.AddSingleton(options);

    // Add automapper and read all mapping configuration files in loaded assemblies.
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Add examples
    services.AddTransient<Examples>();

    return services;
  }
}

