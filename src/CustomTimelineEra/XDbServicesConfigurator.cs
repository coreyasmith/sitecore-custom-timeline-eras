using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Outcomes.Model;
using Sitecore.Marketing.Taxonomy;
using Sitecore.Marketing.Taxonomy.Extensions;

namespace CustomTimelineEra
{
  public class XDbServicesConfigurator : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton(_ => DefinitionManagerFactory.Default);
      serviceCollection.AddSingleton(serviceProvider => serviceProvider.GetService<DefinitionManagerFactory>().GetDefinitionManager<IOutcomeDefinition>());
      serviceCollection.AddSingleton(_ => TaxonomyManager.Provider);
      serviceCollection.AddSingleton(serviceProvider => serviceProvider.GetService<ITaxonomyManagerProvider>().GetOutcomeGroupManager());
    }
  }
}
