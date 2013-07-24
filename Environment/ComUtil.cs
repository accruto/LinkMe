using System.Reflection;
using System.Runtime.InteropServices;

namespace LinkMe.Environment
{
    public static class ComUtil
    {
        public static bool CanRegisterForInterop(Assembly assembly)
        {
            if ( assembly == null )
                throw new System.ArgumentNullException("assembly");

            // Register assemblies that are explicitly marked:
            // - If [ComVisible(true)] at assembly level
            // - If [ComVisible(false)] at assembly level and contains a type explicitly marked as [ComVisible(true)]

            // Look for the [ComVisible] attribute on the assembly but not if it was imported from COM originally.

            object[] attributes = assembly.GetCustomAttributes(typeof(ComVisibleAttribute), false);
            if ( attributes.Length == 0 )
                return false;
            if ( assembly.GetCustomAttributes(typeof(ImportedFromTypeLibAttribute), false).Length > 0 )
                return false;

            // If the assembly value is true then register.

            if ( ((ComVisibleAttribute) attributes[0]).Value )
                return true;

            // Look for COM visible types within the assembly.

            System.Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch ( ReflectionTypeLoadException )
            {
                return false;
            }

            // Check each type.

            foreach ( System.Type type in types )
            {
                if ( Marshal.IsTypeVisibleFromCom(type) && !type.IsImport )
                    return true;
            }

            return false;
        }

        public static void RegisterForInterop(Assembly assembly)
        {
            if ( assembly == null )
                throw new System.ArgumentNullException("assembly");

            // Pass on to the registration services.

            RegistrationServices services = new RegistrationServices();
            services.RegisterAssembly(assembly, AssemblyRegistrationFlags.SetCodeBase);
        }

        public static void UnregisterForInterop(Assembly assembly)
        {
            if ( assembly == null )
                throw new System.ArgumentNullException("assembly");

            // Pass on to the registration services.

            RegistrationServices services = new RegistrationServices();
            services.UnregisterAssembly(assembly);
        }
    }
}