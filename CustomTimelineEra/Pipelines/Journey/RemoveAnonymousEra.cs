using System.Data;
using System.Linq;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Contact.Journey;
using Sitecore.Cintel.Reporting.Processors;

namespace CustomTimelineEra.Pipelines.Journey
{
  public class RemoveAnonymousEra : ReportProcessorBase
  {
    public bool Enabled { get; set; }

    public bool ShowIcon { get; set; }

    public override void Process(ReportProcessorArgs args)
    {
      if (!Enabled) return;

      var resultTableForView = args.ResultTableForView;
      RemoveAnonymousEraFromTimeline(resultTableForView);
    }

    private void RemoveAnonymousEraFromTimeline(DataTable resultTable)
    {
      var dataRows = resultTable.AsEnumerable();
      var anonymousEras = dataRows.Where(r => r.Field<string>(Schema.EraText.Name) == "Unknown Contact");
      foreach (var anonymousEra in anonymousEras.ToList())
      {
        if (ShowIcon)
        {
          anonymousEra.SetField(Schema.EventType.Name, "Outcome");
        }
        else
        {
          resultTable.Rows.Remove(anonymousEra);
        }
      }
    }
  }
}
