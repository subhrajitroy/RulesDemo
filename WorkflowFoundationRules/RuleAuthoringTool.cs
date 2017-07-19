using System;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using System.Workflow.Activities.Rules.Design;

namespace WorkflowFoundationRules
{
    public class RuleAuthoringTool
    {
        private readonly IRulesRepository _rulesRepository;

        public RuleAuthoringTool(IRulesRepository rulesRepository)
        {
            _rulesRepository = rulesRepository;
        }
        public  void Create(string ruleName, Type factType)
        {
            RuleSet ruleset = new RuleSet();
            RuleSetDialog dialog = new RuleSetDialog(factType, null, ruleset);

            DialogResult result = dialog.ShowDialog();

            if
            (result
             ==
             DialogResult.OK
            )
            {
                // save the file
                _rulesRepository.Save(ruleName, dialog.RuleSet);
            }
        }
    }
}