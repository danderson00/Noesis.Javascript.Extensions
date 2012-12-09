using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Noesis.Javascript.Dynamic;

namespace Noesis.Javascript.Dynamic.Tests
{
    [TestFixture]
    public class ContextExtensionsTests
    {
        [Test]
        public void LoadFromResources_loads_and_executes_embedded_resources_with_js_extension()
        {
            var context = new JavascriptContext().LoadFromResources(GetType().Assembly);
            context.GetParameter("Test").Should().Be("test");
        }
    }
}
