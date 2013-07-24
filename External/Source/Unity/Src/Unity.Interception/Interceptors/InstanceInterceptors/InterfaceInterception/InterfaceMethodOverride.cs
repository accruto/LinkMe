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
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    class InterfaceMethodOverride
    {
        private readonly TypeBuilder typeBuilder;
        private readonly MethodInfo methodToOverride;
        private readonly ParameterInfo[] methodParameters;
        private readonly FieldBuilder pipelineField;
        private readonly FieldBuilder targetField;
        private readonly int overrideCount;

        public InterfaceMethodOverride(TypeBuilder typeBuilder, FieldBuilder pipelineField, FieldBuilder targetField, MethodInfo methodToOverride, int overrideCount)
        {
            this.typeBuilder = typeBuilder;
            this.pipelineField = pipelineField;
            this.targetField = targetField;
            this.methodToOverride = methodToOverride;
            methodParameters = methodToOverride.GetParameters();
            this.overrideCount = overrideCount;
        }

        public MethodBuilder AddMethod()
        {
            MethodBuilder delegateMethod = CreateDelegateImplementation();
            return CreateMethodOverride(delegateMethod);
        }

        private string CreateMethodName(string purpose)
        {
            return "<" + methodToOverride.Name + "_" + purpose + ">__" +
                overrideCount.ToString(CultureInfo.InvariantCulture);
        }

        private void SetupGenericParameters(MethodBuilder methodBuilder)
        {
            if(methodToOverride.IsGenericMethod)
            {
                Type[] genericArguments = methodToOverride.GetGenericArguments();
                string[] names = Seq.Make(genericArguments)
                    .Map<string>(delegate(Type t) { return t.Name; })
                    .ToArray();
                GenericTypeParameterBuilder[] builders = methodBuilder.DefineGenericParameters(names);
                for(int i = 0; i < genericArguments.Length; ++i)
                {
                    builders[i].SetGenericParameterAttributes(genericArguments[i].GenericParameterAttributes);

                    foreach (Type type in genericArguments[i].GetGenericParameterConstraints())
                    {
                        builders[i].SetBaseTypeConstraint(type);
                    }
                }
            }
        }

        private static readonly OpCode[] loadArgsOpcodes = {
            OpCodes.Ldarg_1,
            OpCodes.Ldarg_2,
            OpCodes.Ldarg_3
        };

        private static void EmitLoadArgument(ILGenerator il, int argumentNumber)
        {
            if(argumentNumber < loadArgsOpcodes.Length)
            {
                il.Emit(loadArgsOpcodes[argumentNumber]);
            }
            else
            {
                il.Emit(OpCodes.Ldarg, argumentNumber + 1);
            }
        }

        private static readonly OpCode[] loadConstOpCodes = {
            OpCodes.Ldc_I4_0,
            OpCodes.Ldc_I4_1,
            OpCodes.Ldc_I4_2,
            OpCodes.Ldc_I4_3,
            OpCodes.Ldc_I4_4,
            OpCodes.Ldc_I4_5,
            OpCodes.Ldc_I4_6,
            OpCodes.Ldc_I4_7,
            OpCodes.Ldc_I4_8,
        };

        private static void EmitLoadConstant(ILGenerator il, int i)
        {
            if(i < loadConstOpCodes.Length)
            {
                il.Emit(loadConstOpCodes[i]);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, i);
            }
        }

        private static void EmitBox(ILGenerator il, Type typeOnStack)
        {
            if( typeOnStack.IsValueType || typeOnStack.IsGenericParameter)
            {
                il.Emit(OpCodes.Box, typeOnStack);
            }
        }

        private static void EmitUnboxOrCast(ILGenerator il, Type targetType)
        {
            if(targetType.IsValueType || targetType.IsGenericParameter)
            {
                il.Emit(OpCodes.Unbox_Any, targetType);
            }
            else
            {
                il.Emit(OpCodes.Castclass, targetType);
            }
        }

        private MethodBuilder CreateDelegateImplementation()
        {
            string methodName = CreateMethodName("DelegateImplementation");

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName,
                MethodAttributes.Private | MethodAttributes.HideBySig);
            List<LocalBuilder> outOrRefLocals = new List<LocalBuilder>();

            SetupGenericParameters(methodBuilder);

            methodBuilder.SetReturnType(typeof(IMethodReturn));
            // Adding parameters
            methodBuilder.SetParameters(typeof(IMethodInvocation), typeof(GetNextHandlerDelegate));
            // Parameter 
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "inputs");
            // Parameter 
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "getNext");

            methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(CompilerGeneratedAttributeMethods.CompilerGeneratedAttribute, new object[0]));

            ILGenerator il = methodBuilder.GetILGenerator();
            Label done = il.DefineLabel();
            LocalBuilder ex = il.DeclareLocal(typeof(Exception));

            LocalBuilder baseReturn = null;
            LocalBuilder parameters = null;

            if (MethodHasReturnValue)
            {
                baseReturn = il.DeclareLocal(methodToOverride.ReturnType);
            }
            LocalBuilder retval = il.DeclareLocal(typeof(IMethodReturn));

            il.BeginExceptionBlock();
            // Call the target method
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, targetField);

            if(methodParameters.Length > 0)
            {
                parameters = il.DeclareLocal(typeof (IParameterCollection));
                il.Emit(OpCodes.Ldarg_1);
                il.EmitCall(OpCodes.Callvirt, IMethodInvocationMethods.GetArguments, null);
                il.Emit(OpCodes.Stloc, parameters);

                for(int i = 0; i < methodParameters.Length; ++i)
                {
                    il.Emit(OpCodes.Ldloc, parameters);
                    EmitLoadConstant(il, i);
                    il.EmitCall(OpCodes.Callvirt, IListMethods.GetItem, null);
                    Type parameterType = methodParameters[i].ParameterType;

                    if(parameterType.IsByRef)
                    {
                        Type elementType = parameterType.GetElementType();
                        LocalBuilder refShadowLocal = il.DeclareLocal(elementType);
                        outOrRefLocals.Add(refShadowLocal);
                        EmitUnboxOrCast(il, elementType);
                        il.Emit(OpCodes.Stloc, refShadowLocal);
                        il.Emit(OpCodes.Ldloca, refShadowLocal);
                    }
                    else
                    {
                        EmitUnboxOrCast(il, parameterType);
                    }
                }
            }

            il.Emit(OpCodes.Callvirt, methodToOverride);
            if (MethodHasReturnValue)
            {
                il.Emit(OpCodes.Stloc, baseReturn);
            }

            // Generate  the return value
            il.Emit(OpCodes.Ldarg_1);
            if (MethodHasReturnValue)
            {
                il.Emit(OpCodes.Ldloc, baseReturn);
                EmitBox(il, ReturnType);
            }
            else
            {
                il.Emit(OpCodes.Ldnull);
            }
            EmitLoadConstant(il, methodParameters.Length);
            il.Emit(OpCodes.Newarr, typeof(object));

            if(methodParameters.Length > 0)
            {
                LocalBuilder outputArguments = il.DeclareLocal(typeof (object[]));
                il.Emit(OpCodes.Stloc, outputArguments);

                int outputArgNum = 0;
                for(int i = 0; i < methodParameters.Length; ++i)
                {
                    il.Emit(OpCodes.Ldloc, outputArguments);
                    EmitLoadConstant(il, i);

                    Type parameterType = methodParameters[i].ParameterType;
                    if(parameterType.IsByRef)
                    {
                        parameterType = parameterType.GetElementType();
                        il.Emit(OpCodes.Ldloc, outOrRefLocals[outputArgNum++]);
                        EmitBox(il, parameterType);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldloc, parameters);
                        EmitLoadConstant(il, i);
                        il.Emit(OpCodes.Callvirt, IListMethods.GetItem);
                    }
                    il.Emit(OpCodes.Stelem_Ref);
                }
                il.Emit(OpCodes.Ldloc, outputArguments);
            }

            il.Emit(OpCodes.Callvirt, IMethodInvocationMethods.CreateReturn);
            il.Emit(OpCodes.Stloc, retval);
            il.BeginCatchBlock(typeof(Exception));
            il.Emit(OpCodes.Stloc, ex);
            // Create an exception return
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldloc, ex);
            il.EmitCall(OpCodes.Callvirt, IMethodInvocationMethods.CreateExceptionMethodReturn, null);
            il.Emit(OpCodes.Stloc, retval);
            il.EndExceptionBlock();
            il.MarkLabel(done);
            il.Emit(OpCodes.Ldloc, retval);
            il.Emit(OpCodes.Ret);
            return methodBuilder;
        }

        private MethodBuilder CreateMethodOverride(MethodBuilder delegateMethod)
        {
            MethodAttributes attrs = MethodAttributes.Public | 
                MethodAttributes.Virtual | MethodAttributes.Final |
                MethodAttributes.HideBySig | MethodAttributes.NewSlot;

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodToOverride.Name, attrs);

            SetupGenericParameters(methodBuilder);

            methodBuilder.SetReturnType(methodToOverride.ReturnType);
            methodBuilder.SetParameters(
                Seq.Make(methodParameters)
                    .Map<Type>(delegate(ParameterInfo pi) { return pi.ParameterType; })
                    .ToArray());

            int paramNum = 1;
            foreach(ParameterInfo pi in methodParameters)
            {
                methodBuilder.DefineParameter(paramNum++, pi.Attributes, pi.Name);
            }

            ILGenerator il = methodBuilder.GetILGenerator();

            LocalBuilder methodReturn = il.DeclareLocal(typeof(IMethodReturn));
            LocalBuilder ex = il.DeclareLocal(typeof(Exception));
            LocalBuilder pipeline = il.DeclareLocal(typeof (HandlerPipeline));
            LocalBuilder parameterArray = il.DeclareLocal(typeof (object[]));
            LocalBuilder inputs = il.DeclareLocal(typeof (VirtualMethodInvocation));

            // Get pipeline for this method onto the stack
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, pipelineField);
            il.Emit(OpCodes.Ldc_I4, methodToOverride.MetadataToken);
            il.EmitCall(OpCodes.Call, PipelineManagerMethods.GetPipeline, null);
            il.Emit(OpCodes.Stloc, pipeline);

            // Create instance of VirtualMethodInvocation
            il.Emit(OpCodes.Ldarg_0); // target object

            il.Emit(OpCodes.Ldtoken, methodToOverride);
            if(methodToOverride.DeclaringType.IsGenericType)
            {
                il.Emit(OpCodes.Ldtoken, methodToOverride.DeclaringType);
                il.Emit(OpCodes.Call, MethodBaseMethods.GetMethodForGenericFromHandle);
            }
            else
            {
                il.Emit(OpCodes.Call, MethodBaseMethods.GetMethodFromHandle); // target method
            }

            EmitLoadConstant(il, methodParameters.Length);
            il.Emit(OpCodes.Newarr, typeof(object)); // object[] parameters
            if (methodParameters.Length > 0)
            {
                il.Emit(OpCodes.Stloc, parameterArray);

                for (int i = 0; i < methodParameters.Length; ++i)
                {
                    il.Emit(OpCodes.Ldloc, parameterArray);
                    EmitLoadConstant(il, i);
                    EmitLoadArgument(il, i);
                    Type elementType = methodParameters[i].ParameterType;
                    if(elementType.IsByRef)
                    {
                        elementType = methodParameters[i].ParameterType.GetElementType();
                        il.Emit(OpCodes.Ldobj, elementType);
                    }
                    EmitBox(il, elementType);
                    il.Emit(OpCodes.Stelem_Ref);
                }

                il.Emit(OpCodes.Ldloc, parameterArray);
            }
            il.Emit(OpCodes.Newobj, VirtualMethodInvocationMethods.VirtualMethodInvocation);
            il.Emit(OpCodes.Stloc, inputs);

            il.Emit(OpCodes.Ldloc, pipeline);
            il.Emit(OpCodes.Ldloc, inputs);

            // Put delegate reference onto the stack
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldftn, delegateMethod);
            il.Emit(OpCodes.Newobj, InvokeHandlerDelegateMethods.InvokeHandlerDelegate);

            // And call the pipeline
            il.Emit(OpCodes.Call, HandlerPipelineMethods.Invoke);

            il.Emit(OpCodes.Stloc, methodReturn);

            // Was there an exception?
            Label noException = il.DefineLabel();
            il.Emit(OpCodes.Ldloc, methodReturn);
            il.EmitCall(OpCodes.Callvirt, IMethodReturnMethods.GetException, null);
            il.Emit(OpCodes.Stloc, ex);
            il.Emit(OpCodes.Ldloc, ex);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Brtrue_S, noException);
            il.Emit(OpCodes.Ldloc, ex);
            il.Emit(OpCodes.Throw);

            il.MarkLabel(noException);

            // Unpack any ref/out parameters
            if(methodParameters.Length > 0)
            {
                int outputArgNum = 0;
                for (paramNum = 0; paramNum < methodParameters.Length; ++paramNum)
                {
                    ParameterInfo pi = methodParameters[paramNum];
                    if (pi.ParameterType.IsByRef)
                    {
                        // Get the original parameter value - address of the ref or out
                        EmitLoadArgument(il, paramNum);

                        // Get the value of this output parameter out of the Outputs collection
                        il.Emit(OpCodes.Ldloc, methodReturn);
                        il.Emit(OpCodes.Callvirt, IMethodReturnMethods.GetOutputs);
                        EmitLoadConstant(il, outputArgNum++);
                        il.Emit(OpCodes.Callvirt, IListMethods.GetItem);
                        EmitUnboxOrCast(il, pi.ParameterType.GetElementType());

                        // And store in the caller
                        il.Emit(OpCodes.Stobj, pi.ParameterType.GetElementType());
                    }
                }
            }

            if (MethodHasReturnValue)
            {
                il.Emit(OpCodes.Ldloc, methodReturn);
                il.EmitCall(OpCodes.Callvirt, IMethodReturnMethods.GetReturnValue, null);
                EmitUnboxOrCast(il, ReturnType);
            }
            il.Emit(OpCodes.Ret);

            return methodBuilder;
        }

        private bool MethodHasReturnValue
        {
            get { return methodToOverride.ReturnType != typeof(void); }
        }

        private Type ReturnType
        {
            get { return methodToOverride.ReturnType; }
        }    }
}
