using System.Web.Mvc;
using CustomTimelineEra.Models;

namespace CustomTimelineEra.Controllers
{
  public class AlertController : Controller
  {
    public ActionResult Alert(Alert alert)
    {
      if (string.IsNullOrEmpty(alert?.Message)) return new EmptyResult();
      return View(alert);
    }
  }
}
