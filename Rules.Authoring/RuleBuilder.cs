using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Workflow.Activities.Rules;
using System.Workflow.Activities.Rules.Design;

namespace Rules.Authoring
{
    public class RuleBuilder : IRuleBuilder
    {

        public RuleSet Build(string ruleSetJson)
        {
            var ruleSet = new RuleSet();
            ruleSet.ChainingBehavior = RuleChainingBehavior.Full;
            
            var rule = new Rule();
            rule.Active = true;
            var codeMethodReferenceExpression = new CodeMethodReferenceExpression();
            codeMethodReferenceExpression.TargetObject = new CodeThisReferenceExpression();
            codeMethodReferenceExpression.MethodName = "Get";
            var codeMethodInvokeExpression = new CodeMethodInvokeExpression(codeMethodReferenceExpression,"");
            rule.Condition = new RuleExpressionCondition(codeMethodInvokeExpression);
            ruleSet.Rules.Add(rule);
            return ruleSet;
        }
    }

    public interface IRuleBuilder
    {
        RuleSet Build(string ruleSetJson);
    }
}
