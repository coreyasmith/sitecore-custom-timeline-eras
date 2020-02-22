using System.Web.Mvc;
using CustomTimelineEras.Models;

namespace CustomTimelineEras.Controllers
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
