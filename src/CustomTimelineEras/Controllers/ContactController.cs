using System.Web.Mvc;
using CustomTimelineEras.Extensions;
using CustomTimelineEras.Services;

namespace CustomTimelineEras.Controllers
{
  public class ContactController : BaseController
  {

    [HttpPost]
    public ActionResult IdentifyContact()
    {
      if (ContactService.ContactIsIdentified())
      {
        return RedirectToReferrer().WithFailure("Contact already identified.");
      }

      ContactService.IdentifyContact();
      ContactService.UpdateContactInformation();
      return RedirectToReferrer().WithSuccess("Contact identified.");
    }
  }
}
