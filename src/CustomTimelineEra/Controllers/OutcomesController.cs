using System;
using System.Web.Mvc;
using CustomTimelineEra.Extensions;
using CustomTimelineEra.Models;
using CustomTimelineEra.Services;
using Sitecore;
using Sitecore.Common;

namespace CustomTimelineEra.Controllers
{
  public class OutcomesController : BaseController
  {
    private readonly OutcomesPanelViewModelBuilder viewModelBuilder;

    public OutcomesController(OutcomesPanelViewModelBuilder viewModelBuilder)
    {
      this.viewModelBuilder = viewModelBuilder ?? throw new ArgumentNullException(nameof(viewModelBuilder));
    }

    public ViewResult OutcomesPanel(Alert alert)
    {
      var model = viewModelBuilder.BuildViewModel(alert);
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
      
      OutcomeService.RegisterOutcomeForCurrentContact(outcomeDefinitionItem);
      return RedirectToReferrer().WithSuccess($"Successfully triggered outcome \"{outcomeDefinitionItem.Name}\".");
    }
  }
}
