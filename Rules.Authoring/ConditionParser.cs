using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rules.Domain;

namespace Rules.Authoring
{
    public class ConditionParser
    {
        public ICondition Parse(string json)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(json);
            String op  ;
            if (GetValue(jObject, "Op",out op))
            {
                return GetAggregateCondition(jObject);
            }
            return null;
        }

        private static ICondition GetAggregateCondition(JToken jObject)
        {
            var op = jObject["Op"].Value<string>();
            var conditionOperator = ConditionOperator.And.ToString().Equals(op) ? ConditionOperator.And : ConditionOperator.Or;
            var aggregateCondition = new AggregateCondition {Operator = conditionOperator};
            if (jObject["conditions"].HasValues)
            {
                foreach (var child in jObject["conditions"])
                {
                    string @operator;
                    if (GetValue(child, "Op", out @operator))
                    {
                        var condition = GetAggregateCondition(child);
                        aggregateCondition.Conditions.Add(condition);
                        continue;
                    }
                    string property;
                    if (GetValue(child,"property",out property) )
                    {
                        var condition = new Condition
                        {
                            Property = property,
                            Value = child["value"].Value<string>(),
                            Op = child["op"].Value<string>()
                        };
                        aggregateCondition.Conditions.Add(condition);
                        continue;
                    }
                    throw new Exception($"Could not parse \n {child}");
                }
            }
            return aggregateCondition;
        }

        private static bool GetValue(JToken jObject, string key,out string value)
        {
            value = jObject[key]?.Value<string>();
            return !String.IsNullOrEmpty(value);
        }
    }
}