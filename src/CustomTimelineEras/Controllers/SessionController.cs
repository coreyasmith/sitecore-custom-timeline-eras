using System.Web.Mvc;
using CustomTimelineEras.Extensions;
using Sitecore.Analytics;

namespace CustomTimelineEras.Controllers
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
