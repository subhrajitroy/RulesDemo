using System;

namespace WorkflowFoundationRules
{
    public class StringFactValue : IFactValue
    {
        private readonly string _value;

        public StringFactValue(string value)
        {
            _value = value;
        }

        public object Value => _value;
        public bool Equals(object other)
        {
            return _value.Equals((string)other);
        }

        public bool GreaterThan(object other)
        {
            throw new NotImplementedException();
        }

        public bool LessThan(object other)
        {
            throw new NotImplementedException();
        }
    }
}