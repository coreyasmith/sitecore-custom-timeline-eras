using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CustomTimelineEras.Models;
using Sitecore;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Outcomes.Model;
using Sitecore.Marketing.Taxonomy;
using Sitecore.Marketing.Taxonomy.Model;
using Sitecore.Marketing.Taxonomy.Model.OutcomeGroup;

namespace CustomTimelineEras.Services
{
  public class OutcomesPanelViewModelBuilder
  {
    private readonly OutcomeGroupTaxonomyManager _outcomeGroupTaxonomyManager;
    private readonly IDefinitionManager<IOutcomeDefinition> _outcomeDefinitionManager;

    public OutcomesPanelViewModelBuilder(
      OutcomeGroupTaxonomyManager outcomeGroupTaxonomyManager,
      IDefinitionManager<IOutcomeDefinition> outcomeDefinitionManager)
    {
      _outcomeGroupTaxonomyManager = outcomeGroupTaxonomyManager ?? throw new ArgumentNullException(nameof(outcomeGroupTaxonomyManager));
      _outcomeDefinitionManager = outcomeDefinitionManager ?? throw new ArgumentNullException(nameof(outcomeDefinitionManager));
    }

    public OutcomesPanelViewModel BuildViewModel(Alert alert)
    {
      var outcomeGroups = GetAllOutcomeDefinitionGroups();
      return new OutcomesPanelViewModel
      {
        Alert = alert,
        OutcomeGroups = MapOutcomeGroupsToViewModel(outcomeGroups)
      };
    }

    public IEnumerable<OutcomeGroup> GetAllOutcomeDefinitionGroups()
    {
      return _outcomeGroupTaxonomyManager.GetTaxonomy(CultureInfo.InvariantCulture).OutcomeGroups;
    }

    public IEnumerable<OutcomeGroupViewModel> MapOutcomeGroupsToViewModel(IEnumerable<OutcomeGroup> outcomeGroups)
    {
      var viewModels = new List<OutcomeGroupViewModel>();
      foreach (var group in outcomeGroups)
      {
        var definitionsInGroup = GetAllOutcomeDefinitionsForGroup(group);
        viewModels.Add(new OutcomeGroupViewModel
        {
          OutcomeGroupName = group.Name,
          OutcomeDefinitions = MapOutcomeDefinitionsToViewModels(definitionsInGroup)
        });
      }
      return viewModels;
    }

    private IEnumerable<DefinitionResult<IOutcomeDefinition>> GetAllOutcomeDefinitionsForGroup(Taxon outcomeGroup)
    {
      var definitions = _outcomeDefinitionManager.GetAll(CultureInfo.InvariantCulture);
      var definitionsInGroup = definitions.Where(od => od?.Data?.OutcomeGroupUri?.TaxonId == outcomeGroup.Id);
      return definitionsInGroup;
    }

    private static IEnumerable<OutcomeDefinitionViewModel> MapOutcomeDefinitionsToViewModels(IEnumerable<DefinitionResult<IOutcomeDefinition>> outcomeDefinitions)
    {
      return outcomeDefinitions.Select(definition => Context.Database.GetItem(definition.Data.Id))
        .Select(definitionItem => new OutcomeDefinitionViewModel
        {
          Id = definitionItem.ID.ToGuid(),
          Name = definitionItem.Name
        });
    }
  }
}
