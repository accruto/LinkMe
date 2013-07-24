using System;
using System.Reflection;

namespace LinkMe.Environment.Util.Commands
{
    public class RegCommand
        : UtilCommand
    {
        public override void Execute()
        {
            var assembly = Options["i"].Values[0];

            if (Register(assembly))
                Console.WriteLine("Registered '" + assembly + "'.");
        }

        protected static bool Register(string fullPath)
        {
            // It may be a .NET DLL or a COM dll. Try .NET first.

            Assembly assembly = null;
            try
            {
                // Use LoadFile since the assembly is just being examined and not executed.

                assembly = Assembly.LoadFile(fullPath);
            }
            catch (System.BadImageFormatException)
            {
            }

            // Check based on whether it is an assembly or not.

            if (assembly != null)
            {
                if (ComUtil.CanRegisterForInterop(assembly))
                {
                    ComUtil.RegisterForInterop(assembly);
                    return true;
                }
            }

            return false;
        }
    }
}