using System;

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
            return _value == AsInt(other);
        }
        
        public bool GreaterThan(object other)
        {
            return _value > AsInt(other);
        }

        public bool LessThan(object other)
        {
            return _value < AsInt(other);
        }

        private static int AsInt(object other)
        {
//            Console.WriteLine($" Trying to case {other} as string");
            return Int32.Parse(other.ToString());
        }

    }


}