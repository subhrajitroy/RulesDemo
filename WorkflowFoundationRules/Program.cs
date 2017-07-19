using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Workflow.Activities.Rules;

namespace WorkflowFoundationRules
{
    public class Program
    {
        private static Dictionary<string, RuleInfo> _rules;

        static Program()
        {
            _rules = new Dictionary<string,RuleInfo>();
            _rules.Add("thermostat",new RuleInfo("thermostat.rules",typeof(int)));
            _rules.Add("das", new RuleInfo("das.rules", typeof(string)));
        }
        public static void Main(string[] args)
        {
            // Obtain or create ruleset
            var fileBaseRulesRepository = new FileBaseRulesRepository();
            var ruleSetName = args[0];
            var factName = args[1];
            var factValue = args[2];
            var ruleInfo = _rules[ruleSetName];
            var ruleSetFile = ruleInfo.RulePath;

            Fact fact = ruleInfo.GetBuilder().Build(factName,factValue);

            if (File.Exists(ruleSetFile))
            {
                // load file
                var ruleset = fileBaseRulesRepository.Load(ruleSetFile);
                var validation = new RuleValidation(typeof(Fact), null);
                var engine = new RuleExecution(validation, fact);
                ruleset.Execute(engine);
            }
            else
            {
                new RuleAuthoringTool(fileBaseRulesRepository).Create(ruleSetFile, typeof(Fact));
            }
        }
    }
}