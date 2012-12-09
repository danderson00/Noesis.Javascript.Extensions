using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;
using Noesis.Javascript.Dynamic;

namespace Noesis.Javascript.Extensions.Tests
{
    [TestFixture]
    public class JContextTests
    {
        [Test]
        public void Evaluate_parses_and_returns_scalars()
        {
            JContext context = new JContext();
            Assert.AreEqual("testvalue", context.Evaluate("var test = 'testvalue'; test"));
            Assert.AreEqual(1, context.Evaluate("var test = 1; test"));
            Assert.IsTrue(context.Evaluate("var test = true; test"));
        }

        [Test]
        public void Evaluate_parses_and_returns_dynamic_object()
        {
            dynamic result = new JContext().Evaluate(@"var test = { stringValue: 'value', child: { integerValue: 2, secondChild: { booleanValue: true } } }; test");
            Assert.AreEqual("value", result.stringValue);
            Assert.AreEqual(2, result.child.integerValue);
            Assert.IsTrue(result.child.secondChild.booleanValue);
        }

        [Test]
        public void Invoking_JContext_object_runs_scripts_passed()
        {
            dynamic context = new JContext();
            context("Test1 = 'test1'", "Test2 = 2");
            Assert.AreEqual("test1", context.Evaluate("Test1"));
            Assert.AreEqual(2, context.Evaluate("Test2"));
        }

        [Test]
        public void Invoking_JContext_object_returns_result_of_first_script()
        {
            dynamic context = new JContext();
            var result = context("var test = 'first'; test", "var test = 'second'; test");
            Assert.AreEqual("first", result);
        }

        [Test]
        public void Invoking_member_executes_javascript_call_and_returns_results()
        {
            dynamic context = new JContext();
            context.Execute("function testFunction(string, integer, boolean) { return boolean === true && '-' + string + integer + '-'; }");
            Assert.AreEqual("-test2-", context.testFunction("test", 2, true));
        }

        [Test]
        public void Invoking_member_with_complex_object_maps_data_to_javascript_object()
        {
            dynamic context = new JContext();
            context.Execute("function testFunction(input) { return input.test; }");
            Assert.AreEqual("test1", context.testFunction(new { test = new { testValue = "test1"} }).testValue);
        }

        [Test]
        public void Getting_property_returns_global_variable()
        {
            dynamic context = new JContext();
            context.Execute("Test = 'test1'");
            Assert.AreEqual("test1", context.Test);
        }

        [Test]
        public void Setting_property_sets_global_variable()
        {
            dynamic context = new JContext();
            context.Test = "test1";
            Assert.AreEqual("test1", context.Evaluate("Test"));
        }
    }
}
