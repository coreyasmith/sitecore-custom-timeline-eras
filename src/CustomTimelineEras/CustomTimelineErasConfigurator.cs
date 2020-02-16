using CustomTimelineEras.Controllers;
using CustomTimelineEras.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace CustomTimelineEras
{
  public class CustomTimelineErasConfigurator : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient<ContactController>();
      serviceCollection.AddTransient<OutcomesController>();
      serviceCollection.AddTransient<SessionController>();

      serviceCollection.AddSingleton<ContactService>();
      serviceCollection.AddSingleton<OutcomesPanelViewModelBuilder>();
    }
  }
}
