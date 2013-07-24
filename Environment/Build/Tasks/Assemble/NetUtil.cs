using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    public class NetUtil
    {
        public static List<string> GetReferencedAssemblies(string fullPath)
        {
            Assembly assembly = null;
            try
            {
                // Use LoadFile since the assembly is just being examined and not executed.

                assembly = Assembly.LoadFile(fullPath);
            }
            catch (System.BadImageFormatException)
            {
            }
            catch (FileNotFoundException)
            {
            }

            if ( assembly == null )
                return null;

            List<string> assemblies = new List<string>();
            foreach ( AssemblyName assemblyName in assembly.GetReferencedAssemblies() )
                assemblies.Add(assemblyName.Name);

            return assemblies;
        }
    }
}