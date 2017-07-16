using System.Collections.Generic;

namespace CustomTimelineEra.Models
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
