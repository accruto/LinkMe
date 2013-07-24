using System;
using System.CodeDom.Compiler;
using System.Net.Mime;
using System.Reflection;
using Microsoft.CSharp;

namespace LinkMe.Framework.Content.Templates
{
    internal class MergeCompiler
    {
        public static MethodInfo Compile(string code, string mimeType, string method, MergeSettings settings)
        {
            if (code == null)
                throw new ArgumentNullException("code");
            if (method == null)
                throw new ArgumentNullException("method");

            // Create the assembly and then find the method.

            Assembly assembly = CreateAssembly(code, mimeType, settings);
            return GetMethodInfo(assembly, method);
        }

        private static Assembly CreateAssembly(string code, string mimeType, MergeSettings settings)
        {
            // Create an in-memory assembly from the source code.

            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.IncludeDebugInformation = false;
            AddReferences(parameters, mimeType, settings);

            // Compile the code.

            CodeDomProvider provider = new CSharpCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            // Look for errors.

            if (results.Errors.Count > 0)
                throw new ApplicationException("Compile Error: " + results.Errors[0].ErrorText);

            // Return the compiled assembly.

            return results.CompiledAssembly;
        }

        private static void AddReferences(CompilerParameters parameters, string mimeType, MergeSettings settings)
        {
            // Add standard references.

            parameters.ReferencedAssemblies.Add("mscorlib.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Xml.dll");

            if (mimeType == MediaTypeNames.Text.Html)
            {
                if (!settings.References.Contains("System.Web.dll"))
                    parameters.ReferencedAssemblies.Add("System.Web.dll");
            }

            // Add any aditional references that are needed.

            foreach (string reference in settings.References)
                parameters.ReferencedAssemblies.Add(reference);
        }

        private static MethodInfo GetMethodInfo(Assembly assembly, string entry)
        {
            // Return the method for the first type that is found with it.

            foreach (Type type in assembly.GetTypes())
            {
                MethodInfo methodInfo = type.GetMethod(entry, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                if (methodInfo != null)
                    return methodInfo;
            }

            return null;
        }
    }
}