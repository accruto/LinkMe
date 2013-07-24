using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace LinkMe.Environment.Build.Tasks
{
    internal sealed class TypeComparer
        : IComparer<System.Type>
    {
        public static TypeComparer Default = new TypeComparer();

        public int Compare(System.Type typeA, System.Type typeB)
        {
            if ( typeA == null )
                return typeB != null ? -1 : 0;
            if ( typeB == null )
                return 1;
            return string.Compare(typeA.FullName, typeB.FullName, System.StringComparison.Ordinal);
        }

        public bool Equals(System.Type typeA, System.Type typeB)
        {
            return typeA == typeB;
        }

        public int GetHashCode(System.Type type)
        {
            return type.GetHashCode();
        }
    }

    internal sealed class RegistrationAttributeComparer
        : IComparer<RegistrationAttribute>
    {
        public static RegistrationAttributeComparer Default = new RegistrationAttributeComparer();

        public int Compare(RegistrationAttribute attributeA, RegistrationAttribute attributeB)
        {
            // Compare the types.

            int result = string.Compare(attributeA.GetType().FullName, attributeB.GetType().FullName, System.StringComparison.Ordinal);
            if ( result == 0 && attributeA.GetType() == attributeB.GetType() )
            {
                // Types are the same so compare the properties.

                foreach ( PropertyDescriptor property in TypeDescriptor.GetProperties(attributeA.GetType()) )
                {
                    // Try to compare using IComparable.

                    if ( typeof(System.IComparable).IsAssignableFrom(property.PropertyType) )
                    {
                        System.IComparable valueA = property.GetValue(attributeA) as System.IComparable;
                        System.IComparable valueB = property.GetValue(attributeB) as System.IComparable;

                        if ( valueA != null && valueB != null )
                        {
                            result = valueA.CompareTo(valueB);
                            if ( result != 0 )
                                return result;
                        }

                        continue;
                    }

                    // Compare the property types.

                    if ( property.PropertyType == typeof(System.Type) )
                    {
                        System.Type typeA = property.GetValue(attributeA) as System.Type;
                        System.Type typeB = property.GetValue(attributeB) as System.Type;

                        if ( typeA != null && typeB != null )
                        {
                            result = string.Compare(typeA.FullName, typeB.FullName, System.StringComparison.Ordinal);
                            if ( result != 0 )
                                return result;
                        }

                        continue;
                    }

                    // Compare the string representations of the values.

                    object objectA = property.GetValue(attributeA);
                    object objectB = property.GetValue(attributeB);
                    if ( objectA != null && objectB != null )
                    {
                        result = string.Compare(objectA.ToString(), objectB.ToString(), System.StringComparison.Ordinal);
                        if ( result != 0 )
                            return result;
                    }
                }
            }

            return result;
        }
    }

    public static class VsUtil
    {
        public static bool CanRegisterPackages(string fullPath)
        {
            // Try to get the registry root.

            string registryRoot = GetRegistryRoot(fullPath);
            if ( registryRoot == null )
                return false;

            return CanProcessPackages(fullPath);
        }

        private static bool CanProcessPackages(string fullPath)
        {
            // Look for registration types in the assembly.

            return GetRegistrationTypes(fullPath).Count != 0;
        }

        public static void RegisterPackages(string fullPath)
        {
            ProcessPackages(fullPath, true);
        }

        public static void UnregisterPackages(string fullPath)
        {
            ProcessPackages(fullPath, false);
        }

        private static void ProcessPackages(string fullPath, bool register)
        {
            // Try to get the registry root.

            string registryRoot = GetRegistryRoot(fullPath);
            if ( registryRoot == null )
                return;

            // Create the context to process the packages.

            using ( VsRegistryKey key = new VsRegistryKey(registryRoot) )
            {
                using ( VsRegistrationContext context = new VsRegistrationContext(key, RegistrationMethod.CodeBase) )
                {
                    ProcessPackages(fullPath, register, context);
                }
            }
        }

        private static void ProcessPackages(string fullPath, bool register, VsRegistrationContext context)
        {
            // Look for all the registration types in the assembly.

            SortedList<System.Type, List<RegistrationAttribute>> registrationTypes = GetRegistrationTypes(fullPath);

            // Iterate over each type.

            foreach ( KeyValuePair<System.Type, List<RegistrationAttribute>> pair in registrationTypes )
            {
                pair.Value.Sort(RegistrationAttributeComparer.Default);
                foreach ( RegistrationAttribute attribute in pair.Value )
                {
                    context.SetType(pair.Key);
                    if ( register )
                        attribute.Register(context);
                    else
                        attribute.Unregister(context);
                }
            }
        }

        private static SortedList<System.Type, List<RegistrationAttribute>> GetRegistrationTypes(string fullPath)
        {
            // Look for all the registration types in the assembly.

            SortedList<System.Type, List<RegistrationAttribute>> registrationTypes = new SortedList<System.Type, List<RegistrationAttribute>>(TypeComparer.Default);

            Assembly assembly = Assembly.LoadFrom(fullPath);
            foreach ( System.Type type in assembly.GetTypes() )
            {
                if ( !type.IsAbstract )
                {
                    // Registration types are all RegistrationAttributes applied to classes.

                    foreach ( object attribute in type.GetCustomAttributes(true) )
                    {
                        if ( attribute is RegistrationAttribute )
                        {
                            // Add this attribute to the list.

                            if ( registrationTypes.ContainsKey(type) )
                            {
                                registrationTypes[type].Add(attribute as RegistrationAttribute);
                            }
                            else
                            {
                                List<RegistrationAttribute> attributes = new List<RegistrationAttribute>();
                                attributes.Add(attribute as RegistrationAttribute);
                                registrationTypes[type] = attributes;
                            }
                        }
                    }
                }
            }

            return registrationTypes;
        }

        private static string GetRegistryRoot(string fullPath)
        {
            System.Type[] types;
            try
            {
                types = Assembly.LoadFrom(fullPath).GetTypes();
            }
            catch (System.BadImageFormatException)
            {
                // Not an assembly.

                return null;
            }
            catch (ReflectionTypeLoadException)
            {
                return null;
            }

            // Look for a package type.

            foreach ( System.Type type in types )
            {
                if ( (typeof(IVsPackage).IsAssignableFrom(type) && type != typeof(Microsoft.VisualStudio.Shell.Package)) && !type.IsAbstract )
                {
                    // Look for the attribute defining the registry root.

                    foreach ( DefaultRegistryRootAttribute attribute in type.GetCustomAttributes(typeof(DefaultRegistryRootAttribute), true) )
                        return attribute.Root;
                }
            }

            return null;
        }
    }
}