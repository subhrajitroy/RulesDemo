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

        public static void Main(string[] args)
        {
            var fileBaseRulesRepository = new FileBaseRulesRepository();

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                // Values are available here
                if (options.LaunchUi)
                {
                    new RuleAuthoringTool(fileBaseRulesRepository).Create($"{GetRandomFileName()}", typeof(Facts));
                }
                else if (options.CreateRuleSetFromFile)
                {
                    var graniteRulesFile = options.GraniteRulesFile;
                    var ruleSet = new FileBasedRuleAuthoringTool(graniteRulesFile).Create();
                    fileBaseRulesRepository.Save(GetRandomFileName(), ruleSet);
                }
                else if (options.RunRule)
                {
                    var rulesFile = options.RulesFile;
                    var rawFacts = ReadJsonFromFile(options.EventsFile);
                    var facts = new Facts();
                    facts.AddAll(rawFacts.Facts.Select(FromRawFact));
                    var ruleset = fileBaseRulesRepository.Load(rulesFile);
                    var validation = new RuleValidation(typeof(Facts), null);
                    var engine = new RuleExecution(validation, facts);
                    ruleset.Execute(engine);
                }
                else
                {
                    Console.WriteLine(options.GetUsage());
                }
            }
            else
            {
                Console.WriteLine(options.GetUsage());
            }
        }

        private static string GetRandomFileName()
        {
            var guid = Guid.NewGuid();
            var substring = guid.ToString().Substring(0, 6);
            return $"{substring}.rules";
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