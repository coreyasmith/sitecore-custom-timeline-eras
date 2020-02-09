using System.Web.Mvc;
using CustomTimelineEra.Infrastructure;

namespace CustomTimelineEra.Controllers
{
  public class ContactController : BaseController
  {

    [HttpPost]
    public ActionResult IdentifyContact()
    {
      if (ContactHelper.ContactIsIdentified())
      {
        return RedirectToReferrer().WithFailure("Contact already identified.");
      }

      ContactHelper.IdentifyContact();
      ContactHelper.UpdateContactInformation();
      return RedirectToReferrer().WithSuccess("Contact identified.");
    }
  }
}
