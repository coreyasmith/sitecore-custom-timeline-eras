using System.Collections.Generic;

namespace CustomTimelineEra.Models
{
  public class OutcomesPanelViewModel
  {
    public Alert Alert { get; set; }
    public IEnumerable<OutcomeGroupViewModel> OutcomeGroups { get; set; }

    public OutcomesPanelViewModel()
    {
      OutcomeGroups = new List<OutcomeGroupViewModel>();
    }
  }
}
