using System;
using System.Web.Mvc;
using CustomTimelineEras.Extensions;
using CustomTimelineEras.Services;

namespace CustomTimelineEras.Controllers
{
  public class ContactController : BaseController
  {
    private readonly ContactService _contactService;

    public ContactController(ContactService contactService)
    {
      _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
    }

    [HttpPost]
    public ActionResult IdentifyContact()
    {
      if (_contactService.ContactIsIdentified())
      {
        return RedirectToReferrer().WithFailure("Contact already identified.");
      }

      _contactService.IdentifyContact();
      _contactService.UpdateContactInformation();
      return RedirectToReferrer().WithSuccess("Contact identified.");
    }
  }
}
