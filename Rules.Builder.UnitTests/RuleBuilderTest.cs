using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rules.Authoring;

namespace Rules.Builder.UnitTests
{
    [TestFixture]
    public class RuleBuilderTest
    {
        [Test]
        public void ShouldBuildExpectedRule()
        {
            var methodReferenceExpression = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "Get");
            var methodInvokeExpression = new CodeMethodInvokeExpression(methodReferenceExpression, new CodeExpression[] { new CodePrimitiveExpression("State") });
            var codeMethodReferenceExpression = new CodeMethodReferenceExpression(methodInvokeExpression, "Equals");
            var codeMethodInvokeExpression = new CodeMethodInvokeExpression(codeMethodReferenceExpression,new CodeExpression[]{new CodePrimitiveExpression("Alarm")});
            Console.WriteLine(codeMethodInvokeExpression);
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
