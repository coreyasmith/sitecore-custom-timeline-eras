using System.Data;
using System.Linq;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Contact.Journey;
using Sitecore.Cintel.Reporting.Processors;

namespace CustomTimelineEra.Pipelines.Journey
{
  public class RenameAnonymousEra : ReportProcessorBase
  {
    public string AnonymousEraName { get; set; }

    public override void Process(ReportProcessorArgs args)
    {
      var resultTableForView = args.ResultTableForView;
      RenameAnonymousEraInTimeline(resultTableForView);
    }

    private void RenameAnonymousEraInTimeline(DataTable resultTable)
    {
      var dataRows = resultTable.AsEnumerable();
      var anonymousEras = dataRows.Where(r => r.Field<string>(Schema.EraText.Name) == "Unknown Contact");
      foreach (var anonymousEra in anonymousEras.ToList())
      {
        anonymousEra.SetField(Schema.EraText.Name, AnonymousEraName);
      }
    }
  }
}
