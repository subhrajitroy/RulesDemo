using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Workflow.Activities.Rules;
using System.Workflow.Activities.Rules.Design;
using Rules.Domain;

namespace Rules.Authoring
{
    public class RuleBuilder : IRuleBuilder
    {

        public RuleSet Build(GraniteRuleSet graniteRuleSet)
        {
            var graniteRule = graniteRuleSet.Rules.First();
            var conditionExpression = graniteRule.Condition;

            var ruleSet = new RuleSet { ChainingBehavior = RuleChainingBehavior.Full };
            ruleSet.Name = graniteRuleSet.Name;

            var rule = new Rule
            {
                Active = true,
                Name = graniteRule.Name,
                Condition = new RuleExpressionCondition(GetCodeExpression(conditionExpression))
            };
            graniteRule.ActionDetails.ForEach(trigger =>
            {
                rule.ThenActions.Add(new RuleStatementAction(GetCodeExpression($"this.Publish(\"Publishing Action :{trigger.Name}\")")));
            });
            rule.ElseActions.Add(new RuleStatementAction(GetCodeExpression("this.Publish(\"No action required\")")));
            ruleSet.Rules.Add(rule);
            return ruleSet;
        }

        private CodeMethodInvokeExpression GetCodeExpression(string conditionExpression)
        {
            var methodDetails = GetMethodDetails(conditionExpression);
            
            var codeMethodReferenceExpression = new CodeMethodReferenceExpression
            {
                TargetObject = new CodeThisReferenceExpression(),
                MethodName = methodDetails.Name
            };
            var parameters =
                methodDetails.Parameters.Select(methodDetailsParameter => new CodePrimitiveExpression(methodDetailsParameter))
                    .Cast<CodeExpression>()
                    .ToList();
           return new CodeMethodInvokeExpression(codeMethodReferenceExpression, parameters.ToArray());
        }

        private MethodInfo GetMethodDetails(string rule)
        {
           // var rule = "this.HasFactWithEqualValue(\"State\",\"Alarm\")";
            var invocations = rule.Split('.');
            var methodNameWithArguments = invocations[1];
            var startIndexOfOpeningBrace = methodNameWithArguments.IndexOf("(", StringComparison.Ordinal);
            var lastIndexOfClosingBrace = methodNameWithArguments.LastIndexOf(")", StringComparison.Ordinal);
            var methodName = methodNameWithArguments.Substring(0, startIndexOfOpeningBrace);

            var arguments
                = methodNameWithArguments.Substring(startIndexOfOpeningBrace + 1, lastIndexOfClosingBrace - startIndexOfOpeningBrace - 1);
            var methodInfo = new MethodInfo();
            methodInfo.Name = methodName;
            foreach (var argument in arguments.Split(','))
            {
                methodInfo.Parameters.Add(argument.Trim('\"'));
            }
            return methodInfo;
        }

        internal class MethodInfo
        {
            internal MethodInfo()
            {
                Parameters = new List<object>();
            }
            internal string Name { get; set; }

            internal  List<object> Parameters { get; }
        }
    }

    public interface IRuleBuilder
    {
        RuleSet Build(GraniteRuleSet graniteRuleSet);
    }
}
