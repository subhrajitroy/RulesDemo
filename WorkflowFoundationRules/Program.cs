using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Workflow.Activities.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorkflowFoundationRules
{
    public class Program
    {
        private static readonly Dictionary<string, string> _rules;

        static Program()
        {
            _rules = new Dictionary<string, string> {{"thermostat", "thermostat.rules"}, {"das", "das.rules"}};
        }
        public static void Main(string[] args)
        {
            // Obtain or create ruleset
            var fileBaseRulesRepository = new FileBaseRulesRepository();
            var @event = ReadJsonFromFile(args[0]);
            var ruleSetName = @event.SourceId;
            var ruleSetFile = _rules[ruleSetName];

            var facts = new Facts();
            facts.AddAll(@event.Facts.Select(FromRawFact ));

            if (File.Exists(ruleSetFile))
            {
                // load file
                var ruleset = fileBaseRulesRepository.Load(ruleSetFile);
                var validation = new RuleValidation(typeof(Facts), null);
                var engine = new RuleExecution(validation, facts);
                ruleset.Execute(engine);
            }
            else
            {
                new RuleAuthoringTool(fileBaseRulesRepository).Create(ruleSetFile, typeof(Facts));
            }
        }

        private static Fact FromRawFact(RawFact rawFact)
        {
            if (rawFact.Type.Equals("int"))
            {
                return new IntegerFactBuilder().Build(rawFact);
            }
            return new StringFactBuilder().Build(rawFact);
        }

        public static Event ReadJsonFromFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                var content = reader.ReadToEnd();
               return JsonConvert.DeserializeObject<Event>(content);
            }
        }
    }
}