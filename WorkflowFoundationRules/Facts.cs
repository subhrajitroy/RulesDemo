using System.Collections.Generic;

namespace WorkflowFoundationRules
{
    public class Facts
    {
        private readonly Dictionary<string, Fact> _dictionary;

        public Facts()
        {
            _dictionary = new Dictionary<string, Fact>();
        }

        public void Add(Fact fact)
        {
            _dictionary.Add(fact.Name,fact);
        }

        public void AddAll(IEnumerable<Fact> facts)
        {
            foreach (var fact in facts)
            {
                 Add(fact);
            }
        }

        public IFactValue Get(string key)
        {
            return _dictionary.ContainsKey(key) ? _dictionary[key].Value : null;
        }

        public bool HasFactWithEqualValue(string factName, object value)
        {
            return Get(factName).Equals(value);
        }

        public bool HasFactWithGreaterValue(string factName, object value)
        {
            return Get(factName).GreaterThan(value);
        }

        public bool HasFactWithLesserValue(string factName, object value)
        {
            return Get(factName).LessThan(value);
        }
    }
}