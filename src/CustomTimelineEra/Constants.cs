using Sitecore.Configuration;

namespace CustomTimelineEra
{
  public static class Constants
  {
    public static readonly string ContactEmail = Settings.GetSetting("CustomTimelineEra.ContactEmail");
  }
}
