using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    public class RemoveFromGac
        : Task
    {
        private readonly string[] _others = new string[] { "wix" };

        private string _productVersion;
        private string _assemblyPrefix;

        public string ProductVersion
        {
            get { return _productVersion ?? string.Empty; }
            set { _productVersion = value; }
        }

        [Required]
        public string AssemblyPrefix
        {
            get { return _assemblyPrefix ?? string.Empty; }
            set { _assemblyPrefix = value; }
        }

        public override bool Execute()
        {
            using (GacUtil gacutil = new GacUtil())
            {
                foreach (string assembly in gacutil.GetAllAssemblies())
                {
                    // Pull apart the name and the version.

                    int pos = assembly.IndexOf(',');
                    if (pos != -1)
                    {
                        string name = assembly.Substring(0, pos);
                        if (name.StartsWith(_assemblyPrefix, StringComparison.InvariantCultureIgnoreCase))
                        {
                            string rest = assembly.Substring(pos + 1);

                            if (rest.StartsWith(" Version="))
                            {
                                string version = rest.Substring(" Version=".Length);
                                if (version == _productVersion)
                                    gacutil.UninstallAssembly(assembly);
                            }
                        }
                        else if (Contains(_others, name))
                        {
                            // Ignore the version.

                            gacutil.UninstallAssembly(assembly);
                        }
                    }
                }
            }

            return true;
        }

        private static bool Contains(string[] names, string name)
        {
            foreach (string n in names)
            {
                if (string.Compare(n, name, true) == 0)
                    return true;
            }

            return false;
        }
    }
}