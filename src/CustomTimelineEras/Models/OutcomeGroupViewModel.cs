using System.Collections.Generic;

namespace CustomTimelineEras.Models
{
  public class OutcomeGroupViewModel
  {
    public string OutcomeGroupName { get; set; }
    public IEnumerable<OutcomeDefinitionViewModel> OutcomeDefinitions { get; set; }

    public OutcomeGroupViewModel()
    {
      OutcomeDefinitions = new List<OutcomeDefinitionViewModel>();
    }
  }
}
