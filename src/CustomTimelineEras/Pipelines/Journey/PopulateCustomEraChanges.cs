using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Sitecore.Cintel.ContactService;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Contact.Journey;
using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Data.Fields;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Outcomes.Model;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;

namespace CustomTimelineEras.Pipelines.Journey
{
  public class PopulateCustomEraChanges : ReportProcessorBase
  {
    public override void Process(ReportProcessorArgs args)
    {
      var resultTableForView = args.ResultTableForView;
      PopulateWithEraChanges(args.ReportParameters.ContactId, resultTableForView);
    }

    private void PopulateWithEraChanges(Guid contactId, DataTable resultTable)
    {
      var changingOutcomesFor = GetEraChangingOutcomesFor(contactId);
      foreach (var dataRow in resultTable.AsEnumerable())
      {
        var timeLineEventId = dataRow.Field<Guid?>(Schema.TimelineEventId.Name);
        if (!timeLineEventId.HasValue) continue;

        var contactOutcome = changingOutcomesFor.SingleOrDefault(o => o.Id == timeLineEventId.Value);
        if (contactOutcome == null) continue;

        var definition = OutcomeDefinitionManager.Get(contactOutcome.DefinitionId, CurrentCultureInfo);
        ConvertToEraChangeEvent(dataRow, definition);
      }
    }

    protected virtual IReadOnlyCollection<Outcome> GetEraChangingOutcomesFor(Guid contactId)
    {
      var allOutcomeDefinitions = OutcomeDefinitionManager.GetAll(CultureInfo.InvariantCulture);
      var allEraChangingOutcomes = allOutcomeDefinitions.Where(IsCustomEraChangingOutcome);

      var contact = GetContact(contactId);
      var contactOutcomes = contact.Interactions.SelectMany(i => i.Events.OfType<Outcome>());
      var eraChangingOutcomes = contactOutcomes.Where(co => allEraChangingOutcomes.Any(o => o.Data.Id == co.DefinitionId));
      return eraChangingOutcomes.ToList();
    }

    protected virtual Contact GetContact(Guid contactId)
    {
      using (var client = SitecoreXConnectClientConfiguration.GetClient())
      {
        var contactReference = new ContactReference(contactId);
        var contact = client.Get(contactReference, new ContactExpandOptions(Array.Empty<string>())
        {
          Interactions = new RelatedInteractionsExpandOptions
          {
            StartDateTime = DateTime.MinValue,
            Limit = int.MaxValue
          }
        });
        if (contact == null) throw new ContactNotFoundException($"No Contact with id [{contactId}] found.");
        return contact;
      }
    }

    protected virtual bool IsCustomEraChangingOutcome(DefinitionResult<IOutcomeDefinition> outcomeDefinition)
    {
      var definitionItem = GetItemFromCurrentContext(outcomeDefinition.Data.Id);
      var showAsEraField = (CheckboxField)definitionItem.Fields[Templates.OutcomeDefinition.Fields.ShowAsEra];
      var isCustomEraChangingOutcome = showAsEraField?.Checked ?? false;
      return isCustomEraChangingOutcome;
    }

    private static void ConvertToEraChangeEvent(DataRow outcomeRow, IDefinition outcomeDefinition)
    {
      outcomeRow.SetField(Schema.EventType.Name, Schema.TimelineEventTypes.EraChange);
      outcomeRow.SetField(Schema.EraText.Name, outcomeDefinition.Name);
    }
  }
}
