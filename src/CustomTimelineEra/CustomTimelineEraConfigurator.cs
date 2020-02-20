using CustomTimelineEra.Controllers;
using CustomTimelineEra.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace CustomTimelineEra
{
  public class CustomTimelineEraConfigurator : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient<ContactController>();
      serviceCollection.AddTransient<OutcomesController>();
      serviceCollection.AddTransient<SessionController>();

      serviceCollection.AddSingleton<OutcomesPanelViewModelBuilder>();
    }
  }
}
