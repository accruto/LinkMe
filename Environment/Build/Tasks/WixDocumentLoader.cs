using System.Xml;
using System.Text;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Tools.WindowsInstallerXml.Serialize;

using Directory = Microsoft.Tools.WindowsInstallerXml.Serialize.Directory;
using File = Microsoft.Tools.WindowsInstallerXml.Serialize.File;
using Registry=LinkMe.Environment.Build.Tasks.Constants.Registry;
using Wix = LinkMe.Environment.Build.Tasks.Constants.Wix;

namespace LinkMe.Environment.Build.Tasks
{
    public class WixOperator
    {
        protected List<T> GetElements<T>(IParentElement parentElement)
        {
            List<T> list = new List<T>();

            foreach (ISchemaElement element in parentElement.Children)
            {
                if (element.GetType() == typeof(T))
                    list.Add((T)element);
                if (element is IParentElement)
                    list.AddRange(GetElements<T>(element as IParentElement));
            }

            return list;
        }
    }

    public class WixFragmentLoader
        : WixOperator
    {
        protected Directory CreateDirectory(string id, string name)
        {
            Directory directory = new Directory();
            directory.Id = id;
            directory.Name = name;
            return directory;
        }

        protected Directory CreateDirectoryByParentId(string parentId, string name)
        {
            Directory directory = new Directory();
            directory.Id = CreateDirectoryId(parentId, name);
            directory.Name = name;
            return directory;
        }

        protected Directory GetTargetDirectory(ISchemaElement element)
        {
            if (element is Directory)
            {
                if (((Directory)element).Id == Wix.Xml.Directory.Target.Id)
                    return element as Directory;
            }

            return element.ParentElement == null ? null : GetTargetDirectory(element.ParentElement);
        }

        protected Directory GetDirectory(Directory parent, string projectRelativePath)
        {
            string[] folders = projectRelativePath.Split(Path.DirectorySeparatorChar);
            Directory directory = parent;
            for (int index = 0; index < folders.Length; ++index)
            {
                string folder = folders[index];
                if (folder != ".")
                    directory = GetChildDirectoryByName(directory, folder);
            }

            return directory;
        }

        protected Directory GetChildDirectory(Directory parent, string id, string name)
        {
            foreach (ISchemaElement element in parent.Children)
            {
                if (element is Directory)
                {
                    if (((Directory)element).Id == id)
                        return element as Directory;
                }
            }

            // Not found so create it.

            Directory directory = CreateDirectory(id, name);
            parent.AddChild(directory);
            return directory;
        }

        protected static File CreateFile(string projectPath, string sourcePath)
        {
            return CreateFile(projectPath, sourcePath, string.Empty);
        }

        protected static File CreateFile(string projectPath, string sourcePath, string prefix)
        {
            File file = new File();
            file.Id = CreateFileId(projectPath, prefix);
            file.Name = Path.GetFileName(projectPath);
            file.Compressed = Wix.Xml.CompressedDefault;
            file.Source = sourcePath;
            return file;
        }

        protected static Shortcut CreateShortcut(string projectPath, string shortcutName)
        {
            Shortcut shortcut = new Shortcut();
            shortcut.Id = CreateShortcutId(projectPath);
            shortcut.Name = shortcutName;
            return shortcut;
        }

        protected static Component CreateComponent(bool isWin64, string path, System.Guid guid)
        {
            return CreateComponent(isWin64, path, guid, string.Empty);
        }

        protected static Component CreateComponent(bool isWin64, string path, System.Guid guid, string prefix)
        {
            Component component = new Component();
            component.Id = CreateComponentId(path, prefix);
            component.Guid = guid.ToString("D");
            if (isWin64)
                component.Win64 = YesNoType.yes;
            return component;
        }

        protected void Load(RegistryCaptureKey key, IParentElement component)
        {
            // Iterate.

            foreach (RegistryCaptureKey subKey in key.SubKeys)
                Load(subKey, component);

            // Iterate over values.

            foreach (RegistryCaptureValue value in key.Values)
            {
                // Add the common information.

                RegistryValue registryValue = new RegistryValue();
                if (!string.IsNullOrEmpty(value.Name))
                    registryValue.Name = value.Name;
                registryValue.Action = RegistryValue.ActionType.write;
                registryValue.Root = GetRegistryRoot(key.Root);
                registryValue.Key = key.RootRelativePath;

                // Add the type specific value.

                if (value is RegistryCaptureStringValue)
                {
                    registryValue.Type = RegistryValue.TypeType.@string;
                    registryValue.Value = GetValue((RegistryCaptureStringValue)value);
                }
                else if (value is RegistryCaptureDWordValue)
                {
                    registryValue.Type = RegistryValue.TypeType.integer;
                    registryValue.Value = GetValue((RegistryCaptureDWordValue)value);
                }
                else if (value is RegistryCaptureBinaryValue)
                {
                    registryValue.Type = RegistryValue.TypeType.binary;
                    registryValue.Value = GetValue((RegistryCaptureBinaryValue)value);
                }
                else if (value is RegistryCaptureExpandStringValue)
                {
                    registryValue.Type = RegistryValue.TypeType.@string;
                    registryValue.Value = GetValue((RegistryCaptureExpandStringValue)value);
                }
                else if (value is RegistryCaptureMultiStringValue)
                {
                    registryValue.Type = RegistryValue.TypeType.multiString;

                    foreach (string multiStringValueContent in ((RegistryCaptureMultiStringValue)value).Value)
                    {
                        MultiStringValue multiStringValue = new MultiStringValue();
                        multiStringValue.Content = multiStringValueContent;
                        registryValue.AddChild(multiStringValue);
                    }
                }
                else
                {
                    registryValue.Type = RegistryValue.TypeType.@string;
                    registryValue.Value = GetValue(value);
                }

                component.AddChild(registryValue);
            }
        }

        private RegistryRootType GetRegistryRoot(string root)
        {
            switch (root)
            {
                case Registry.Key.ClassesRoot:
                    return RegistryRootType.HKCR;

                case Registry.Key.CurrentUser:
                    return RegistryRootType.HKCU;

                case Registry.Key.Users:
                    return RegistryRootType.HKU;

                default:
                    return RegistryRootType.HKLM;
            }
        }

        private string GetValue(RegistryCaptureStringValue value)
        {
            return value.Value;
        }

        private string GetValue(RegistryCaptureDWordValue value)
        {
            return value.Value.ToString(CultureInfo.InvariantCulture);
        }

        private string GetValue(RegistryCaptureBinaryValue value)
        {
            StringBuilder hexadecimalValue = new StringBuilder();
            foreach (byte byteValue in value.Value)
                hexadecimalValue.Append(byteValue.ToString("X2", CultureInfo.InvariantCulture.NumberFormat));
            return hexadecimalValue.ToString();
        }

        private string GetValue(RegistryCaptureExpandStringValue value)
        {
            return value.Value;
        }

        private string GetValue(RegistryCaptureValue value)
        {
            return value.Value == null ? string.Empty : value.Value.ToString();
        }

        protected Directory GetChildDirectoryByName(Directory parent, string name)
        {
            if (string.IsNullOrEmpty(name))
                return parent;

            foreach (ISchemaElement element in parent.Children)
            {
                if (element is Directory)
                {
                    if (((Directory)element).Name == name)
                        return element as Directory;
                }
            }

            // Not found so create it.

            Directory directory = CreateDirectoryByParentId(parent.Id, name);
            parent.AddChild(directory);
            return directory;
        }

        private static string CreateDirectoryId(string parentId, string name)
        {
            switch (parentId)
            {
                case Wix.Xml.Directory.Target.Id:
                    return (Wix.Xml.Id.DirectoryPrefix + Wix.Xml.Id.NameSeparator + name).Replace(" ", "__").Replace("-", "__").Replace("&", "").Replace("(", "").Replace(")", "").Replace("'", "");

                default:
                    return (parentId + Wix.Xml.Id.NameSeparator + name).Replace(" ", "__").Replace("-", "__").Replace("&", "").Replace("(", "").Replace(")", "").Replace("'", "");
            }
        }

        private static string CreateFileId(string projectPath, string prefix)
        {
            return (Wix.Xml.Id.FilePrefix
                    + Wix.Xml.Id.NameSeparator
                    + (string.IsNullOrEmpty(prefix) ? string.Empty : (prefix + Wix.Xml.Id.NameSeparator))
                    + projectPath.Replace(new string(Path.DirectorySeparatorChar, 1), Wix.Xml.Id.NameSeparator)).Replace(" ", "__").Replace("-", "__").Replace("&", "").Replace("(", "").Replace(")", "").Replace("'", "");
        }

        private static string CreateShortcutId(string projectPath)
        {
            return (Wix.Xml.Id.ShortcutPrefix
                    + Wix.Xml.Id.NameSeparator
                    + projectPath.Replace(new string(Path.DirectorySeparatorChar, 1), Wix.Xml.Id.NameSeparator)).Replace(" ", "__").Replace("-", "__").Replace("&", "").Replace("(", "").Replace(")", "").Replace("'", "");
        }

        private static string CreateComponentId(string projectPath, string prefix)
        {
            return (Wix.Xml.Id.ComponentPrefix
                    + Wix.Xml.Id.NameSeparator
                    + (string.IsNullOrEmpty(prefix) ? string.Empty : (prefix + Wix.Xml.Id.NameSeparator))
                    + projectPath.Replace(new string(Path.DirectorySeparatorChar, 1), Wix.Xml.Id.NameSeparator)).Replace(" ", "__").Replace("-", "__").Replace("&", "").Replace("(", "").Replace(")", "").Replace("'", "");
        }
    }

    public class WixDocumentLoader
        : WixFragmentLoader
    {
        public void Save(string path)
        {
            using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.Unicode))
            {
                writer.Indentation = 4;
                writer.Formatting = Formatting.Indented;
                m_wix.OutputXml(writer);
            }
        }

        public Microsoft.Tools.WindowsInstallerXml.Serialize.Wix Wix
        {
            get
            {
                if (m_wix == null)
                    m_wix = new Microsoft.Tools.WindowsInstallerXml.Serialize.Wix();
                return m_wix;
            }
        }

        protected static void Resolve(Directory directory)
        {
            WixPathResolver pathResolver = new WixPathResolver();
            pathResolver.Resolve(directory);

            // There seems to be a problem with Wix for .NET classes as COM objects
            // where it doesn't seem to handle the InprocServer32 being set to mscoree.dll
            // properly.  Get a "is duplicated in table 'Registry'" error.
            // Just leave as raw registry entries for now and don't
            // try to resolve.  Whilst not as nice the end result should be equivalent.

            //WixComResolver comResolver = new WixComResolver();
            //comResolver.Resolve(directory);
        }

        private Microsoft.Tools.WindowsInstallerXml.Serialize.Wix m_wix;
    }
}