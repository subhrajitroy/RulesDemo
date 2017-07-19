namespace WorkflowFoundationRules
{
    public class Fact : IFact
    {
        public Fact(string name, IFactValue factValue)
        {
            Name = name;
            Value = factValue;
        }

        public string Name { get; set; }
        public IFactValue Value { get; set; }
    }

    public interface IFact
    {
        string Name { get; set; }
        IFactValue Value { get; set; }
    }
}