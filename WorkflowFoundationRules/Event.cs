using System.Collections.Generic;

namespace WorkflowFoundationRules
{
    public class Event
    {
        public string SourceId { get; set; }
        public List<RawFact> Facts { get; set; }
    }

    public class RawFact
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
    }
}