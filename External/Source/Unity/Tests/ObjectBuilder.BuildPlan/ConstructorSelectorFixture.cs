﻿//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2.Tests.TestDoubles;
using Microsoft.Practices.ObjectBuilder2.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.ObjectBuilder2.Tests
{
    /// <summary>
    /// Tests for the default ConstructorSelectorPolicy
    /// </summary>
    [TestClass]
    public class ConstructorSelectorFixture
    {
        [TestMethod]
        public void SelectorPicksDefaultConstructor()
        {
            IConstructorSelectorPolicy policy = CreateSelector();
            ConstructorInfo expectedCtor = GetConstructor<object>();
            TestingBuilderContext context = GetContext<object>();
            SelectedConstructor result = policy.SelectConstructor(context);

            Assert.AreEqual(expectedCtor, result.Constructor);
        }

        [TestMethod]
        public void SelectorPicksConstructorWithAttribute()
        {
            IConstructorSelectorPolicy policy = CreateSelector();
            ConstructorInfo expected = GetConstructor<ObjectWithMarkedConstructor>(typeof(string));

            SelectedConstructor result = policy.SelectConstructor(GetContext<ObjectWithMarkedConstructor>());

            Assert.AreEqual(expected, result.Constructor);
        }

        [TestMethod]
        public void SelectorPicksLongestConstructor()
        {
            IConstructorSelectorPolicy policy = CreateSelector();
            ConstructorInfo expected = GetConstructor<ObjectWithMultipleConstructors>(
                typeof(int), typeof(string));

            SelectedConstructor result =
                policy.SelectConstructor(GetContext<ObjectWithMultipleConstructors>());

            Assert.AreEqual(expected, result.Constructor);
        }

        [TestMethod]
        public void SelectorThrowsIfConstructorsAreAmbiguous()
        {
            IConstructorSelectorPolicy policy = CreateSelector();

            try
            {
                policy.SelectConstructor(GetContext<ObjectWithAmbiguousConstructors>());
            }
            catch(InvalidOperationException)
            {
                // If we got here we're ok
                return;
            }
            Assert.Fail("Expected exception did not occur");
        }

        [TestMethod]
        public void SelectorPicksMarkedConstructorEvenIfOtherwiseAmbiguous()
        {
            IConstructorSelectorPolicy policy = CreateSelector();
            ConstructorInfo expected = GetConstructor<ObjectWithAmbiguousMarkedConstructor>(
                typeof(string), typeof(string), typeof(int));

            SelectedConstructor result =
                policy.SelectConstructor(GetContext<ObjectWithAmbiguousMarkedConstructor>());

            Assert.AreEqual(expected, result.Constructor);
        }

        private static ConstructorSelectorPolicy<InjectionConstructorAttribute> CreateSelector()
        {
            return new ConstructorSelectorPolicy<InjectionConstructorAttribute>();
        }

        private static ConstructorInfo GetConstructor<T>(params Type[] paramTypes)
        {
            return typeof(T).GetConstructor(paramTypes);
        }

        private static TestingBuilderContext GetContext<T>()
        {
            TestingBuilderContext context = new TestingBuilderContext();
            context.BuildKey = typeof(T);
            return context;
        }
    }
}
