using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CustomTimelineEra.Models;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Analytics.Outcome.Extensions;
using Sitecore.Analytics.Outcome.Model;
using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Outcomes.Model;
using Sitecore.Marketing.Taxonomy;
using Sitecore.Marketing.Taxonomy.Model;
using Sitecore.Marketing.Taxonomy.Model.OutcomeGroup;

namespace CustomTimelineEra.Infrastructure
{
  public class OutcomeHelper
  {
    private readonly OutcomeGroupTaxonomyManager _outcomeGroupTaxonomyManager;
    private readonly IDefinitionManager<IOutcomeDefinition> _outcomeDefinitionManager;

    public OutcomeHelper(
      OutcomeGroupTaxonomyManager outcomeGroupTaxonomyManager,
      IDefinitionManager<IOutcomeDefinition> outcomeDefinitionManager)
    {
      _outcomeGroupTaxonomyManager = outcomeGroupTaxonomyManager ?? throw new ArgumentNullException(nameof(outcomeGroupTaxonomyManager));
      _outcomeDefinitionManager = outcomeDefinitionManager ?? throw new ArgumentNullException(nameof(outcomeDefinitionManager));
    }

    public IEnumerable<OutcomeGroup> GetAllOutcomeDefinitionGroups()
    {
      return _outcomeGroupTaxonomyManager.GetTaxonomy(CultureInfo.InvariantCulture).OutcomeGroups;
    }

    public IEnumerable<OutcomeGroupViewModel> MapOutcomeGroupsToViewModel(IEnumerable<OutcomeGroup> outcomeGroups)
    {
      var outcomeGroupViewModels = new List<OutcomeGroupViewModel>();
      foreach (var outcomeGroup in outcomeGroups)
      {
        var definitionsInGroup = GetAllOutcomeDefinitionsForGroup(outcomeGroup);
        outcomeGroupViewModels.Add(new OutcomeGroupViewModel
        {
          OutcomeGroupName = outcomeGroup.Name,
          OutcomeDefinitions = MapOutcomeDefinitionsToViewModels(definitionsInGroup)
        });
      }
      return outcomeGroupViewModels;
    }

    private IEnumerable<DefinitionResult<IOutcomeDefinition>> GetAllOutcomeDefinitionsForGroup(Taxon outcomeGroup)
    {
      var outcomeDefinitions = _outcomeDefinitionManager.GetAll(CultureInfo.InvariantCulture);
      var definitionsInGroup = outcomeDefinitions.Where(od => od?.Data?.OutcomeGroupUri?.TaxonId == outcomeGroup.Id);
      return definitionsInGroup;
    }

    private static IEnumerable<OutcomeDefinitionViewModel> MapOutcomeDefinitionsToViewModels(IEnumerable<DefinitionResult<IOutcomeDefinition>> outcomeDefinitions)
    {
      var outcomeViewModels = new List<OutcomeDefinitionViewModel>();
      foreach (var outcomeDefiniton in outcomeDefinitions)
      {
        var itemId = outcomeDefiniton.Data.Id;
        var outcomeDefinitionItem = Context.Database.GetItem(itemId);
        outcomeViewModels.Add(new OutcomeDefinitionViewModel
        {
          Id = itemId.ToGuid(),
          Name = outcomeDefinitionItem.Name
        });
      }
      return outcomeViewModels;
    }

    public void RegisterOutcomeForCurrentContact(Item outcomeDefinitionItem)
    {
      var outcomeId = ID.NewID;
      var definitionId = outcomeDefinitionItem.ID;
      var contactId = Tracker.Current.Contact.ContactId.ToID();

      var outcome = new ContactOutcome(outcomeId, definitionId, contactId);
      Tracker.Current.RegisterContactOutcome(outcome);
    }
  }
}
