using System;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace WorkflowFoundationRules
{
    public class FileBaseRulesRepository : IRulesRepository
    {
        public  RuleSet Load(string ruleName)
        {
            XmlTextReader reader = new XmlTextReader(ruleName);
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            object results = serializer.Deserialize(reader);
            RuleSet ruleset = (RuleSet) results;

            if (ruleset == null)
            {
                Console.WriteLine("The rules file " + ruleName + " does not appear to contain a valid ruleset.");
            }
            return ruleset;
        }

        public void Save(string filename, RuleSet ruleset)
        {
            XmlTextWriter writer = new XmlTextWriter(filename, null);
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            serializer.Serialize(writer, ruleset);
            Console.WriteLine("Wrote rules file: " + filename);
        }
    }
}