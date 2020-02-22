using System;
using System.Web;
using System.Web.Mvc;
using CustomTimelineEras.Models;

namespace CustomTimelineEras.Extensions
{
  public static class RedirectResultExtensions
  {
    public static RedirectResult WithSuccess(this RedirectResult result, string message)
    {
      return WithMessage(result, message, "alert-success");
    }

    public static RedirectResult WithFailure(this RedirectResult result, string message)
    {
      return WithMessage(result, message, "alert-danger");
    }

    private static RedirectResult WithMessage(RedirectResult result, string message, string alertClass)
    {
      if (string.IsNullOrEmpty(message)) return result;

      var redirectUrl = new UriBuilder(result.Url);
      var queryString = HttpUtility.ParseQueryString(redirectUrl.Query);
      queryString[nameof(Alert.Message)] = message;
      queryString[nameof(Alert.Class)] = alertClass;
      redirectUrl.Query = queryString.ToString();

      var resultWithMessage = new RedirectResult(redirectUrl.ToString(), result.Permanent);
      return resultWithMessage;
    }
  }
}
