using System;

namespace WorkflowFoundationRules
{

    public interface IFactBuilder
    {
        Fact Build(string name, object value);
    }

    public class IntegerFactBuilder : IFactBuilder
    {
        public Fact Build(string name, object value)
        {
            var integerValue = new IntegerFactValue(Int32.Parse(value.ToString()));
            return new Fact(name,integerValue);
        }

    }

    public class StringFactBuilder : IFactBuilder
    {
        public Fact Build(string name, object value)
        {
            var stringFactValue = new StringFactValue(value.ToString());
            return new Fact(name,stringFactValue);
        }
    }
}