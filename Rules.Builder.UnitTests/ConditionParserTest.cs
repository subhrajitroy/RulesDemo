using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var content = File.ReadAllText("C:\\Users\\trainline\\conditions.json");
            var condition = new ConditionParser().Parse(content);
            Assert.NotNull(condition);
            Console.WriteLine(JsonConvert.SerializeObject(condition,Formatting.Indented));
        }
    }
}