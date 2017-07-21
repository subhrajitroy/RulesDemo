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

        public RuleSet Build(string conditionExpression)
        {
            var ruleSet = new RuleSet { ChainingBehavior = RuleChainingBehavior.Full };

            var rule = new Rule
            {
                Active = true,
                Condition = new RuleExpressionCondition(GetCodeInvocation(conditionExpression))
            };
            rule.ThenActions.Add(new RuleStatementAction());
            rule.ElseActions.Add(new RuleStatementAction());
            ruleSet.Rules.Add(rule);
            return ruleSet;
        }

        private CodeMethodInvokeExpression GetCodeInvocation(string conditionExpression)
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
            var indexOfOpeningBrace = methodNameWithArguments.IndexOf("(");
            var lastIndexOfClosingBrace = methodNameWithArguments.LastIndexOf(")");
            var methodName = methodNameWithArguments.Substring(0, indexOfOpeningBrace);

            var arguments
                = methodNameWithArguments.Substring(indexOfOpeningBrace + 1, lastIndexOfClosingBrace - indexOfOpeningBrace);

            var methodInfo = new MethodInfo();
            methodInfo.Name = methodName;
            foreach (var argument in arguments)
            {
                methodInfo.Parameters.Add(argument.ToString().Trim('"'));
            }
            return methodInfo;
        }

        private class MethodInfo
        {
            internal string Name { get; set; }

            internal  List<object> Parameters { get; set; }
        }
    }

    public interface IRuleBuilder
    {
        RuleSet Build(string conditionExpression);
    }
}
