using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using Rules.Authoring;

namespace Rules.Builder.UnitTests
{
    [TestFixture]
    public class ConditionParserTest
    {
        [Test]
        public void ShouldParse()
        {
            var content = File.ReadAllText("C:\\Projects\\conditions.json");
            var condition = new ConditionParser().Parse(content);
            Assert.NotNull(condition);
            Console.WriteLine(JsonConvert.SerializeObject(condition,Formatting.Indented));
        }
    }
}