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
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// A <see cref="IBuilderStrategy"/> that intercepts objects
    /// in the build chain by creating a proxy object.
    /// </summary>
    public class InstanceInterceptionStrategy : BuilderStrategy
    {
        /// <summary>
        /// Called during the chain of responsibility for a build operation. The
        /// PostBuildUp method is called when the chain has finished the PreBuildUp
        /// phase and executes in reverse order from the PreBuildUp calls.
        /// </summary>
        /// <param name="context">Context of the build operation.</param>
        public override void PostBuildUp(IBuilderContext context)
        {
            // If it's already been intercepted, don't do it again.
            if(context.Existing is IInterceptingProxy) return;

            Type originalType;
            if (!BuildKey.TryGetType(context.OriginalBuildKey, out originalType)) return;

            Type typeToIntercept;
            IInstanceInterceptionPolicy interceptionPolicy = FindInterceptorPolicy(context, out typeToIntercept);

            if (interceptionPolicy != null)
            {
                IInstanceInterceptor interceptor = interceptionPolicy.Interceptor;
                if (interceptor.CanIntercept(typeToIntercept))
                {
                    IUnityContainer container = BuilderContext.NewBuildUp<IUnityContainer>(context);
                    InjectionPolicy[] policies = BuilderContext.NewBuildUp<InjectionPolicy[]>(context);
                    PolicySet allPolicies = new PolicySet(policies);
                    IInterceptingProxy proxy = interceptor.CreateProxy(typeToIntercept, context.Existing);
                    bool hasHandlers = false;
                    foreach (MethodImplementationInfo method in interceptor.GetInterceptableMethods(typeToIntercept, context.Existing.GetType()))
                    {
                        HandlerPipeline pipeline = new HandlerPipeline(allPolicies.GetHandlersFor(method, container));
                        if(pipeline.Count > 0)
                        {
                            proxy.SetPipeline(interceptor.MethodInfoForPipeline(method), pipeline);
                            hasHandlers = true;
                        }
                    }
                    if(hasHandlers)
                    {
                        context.Existing = proxy;
                    }
                }
            }
        }

        private static IInstanceInterceptionPolicy FindInterceptorPolicy(IBuilderContext context, out Type typeToIntercept)
        {
            typeToIntercept = null;

            Type currentType = BuildKey.GetType(context.BuildKey);
            Type originalType = BuildKey.GetType(context.OriginalBuildKey);

            // First, try for a match against the current build key
            IInstanceInterceptionPolicy policy;

            policy = context.Policies.Get<IInstanceInterceptionPolicy>(context.BuildKey, false) ??
                context.Policies.Get<IInstanceInterceptionPolicy>(currentType, false);
            if(policy != null)
            {
                typeToIntercept = currentType;
                return policy;
            }

            // Next, try the original build key

            policy = context.Policies.Get<IInstanceInterceptionPolicy>(context.OriginalBuildKey, false) ??
                context.Policies.Get<IInstanceInterceptionPolicy>(originalType, false);

            if(policy != null)
            {
                typeToIntercept = originalType;
            }

            return policy;
        }
    }
}
