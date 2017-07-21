using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace WorkflowFoundationRules
{
    public class Options
    {
        [Option('r',"Run Rule",Required = false,DefaultValue = false,
            HelpText = "Option to run a rule.You need to specify the rule fine with -f and events file with -e")]
        public bool RunRule { get; set; }

        [Option('f', "Rule file to run", Required = false, DefaultValue = "",
            HelpText = "File path of workflow generated rule file using either the UI tool or command line")]
        public string RulesFile { get; set; }


        [Option('i', "Interactive UI", Required = false, DefaultValue = false,
            HelpText = "Launches Workflow Rule Authoring tool")]
        public bool LaunchUi { get; set; }


        [Option('c', "Create Rule", Required = false, DefaultValue = false,
            HelpText = "Create rule file understood by Workflow rule engine.Input is granite rule file given as -g")]
        public bool CreateRuleSetFromFile { get; set; }

        [Option('e', "Events File", Required = false, DefaultValue = "",
            HelpText = "File containig events data")]
        public string EventsFile { get; set; }

        [Option('g', "Granite Rules File", Required = false, DefaultValue = "",
            HelpText = "Json file containg rule definitions in granite format")]
        public string GraniteRulesFile { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}

