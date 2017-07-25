using Rules.Domain;

namespace Rules.Authoring
{
    public class Condition : ICondition
    {
        public string Property { get; set; }
        public string Op { get; set; }
        public object Value { get; set; }
    }
}