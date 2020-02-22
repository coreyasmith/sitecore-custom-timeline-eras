using Sitecore.Configuration;

namespace CustomTimelineEras
{
  public static class Constants
  {
    public static readonly string ContactEmail = Settings.GetSetting("CustomTimelineEras.ContactEmail");
  }
}
