using System.Collections.Generic;
using Rules.Authoring;

namespace Rules.Domain
{
    public class AggregateCondition : ICondition
    {
        public AggregateCondition()
        {
            Conditions = new List<ICondition>();
        }
        public ConditionOperator Operator { get; set; }
        public List<ICondition> Conditions { get; set; }
    }
}