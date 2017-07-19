using System;
using System.Collections.Generic;

namespace WorkflowFoundationRules
{
    public class RuleInfo
    {
        public string RulePath { get; }
        public Type FactType { get; }

        public RuleInfo(string rulePath,Type factType)
        {
            RulePath = rulePath;
            FactType = factType;
        }

        public IFactBuilder GetBuilder()
        {
            if (typeof(int) == FactType)
            {
               return  new IntegerFactBuilder();
            }
            return  new StringFactBuilder();
        }
    }
}