using System.Web.Mvc;

namespace CustomTimelineEra.Infrastructure
{
  public abstract class BaseController : Controller
  {
    public RedirectResult RedirectToReferrer()
    {
      var redirectUrl = Request.UrlReferrer;
      return redirectUrl == null ? new RedirectResult(Request.Url?.ToString()) : new RedirectResult(redirectUrl.ToString());
    }
  }
}
