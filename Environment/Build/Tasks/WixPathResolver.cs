using System.Collections.Generic;
using System.Globalization;
using Microsoft.Tools.WindowsInstallerXml.Serialize;

namespace LinkMe.Environment.Build.Tasks
{
    internal class WixPathResolver
        : WixOperator
    {
        public void Resolve(Directory directory)
        {
            // Get all directory and file ids.

            SortedList<string, string> directoryIds = GetDirectoryIds(directory);
            SortedList<string, string> fileIds = GetFileIds(directory);

            // Get all registry values.

            List<RegistryValue> values = GetElements<RegistryValue>(directory);
            foreach ( RegistryValue registryValue in values )
            {
                if ( registryValue.Value != null )
                {
                    ResolveRegistryValue(directoryIds, fileIds, registryValue);
                }
                else
                {
                    foreach ( ISchemaElement element in registryValue.Children )
                    {
                        if ( element is MultiStringValue )
                            ResolveMultiStringValue(directoryIds, fileIds, (MultiStringValue) element);
                    }
                }
            }
        }

        private void ResolveRegistryValue(SortedList<string, string> directoryIds, SortedList<string, string> fileIds, RegistryValue registryValue)
        {
            string value = registryValue.Value.ToLower(CultureInfo.InvariantCulture);

            // Replace file paths with ids.

            foreach ( KeyValuePair<string, string> pair in fileIds )
            {
                int index;
                while ( (index = value.IndexOf(pair.Key)) != -1 )
                {
                    // Replace the non-lower case value itself.

                    registryValue.Value = registryValue.Value.Remove(index, (pair.Key).Length);
                    registryValue.Value = registryValue.Value.Insert(index, pair.Value);
                    value = registryValue.Value.ToLower(CultureInfo.InvariantCulture);
                }
            }

            // Replace directory paths with ids.

            foreach ( KeyValuePair<string, string> pair in directoryIds )
            {
                int index;
                while ( (index = value.IndexOf(pair.Key)) != -1 )
                {
                    // Replace the non-lower case value itself.

                    registryValue.Value = registryValue.Value.Remove(index, (pair.Key).Length);
                    registryValue.Value = registryValue.Value.Insert(index, pair.Value);
                    value = registryValue.Value.ToLower(CultureInfo.InvariantCulture);
                }
            }
        }

        private void ResolveMultiStringValue(SortedList<string, string> directoryIds, SortedList<string, string> fileIds, MultiStringValue multiStringValue)
        {
            string value = multiStringValue.Content.ToLower(CultureInfo.InvariantCulture);

            // Replace file paths with ids.

            foreach ( KeyValuePair<string, string> pair in fileIds )
            {
                int index;
                while ( (index = value.IndexOf(pair.Key)) != -1 )
                {
                    // Replace the non-lower case value itself.

                    multiStringValue.Content = multiStringValue.Content.Remove(index, (pair.Key).Length);
                    multiStringValue.Content = multiStringValue.Content.Insert(index, pair.Value);
                    value = multiStringValue.Content.ToLower(CultureInfo.InvariantCulture);
                }
            }

            // Replace directory paths with ids.

            foreach ( KeyValuePair<string, string> pair in directoryIds )
            {
                int index;
                while ( (index = value.IndexOf(pair.Key)) != -1 )
                {
                    // Replace the non-lower case value itself.

                    multiStringValue.Content = multiStringValue.Content.Remove(index, (pair.Key).Length);
                    multiStringValue.Content = multiStringValue.Content.Insert(index, pair.Value);
                    value = multiStringValue.Content.ToLower(CultureInfo.InvariantCulture);
                }
            }
        }

        private class ReverseComparer
            : IComparer<string>
        {
            public int Compare(string a, string b)
            {
                return -1 * System.StringComparer.InvariantCultureIgnoreCase.Compare(a, b);
            }
        }

        private SortedList<string, string> GetDirectoryIds(Directory directory)
        {
            // Get a list of all directories first.

            List<Directory> childDirectories = new List<Directory>();
            foreach ( Directory childDirectory in GetElements<Directory>(directory) )
                childDirectories.Add(childDirectory);

            // Create a list where longer paths come first.

            SortedList<string, string> ids = new SortedList<string, string>(new ReverseComparer());

            foreach ( Directory childDirectory in childDirectories )
            {
                string path = childDirectory.FileSource;
                if ( path != null )
                {
                    // Create a new directory element without the FileSource attribute.

                    Directory newDirectory = new Directory();
                    newDirectory.Id = childDirectory.Id;
                    newDirectory.Name = childDirectory.Name;
                    foreach ( ISchemaElement childElement in childDirectory.Children )
                        newDirectory.AddChild(childElement);

                    ((IParentElement) childDirectory.ParentElement).AddChild(newDirectory);
                    ((IParentElement) childDirectory.ParentElement).RemoveChild(childDirectory);

                    if ( newDirectory.Id != null )
                    {
                        // Add a mapping.

                        ids[path.ToLower(CultureInfo.InvariantCulture)] = string.Concat("[", newDirectory.Id, "]");
                    }
                }
            }

            return ids;
        }

        private SortedList<string, string> GetFileIds(Directory directory)
        {
            SortedList<string, string> ids = new SortedList<string, string>();

            // Look for all files within the directory.

            List<File> files = GetElements<File>(directory);
            foreach ( File file in files )
            {
                if ( !string.IsNullOrEmpty(file.Id) && !string.IsNullOrEmpty(file.Source) )
                {
                    // Look for the source itself.

                    ids[file.Source.ToLower(CultureInfo.InvariantCulture)] = string.Concat("[#", file.Id, "]");

                    // Look for a file URI for the file.

                    ids[new System.Uri(file.Source).ToString().ToLower(CultureInfo.InvariantCulture)] = string.Concat("[#", file.Id, "]");

                    // Look for the short path.

                    ids[FilePath.GetShortPathName(file.Source).ToLower(CultureInfo.InvariantCulture)] = string.Concat("[!", file.Id, "]");
                }
            }

            return ids;
        }
    }
}