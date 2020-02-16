using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Marketing.Definitions.Outcomes.Model;

namespace CustomTimelineEras.Services
{
  public static class OutcomeService
  {
    public static void RegisterOutcomeForCurrentContact(Item outcomeDefinitionItem)
    {
      var outcome = GetOutcomeDefinition(outcomeDefinitionItem.ID);
      Tracker.Current.Session.Interaction.CurrentPage.RegisterOutcome(outcome, "US", 0);
    }

    private static IOutcomeDefinition GetOutcomeDefinition(ID id)
    {
      return Tracker.MarketingDefinitions.Outcomes[id];
    }
  }
}
