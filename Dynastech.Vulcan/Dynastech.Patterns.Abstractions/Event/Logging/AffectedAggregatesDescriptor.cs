using System.Collections.Generic;

namespace Dynastech.Patterns
{
    public class AffectedAggregatesDescriptor
    {
        public int Count { get; set; }
        public string Type { get; set; }

        public List<AggregateDescriptor> Aggregates { get; set; } = new List<AggregateDescriptor>();
    }
}
