using System.Web.Mvc;

namespace CustomTimelineEra.Controllers
{
  public abstract class BaseController : Controller
  {
    protected RedirectResult RedirectToReferrer()
    {
      var redirectUrl = Request.UrlReferrer;
      return redirectUrl == null ? new RedirectResult(Request.Url?.ToString()) : new RedirectResult(redirectUrl.ToString());
    }
  }
}
