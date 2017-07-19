using System.Workflow.Activities.Rules;

namespace WorkflowFoundationRules
{
    public interface IRulesRepository
    {
        RuleSet Load(string ruleName);
        void Save(string ruleName, RuleSet ruleSet);
    }
}