﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Noesis.Javascript.Dynamic;

namespace Noesis.Javascript.Extensions.Tests
{
    [TestFixture]
    public class DictionaryConverterTests
    {
        [Test]
        public void Convert_returns_single_level_scalars()
        {
            var item = new Dictionary<string, object>
                           {
                               { "StringValue", "value" },
                               { "IntegerValue", 2 },
                               { "BooleanValue", true }
                           };
            dynamic result = item.AsDynamic();
            Assert.AreEqual("value", result.StringValue);
            Assert.AreEqual(2, result.IntegerValue);
            Assert.IsTrue(result.BooleanValue);
        }

        [Test]
        public void Convert_returns_nested_scalars()
        {
            var item = new Dictionary<string, object>
                           {
                               { "StringValue", "value" },
                               { "Child", new Dictionary<string, object>
                                   {
                                       { "IntegerValue", 2 },
                                       { "SecondChild", new Dictionary<string, object> { { "BooleanValue", true } } }
                                   }
                                }
                           };
            dynamic result = item.AsDynamic();
            Assert.AreEqual("value", result.StringValue);
            Assert.AreEqual(2, result.Child.IntegerValue);
            Assert.IsTrue(result.Child.SecondChild.BooleanValue);
        }
    }
}
