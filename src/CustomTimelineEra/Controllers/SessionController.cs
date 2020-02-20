using System.Web.Mvc;
using CustomTimelineEra.Extensions;
using Sitecore.Analytics;

namespace CustomTimelineEra.Controllers
{
  public class SessionController : BaseController
  {

    [HttpPost]
    public ActionResult AbandonSession()
    {
      Tracker.Current.EndTracking();
      Session.Abandon();
      return RedirectToReferrer().WithSuccess("Session abandoned.");
    }
  }
}
