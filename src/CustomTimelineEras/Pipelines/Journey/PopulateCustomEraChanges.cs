using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Sitecore;
using Sitecore.Analytics.Outcome.Model;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Contact.Journey;
using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Common;
using Sitecore.Data.Fields;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Outcomes.Model;

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

        var contactOutcome = changingOutcomesFor.SingleOrDefault(o => o.Id == timeLineEventId.Value.ToID());
        if (contactOutcome == null) continue;

        var outcomeDefinition = OutcomeDefinitionManager.Get(contactOutcome.DefinitionId, CurrentCultureInfo);
        ConvertToEraChangeEvent(dataRow, outcomeDefinition);
      }
    }

    protected virtual List<ContactOutcome> GetEraChangingOutcomesFor(Guid contactId)
    {
      var allOutcomeDefinitions = OutcomeDefinitionManager.GetAll(CultureInfo.InvariantCulture);
      var allEraChangingOutcomes = allOutcomeDefinitions.Where(IsCustomEraChangingOutcome).ToList();

      var contactOutcomes = OutcomeManager.GetForEntity<ContactOutcome>(contactId.ToID());
      var contactEraChangingOutcomes = contactOutcomes.Where(oc => allEraChangingOutcomes.Any(od => od.Data.Id == oc.DefinitionId));
      return contactEraChangingOutcomes.ToList();
    }

    protected virtual bool IsCustomEraChangingOutcome(DefinitionResult<IOutcomeDefinition> outcomeDefinition)
    {
      var outcomeDefinitionItem = Context.Database.GetItem(outcomeDefinition.Data.Id);
      var showAsEraField = (CheckboxField)outcomeDefinitionItem.Fields[Templates.CustomOutcomeDefinition.Fields.ShowAsEra];
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
