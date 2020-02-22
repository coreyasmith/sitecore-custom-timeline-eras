using Sitecore.Analytics;
using Sitecore.Analytics.Outcome.Extensions;
using Sitecore.Analytics.Outcome.Model;
using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace CustomTimelineEras.Services
{
  public static class OutcomeService
  {
    public static void RegisterOutcomeForCurrentContact(Item outcomeDefinitionItem)
    {
      var outcomeId = ID.NewID;
      var definitionId = outcomeDefinitionItem.ID;
      var contactId = Tracker.Current.Contact.ContactId.ToID();

      var outcome = new ContactOutcome(outcomeId, definitionId, contactId);
      Tracker.Current.RegisterContactOutcome(outcome);
    }
  }
}
