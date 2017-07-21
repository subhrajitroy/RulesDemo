namespace WorkflowFoundationRules
{
    public interface IFactValue
    {
        object Value { get;  }
        bool Equals(object other);

        bool GreaterThan(object other);

        bool LessThan(object other);

    }
}