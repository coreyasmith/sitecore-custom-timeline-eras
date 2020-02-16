using System;
using System.Web.Mvc;
using CustomTimelineEras.Extensions;
using CustomTimelineEras.Models;
using CustomTimelineEras.Services;
using Sitecore;
using Sitecore.Common;

namespace CustomTimelineEras.Controllers
{
  public class OutcomesController : BaseController
  {
    private readonly OutcomesPanelViewModelBuilder _viewModelBuilder;

    public OutcomesController(OutcomesPanelViewModelBuilder viewModelBuilder)
    {
      _viewModelBuilder = viewModelBuilder ?? throw new ArgumentNullException(nameof(viewModelBuilder));
    }

    public ViewResult OutcomesPanel(Alert alert)
    {
      var model = _viewModelBuilder.BuildViewModel(alert);
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
