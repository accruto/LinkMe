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
using Microsoft.Practices.ObjectBuilder2.Tests.TestDoubles;
using Microsoft.Practices.ObjectBuilder2.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.ObjectBuilder2.Tests
{
    [TestClass]
    public class DynamicMethodConstructionFixture
    {
        [TestMethod]
        public void CanBuildUpObjectWithDefaultConstructorViaBuildPlan()
        {
            TestingBuilderContext context = GetContext();
            IBuildPlanPolicy plan = GetPlanCreator(context).CreatePlan(context, typeof(NullLogger));
            context.BuildKey = typeof(NullLogger);
            plan.BuildUp(context);

            object result = context.Existing;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NullLogger));
        }

        [TestMethod]
        public void CanResolveSimpleParameterTypes()
        {
            TestingBuilderContext context = GetContext();
            SingletonLifetimePolicy lifetimePolicy = new SingletonLifetimePolicy();
            lifetimePolicy.SetValue("C:\\Log.txt");
            context.Policies.Set<ILifetimePolicy>(lifetimePolicy, typeof(string));

            IBuildPlanPolicy plan = GetPlanCreator(context).CreatePlan(context, typeof(FileLogger));
            context.BuildKey = typeof(FileLogger);

            plan.BuildUp(context);
            object result = context.Existing;
            FileLogger logger = result as FileLogger;

            Assert.IsNotNull(result);
            Assert.IsNotNull(logger);
            Assert.AreEqual("C:\\Log.txt", logger.LogFile);
        }

        [TestMethod]
        public void GetParameterNameInExceptionMessageOnFailedResolution()
        {
            TestingBuilderContext context = TestingBuilderContext.GetFullContext();

            try
            {
                context.ExecuteBuildUp(typeof(FileLogger), null);
                Assert.Fail();
            }
            catch (BuildFailedException ex)
            {
                Assert.IsTrue(ex.Message.Contains("logFile"));
                return;
            }
        }

        [TestMethod]
        public void GetProperExceptionMessageWhenBuildingInterface()
        {
            TestingBuilderContext context = TestingBuilderContext.GetFullContext();
            try
            {
                context.ExecuteBuildUp(typeof(ISomethingOrOther), null);
                Assert.Fail();
            }
            catch(BuildFailedException ex)
            {
                Assert.IsTrue(ex.Message.Contains("is an interface"));
                return;
            }
        }

        [TestMethod]
        public void ExistingObjectIsUntouchedByConstructionPlan()
        {
            TestingBuilderContext context = GetContext();
            IBuildPlanPolicy plan = GetPlanCreator(context).CreatePlan(context, typeof(OptionalLogger));

            OptionalLogger existing = new OptionalLogger("C:\\foo.bar");

            context.BuildKey = typeof(OptionalLogger);
            context.Existing = existing;

            plan.BuildUp(context);
            object result = context.Existing;

            Assert.AreSame(existing, result);
            Assert.AreEqual("C:\\foo.bar", existing.LogFile);
        }

        [TestMethod]
        public void CanCreateObjectWithoutExplicitConstructorDefined()
        {
            TestingBuilderContext context = GetContext();
            IBuildPlanPolicy plan =
                GetPlanCreator(context).CreatePlan(context,
                    typeof(InternalObjectWithoutExplicitConstructor));

            context.BuildKey = typeof(InternalObjectWithoutExplicitConstructor);
            plan.BuildUp(context);
            InternalObjectWithoutExplicitConstructor result =
                (InternalObjectWithoutExplicitConstructor)context.Existing;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(MethodAccessException))]
        public void CannotCreatePlanForPrivateClass()
        {
            TestingBuilderContext context = GetContext();
            IBuildPlanPolicy plan =
                GetPlanCreator(context).CreatePlan(context,
                    typeof(PrivateClassWithoutExplicitConstructor));

            context.BuildKey = typeof(PrivateClassWithoutExplicitConstructor);
            plan.BuildUp(context);
        }

        private TestingBuilderContext GetContext()
        {
            StagedStrategyChain<BuilderStage> chain = new StagedStrategyChain<BuilderStage>();
            chain.AddNew<DynamicMethodConstructorStrategy>(BuilderStage.Creation);

            DynamicMethodBuildPlanCreatorPolicy policy =
                new DynamicMethodBuildPlanCreatorPolicy(chain);

            TestingBuilderContext context = new TestingBuilderContext();

            context.Strategies.Add(new LifetimeStrategy());

            context.PersistentPolicies.SetDefault<IConstructorSelectorPolicy>(
                new ConstructorSelectorPolicy<InjectionConstructorAttribute>());
            context.PersistentPolicies.SetDefault<IDynamicBuilderMethodCreatorPolicy>(
                new DefaultDynamicBuilderMethodCreatorPolicy());
            context.PersistentPolicies.SetDefault<IBuildPlanCreatorPolicy>(policy);

            return context;
        }

        private IBuildPlanCreatorPolicy GetPlanCreator(IBuilderContext context)
        {
            return context.Policies.Get<IBuildPlanCreatorPolicy>(null);
        }

        internal class InternalObjectWithoutExplicitConstructor
        {
            public void DoSomething()
            {
                // We do nothing!
            }
        }

        private class PrivateClassWithoutExplicitConstructor
        {
            public void DoNothing()
            {
                // Again, do nothing
            }
        }

        interface ISomethingOrOther
        {
            
        }
    }
}
