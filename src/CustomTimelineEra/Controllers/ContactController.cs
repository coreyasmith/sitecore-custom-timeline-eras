using System.Web.Mvc;
using CustomTimelineEra.Extensions;
using CustomTimelineEra.Services;

namespace CustomTimelineEra.Controllers
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
