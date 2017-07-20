using System;

namespace WorkflowFoundationRules
{

    public interface IFactBuilder
    {
        Fact Build(string name, object value);
        Fact Build(RawFact rawFact);
    }

    public class IntegerFactBuilder : IFactBuilder
    {
        public Fact Build(string name, object value)
        {
            var integerValue = new IntegerFactValue(Int32.Parse(value.ToString()));
            return new Fact(name,integerValue);
        }

        public Fact Build(RawFact rawFact)
        {
            return Build(rawFact.Name, rawFact.Value);
        }
    }

    public class StringFactBuilder : IFactBuilder
    {
        public Fact Build(string name, object value)
        {
            var stringFactValue = new StringFactValue(value.ToString());
            return new Fact(name,stringFactValue);
        }
        public Fact Build(RawFact rawFact)
        {
            return Build(rawFact.Name, rawFact.Value);
        }
    }
}