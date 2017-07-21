using System.IO;
using System.Workflow.Activities.Rules;
using Newtonsoft.Json;
using Rules.Authoring;
using Rules.Domain;

namespace WorkflowFoundationRules
{
    public class FileBasedRuleAuthoringTool
    {
        private readonly string _jsonFilePath;

        public FileBasedRuleAuthoringTool(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public RuleSet Create()
        {
            using (var reader = new StreamReader(_jsonFilePath))
            {
                var rulesInJson = reader.ReadToEnd();
                var graniteRuleSet = JsonConvert.DeserializeObject<GraniteRuleSet>(rulesInJson);
                return new RuleBuilder().Build(graniteRuleSet);
            }
        }
    }
}