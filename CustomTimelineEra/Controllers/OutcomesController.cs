using System;
using System.Web.Mvc;
using CustomTimelineEra.Infrastructure;
using CustomTimelineEra.Models;
using Sitecore;
using Sitecore.Common;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Outcomes;
using Sitecore.Marketing.Definitions.Outcomes.Model;
using Sitecore.Marketing.Taxonomy;
using Sitecore.Marketing.Taxonomy.Extensions;

namespace CustomTimelineEra.Controllers
{
  public class OutcomesController : BaseController
  {
    private readonly OutcomeHelper _outcomeHelper;

    public OutcomesController()
    {
      var outcomeGroupTaxonomyManager = TaxonomyManager.Provider.GetOutcomeGroupManager();
      var outcomeDefinitionManager = (OutcomeDefinitionManager)DefinitionManagerFactory.Default.GetDefinitionManager<IOutcomeDefinition>();
      _outcomeHelper = new OutcomeHelper(outcomeGroupTaxonomyManager, outcomeDefinitionManager);
    }

    public ViewResult OutcomesPanel(Alert alert)
    {
      var outcomeGroups = _outcomeHelper.GetAllOutcomeDefinitionGroups();
      var model = new OutcomesPanelViewModel
      {
        Alert = alert,
        OutcomeGroups = _outcomeHelper.MapOutcomeGroupsToViewModel(outcomeGroups)
      };
      return View(model);
    }

    [HttpPost]
    public ActionResult TriggerOutcome(Guid outcomeDefinitionId)
    {
      var outcomeDefinitionItem = Context.Database.GetItem(outcomeDefinitionId.ToID());
      if (outcomeDefinitionItem == null)
      {
        return RedirectToReferrer().WithFailure("Outcome not found. Did you forget to publish?");
      }
      
      _outcomeHelper.RegisterOutcomeForCurrentContact(outcomeDefinitionItem);
      return RedirectToReferrer().WithSuccess($"Successfully triggered outcome \"{outcomeDefinitionItem.Name}\".");
    }
  }
}
