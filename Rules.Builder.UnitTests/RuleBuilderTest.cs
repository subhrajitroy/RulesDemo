using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Rules.Authoring;
using Rules.Domain;

namespace Rules.Builder.UnitTests
{
    [TestFixture]
    public class RuleBuilderTest
    {
        [Test]
        public void ShouldBuildExpectedRule()
        {
            var condition = "this.HasFactWithEqualValue(\"State\",\"Alarm\")";
            var cond2 = "this.HasFactWithGreaterValue(\"criticalSensors\",10)";
            var ruleSet = new GraniteRuleSet();
            ruleSet.Id = Guid.NewGuid();
            ruleSet.IsActive = true;
            ruleSet.LastUpdated = DateTime.Now;
            ruleSet.LastUpdatedBy = "me@me.com";
            ruleSet.Name = "Single Device Rule";
            ruleSet.Version = "0.0.0.1";
            var rule = new GraniteRule();
            rule.Id = Guid.NewGuid();
            rule.Condition = cond2;
            rule.IsActive = true;
            rule.Name = "Alarm trigger rule";

            var triggerAction = GetTriggerAction();

            rule.ActionDetails.Add(triggerAction);
            ruleSet.Rules.Add(rule);

            var currentContextWorkDirectory = TestContext.CurrentContext.WorkDirectory;

            File.WriteAllText($"{currentContextWorkDirectory}\\granite.rules",ruleSet.ToString());

            var workflowRuleSet = new RuleBuilder().Build(ruleSet);
            Assert.That(workflowRuleSet.Rules.Count,Is.EqualTo(1));
        }

        [Test]
        public void GenerateTriggerAction()
        {
            var actionAsJson = JsonConvert.SerializeObject(GetTriggerAction(),Formatting.Indented);
            var currentContextWorkDirectory = TestContext.CurrentContext.WorkDirectory;
            Console.WriteLine(currentContextWorkDirectory);
            File.WriteAllText($"{currentContextWorkDirectory}\\action.json",actionAsJson);
        }

        private static TriggerAction GetTriggerAction()
        {
            var triggerAction = new TriggerAction();
            triggerAction.Name = "Trigger Alarm";
            triggerAction.ActionType = ActionType.Notification;
            triggerAction.ExecutorId = Guid.NewGuid();
            triggerAction.Schedule = new ActionSchedule();

            var emailActionDetail = new EmailActionDetail();
            emailActionDetail.Receivers.Add(new UserEmail("user@gmail.com"));


            triggerAction.ActionDetail
                = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(emailActionDetail));
            return triggerAction;
        }


        private static string ReadRuleSetJson(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
