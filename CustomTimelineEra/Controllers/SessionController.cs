using System.Web.Mvc;
using CustomTimelineEra.Infrastructure;

namespace CustomTimelineEra.Controllers
{
  public class SessionController : BaseController
  {
    [HttpPost]
    public ActionResult AbandonSession()
    {
      Session.Abandon();
      return RedirectToReferrer();
    }
  }
}
