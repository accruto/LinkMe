using Microsoft.Tools.WindowsInstallerXml.Serialize;

namespace LinkMe.Environment.Build.Tasks
{
    public static class Constants
    {
        internal static class Product
        {
            internal const string Sdk = LinkMe.Environment.Constants.Product.Sdk;
        }

        public static class Project
        {
            internal const string InprocServerDll = "mscoree.dll";

            public static class Item
            {
                public const string Link = "Link";

                public static class Assemble
                {
                    public static class InstallInGac
                    {
                        public const string Name = "InstallInGac";
                    }

                    internal static class GacGuid
                    {
                        internal const string Name = "GacGuid";
                    }

                    public static class ShortcutName
                    {
                        public const string Name = "ShortcutName";
                    }

                    public static class ShortcutPath
                    {
                        public const string Name = "ShortcutPath";
                    }

                    public static class Guid
                    {
                        public const string Name = "Guid";
                    }
                }
            }
        }

        internal static class Wix
        {
            internal const string RegistryCaptureName = "Wix";
            internal const string AssemblyNet = ".net";

            internal static class Xml
            {
                internal const string Prefix = "wix";
                internal const string Namespace = "http://schemas.microsoft.com/wix/2003/01/wi";

                internal const string WixElement = "Wix";
                internal const string ModuleElement = "Module";
                internal const string ComponentElement = "Component";
                internal const string FileElement = "File";
                internal const string RegistryElement = "Registry";
                internal const string PackageElement = "Package";
                internal const string DirectoryElement = "Directory";

                internal const string IdAttribute = "Id";
                internal const string LanguageAttribute = "Language";
                internal const string CodepageAttribute = "Codepage";
                internal const string VersionAttribute = "Version";
                internal const string GuidAttribute = "Guid";
                internal const string ManufacturerAttribute = "Manufacturer";
                internal const string NameAttribute = "Name";
                internal const string CompressedAttribute = "Compressed";
                internal const string SourceAttribute = "Source";
                internal const string AssemblyAttribute = "Assembly";
                internal const string AssemblyApplicationAttribute = "AssemblyApplication";
                internal const string KeyPathAttribute = "KeyPath";
                internal const string ActionAttribute = "Action";
                internal const string KeyAttribute = "Key";
                internal const string RootAttribute = "Root";
                internal const string TypeAttribute = "Type";
                internal const string ValueAttribute = "Value";

                internal const string LanguageDefault = "1033";
                internal const int CodepageDefault = 1252;
                internal const YesNoDefaultType CompressedDefault = YesNoDefaultType.@default;
                internal const string VersionDefault = "0.0.0.0";
                internal const string Yes = "yes";
                internal const string No = "no";

                internal static class Directory
                {
                    internal static class Target
                    {
                        internal const string Id = "TARGETDIR";
                        internal const string Name = "SourceDir";
                    }

                    internal static class ProgramMenu
                    {
                        internal const string Id = "ProgramMenuFolder";
                        internal const string Name = "PMenu";
                    }

                    internal static class AppProgramMenu
                    {
                        internal const string Id = "AppProgramMenuDir";
                    }
                }

                internal static class Id
                {
                    internal const string DirectoryPrefix = "Dir";
                    internal const string MenuDirectoryPrefix = "Menu";
                    internal const string ComponentPrefix = "Comp";
                    internal const string FilePrefix = "File";
                    internal const string ShortcutPrefix = "Scut";
                    internal const string NameSeparator = "_";
                    internal const string GacPrefix = "Gac";
                }

                internal static class Action
                {
                    internal const string CreateKey = "createKey";
                    internal const string CreateKeyAndRemoveKeyOnUninstall = "createKeyAndRemoveKeyOnUninstall";
                    internal const string Write = "write";
                }

                internal static class Type
                {
                    internal const string String = "string";
                    internal const string Integer = "integer";
                    internal const string Binary = "binary";
                    internal const string Expandable = "expandable";
                    internal const string MultiString = "multiString";
                }

                internal static class XPath
                {
                    internal const string TargetDirectory = "//*[local-name() = 'Directory' and @Id='TARGETDIR']";
                }
            }
        }

        internal static class Catalogue
        {
            internal static class Artifact
            {
                internal const string Guid = "guid";
                internal const string Assembly = "assembly";
                internal const string InstallInGac = "installInGac";
                internal const string GacGuid = "gacGuid";
                internal const string Register = "register";
                internal const string RegisterPackages = "registerPackages";
                internal const string ShortcutName = "shortcutName";
                internal const string ShortcutPath = "shortcutPath";
            }

            internal static class Xml
            {
                internal const string Prefix = "cat";
                internal const string Namespace = LinkMe.Environment.Constants.Xml.RootNamespace + "/Build/Catalogue";

                internal const string ConfigurationElement = "configuration";
                internal const string LinkMeElement = "linkMe";
                internal const string CatalogueElement = "catalogue";
                internal const string ArtifactsElement = "artifacts";
                internal const string ArtifactElement = "artifact";
                internal const string AssociatedArtifactsElement = "associatedArtifacts";

                internal const string RootFolderAttribute = "rootFolder";
                internal const string GuidAttribute = "guid";
                internal const string PathAttribute = "path";
            }
        }

        public static class File
        {
            internal static class Dll
            {
                internal const string Extension = ".dll";
            }

            internal static class Exe
            {
                internal const string Extension = ".exe";
            }

            internal static class Msc
            {
                internal const string Extension = ".msc";
            }

            internal static class Tlb
            {
                internal const string Extension = ".tlb";
            }

            internal static class Xml
            {
                internal const string Extension = ".xml";
            }

            internal static class Pdb
            {
                internal const string Extension = ".pdb";
            }

            internal static class Wxs
            {
                internal const string Extension = ".wxs";
            }

            internal static class Wixobj
            {
                internal const string Extension = ".wixobj";
            }

            public static class Catalogue
            {
                public const string Extension = ".catalogue";
            }

            internal static class Reg
            {
                internal const string Extension = ".reg";
            }

            internal static class Config
            {
                internal const string Extension = ".config";
            }

            internal static class Msm
            {
                internal const string Extension = ".msm";
            }
        }

        internal static class Folder
        {
            internal const string Bin = LinkMe.Environment.Constants.Folder.Bin;
            internal const string Policy = "Policy";
            internal const string Targets = "Targets";
            internal const string Obj = "obj";
        }

        internal static class Registry
        {
            internal const uint StandardRightsAll = 0x001F0000;
            internal const uint GenericRead = 0x80000000;
            internal const uint GenericWrite = 0x40000000;
            internal const uint GenericExecute = 0x20000000;
            internal const uint GenericAll = 0x10000000;

            internal static class Key
            {
                internal static readonly System.UIntPtr HKeyClassesRoot = (System.UIntPtr)0x80000000;
                internal static readonly System.UIntPtr HKeyCurrentUser = (System.UIntPtr)0x80000001;
                internal static readonly System.UIntPtr HKeyLocalMachine = (System.UIntPtr)0x80000002;
                internal static readonly System.UIntPtr HKeyUsers = (System.UIntPtr)0x80000003;

                internal const string Software = "SOFTWARE";
                internal const string ClassesRoot = "HKCR";
                internal const string CurrentUser = "HKCU";
                internal const string Users = "HKU";
                internal const string LocalMachine = "HKLM";
            }
        }
    }
}