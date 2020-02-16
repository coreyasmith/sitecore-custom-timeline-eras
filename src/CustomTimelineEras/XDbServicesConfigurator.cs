using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.Analytics.Tracking;
using Sitecore.DependencyInjection;
using Sitecore.Marketing.Taxonomy;
using Sitecore.Marketing.Taxonomy.Extensions;

namespace CustomTimelineEras
{
  public class XDbServicesConfigurator : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton(serviceProvider => (ContactManager)serviceProvider.GetService<BaseFactory>().CreateObject("tracking/contactManager", true));
      serviceCollection.AddSingleton(serviceProvider => serviceProvider.GetService<ITaxonomyManagerProvider>().GetOutcomeGroupManager());
    }
  }
}
