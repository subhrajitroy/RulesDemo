namespace WorkflowFoundationRules
{
    public class IntegerFactValue  : IFactValue
    {
        private readonly int _value;
        public IntegerFactValue(int value)
        {
            _value = value;
        }

        public object Value => _value;

        public bool Equals(object other)
        {
            return _value == (int)other;
        }

        public bool GreaterThan(object other)
        {
            return _value > (int)other;
        }

        public bool LessThan(object other)
        {
            return _value < (int)other;
        }
    }

    
}