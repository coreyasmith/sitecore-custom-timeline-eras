using Sitecore.Analytics.Model;
using Sitecore.Analytics.Pipelines.CreateVisits;

namespace CustomTimelineEra.Pipelines.CreateVisit
{
  public class IdentifyContact : CreateVisitProcessor
  {
    public override void Process(CreateVisitArgs args)
    {
      var session = args.Session;
      if (session.Contact.Identifiers.IdentificationLevel == ContactIdentificationLevel.Known) return;

      session.Identify(Constants.ContactEmail);
    }
  }
}
