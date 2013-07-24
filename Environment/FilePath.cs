using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LinkMe.Environment
{
    public static class FilePath
    {
        /// <summary>
        /// Converts a path (<paramref name="relativePath"/>) relative to another path
        /// (<paramref name="relativeToFolder"/>) to an absolute path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="relativeToFolder">The absolute path that <paramref name="relativePath"/> is relative to.</param>
        /// <returns>The absolute path obtained by combining <paramref name="relativePath"/> and
        /// <paramref name="relativeToFolder"/>.</returns>
        /// <remarks>
        /// <paramref name="relativeToFolder"/> must be an absolute path to a directory, not a file name, otherwise
        /// this method will return incorrect results. No check is made to determine whether the path refers
        /// to a file or directory, so the path does not need to actually exist in the file system.
        /// 
        /// If <paramref name="relativePath"/> is already an absolute path it is returned unchanged.
        /// </remarks>
        public static string GetAbsolutePath(string relativePath, string relativeToFolder)
        {
            if (relativePath == null)
                throw new System.ArgumentNullException("relativePath");
            if (relativeToFolder == null)
                throw new System.ArgumentNullException("relativeToFolder");

            string relativeToFolderRoot = Path.GetPathRoot(relativeToFolder);
            if (relativeToFolderRoot.Length == 0 || relativeToFolderRoot == Path.DirectorySeparatorChar.ToString())
                throw new System.ApplicationException("The 'relative to folder' path must be absolute.");

            string relativePathRoot = Path.GetPathRoot(relativePath);
            if (relativePathRoot.Length != 0)
            {
                if (relativePathRoot == Path.DirectorySeparatorChar.ToString())
                    return Path.Combine(relativePathRoot, relativePath.TrimStart(Path.DirectorySeparatorChar)); // The path is relative to the root (starts with "\").
                return relativePath; // Path is already rooted, so just return it.
            }

            // Go up one directory for each .. in the relative path.

            string parentFolderPath = ".." + Path.DirectorySeparatorChar;

            // If the path ends with ".." treat it the same way as "..\"

            if (relativePath.EndsWith(".."))
                relativePath += Path.DirectorySeparatorChar;

            var absoluteDirectoryInfo = new DirectoryInfo(relativeToFolder);
            while (relativePath.StartsWith(parentFolderPath))
            {
                absoluteDirectoryInfo = absoluteDirectoryInfo.Parent;
                if (absoluteDirectoryInfo == null)
                    throw new System.ApplicationException(string.Format("Relative path '{0}' is not valid relative to '{1}', because it goes up too many levels.", relativePath, relativeToFolder));
                relativePath = relativePath.Substring(parentFolderPath.Length);
            }

            return Path.Combine(absoluteDirectoryInfo.FullName, relativePath);
        }

        public static string GetRelativePath(string absolutePath, string relativeToFolder)
        {
            if ( absolutePath == null )
                throw new System.ArgumentNullException("absolutePath");
            if ( relativeToFolder == null )
                throw new System.ArgumentNullException("relativeToFolder");

            absolutePath = Collapse(absolutePath);
            relativeToFolder = Collapse(relativeToFolder);

            string relativeToRoot = Path.GetPathRoot(relativeToFolder);
            if ( relativeToRoot.Length == 0 || relativeToRoot == Path.DirectorySeparatorChar.ToString() )
                throw new System.ApplicationException("The 'relative to folder' path must be absolute.");

            string absoluteRoot = Path.GetPathRoot(absolutePath);
            if ( absoluteRoot.Length == 0 || absoluteRoot == Path.DirectorySeparatorChar.ToString() )
                return absolutePath; // Path is not rooted, so just return it.

            if ( string.Compare(absoluteRoot, relativeToRoot, true) != 0 )
                return absolutePath; // Root directories are different, so there is no relative path.

            string[] absolutePathFolder = Path.GetDirectoryName(absolutePath).Split(Path.DirectorySeparatorChar);
            string[] relativeToFolders = relativeToFolder.TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

            int index = 0;
            while ( index < absolutePathFolder.Length && index < relativeToFolders.Length && string.Compare(absolutePathFolder[index], relativeToFolders[index], true) == 0 )
            {
                index++;
            }

            // Go up the directory tree to where 'path' and 'relativeTo' meet.

            var sb = new StringBuilder();
            if ( index < relativeToFolders.Length )
            {
                string upOneLevel = ".." + Path.DirectorySeparatorChar;
                for ( int temp = index; temp < relativeToFolders.Length; temp++ )
                    sb.Append(upOneLevel);
            }

            // Go down the tree to 'path'.

            if ( index < absolutePathFolder.Length )
            {
                sb.Append(string.Join(Path.DirectorySeparatorChar.ToString(), absolutePathFolder, index, absolutePathFolder.Length - index));
                sb.Append(Path.DirectorySeparatorChar);
            }

            sb.Append(Path.GetFileName(absolutePath));
            return sb.ToString();
        }

        private static string Collapse(string path)
        {
            var parts = path.Split(Path.DirectorySeparatorChar);
            var newParts = new List<string>();
            for (var index = 0; index < parts.Length; ++index)
            {
                if (parts[index] != "..")
                    newParts.Add(parts[index]);
                else
                    newParts.RemoveAt(newParts.Count - 1);
            }

            return string.Join(Path.DirectorySeparatorChar.ToString(), newParts.ToArray());
        }

        public static bool IsAbsolutePath(string path)
        {
            if ( path == null )
                throw new System.ArgumentNullException("path");
            string pathRoot = Path.GetPathRoot(path);
            return pathRoot.Length > 0 && pathRoot != Path.DirectorySeparatorChar.ToString();
        }

        public static string GetShortPathName(string fullPath)
        {
            var shortPath = new StringBuilder(Constants.Path.MaxPath, Constants.Path.MaxPath);
            if (GetShortPathName(fullPath, shortPath, Constants.Path.MaxPath) == 0)
                throw new COMException(StringResourceManager.Format(Resources.Exceptions.FilePathShortPathName, fullPath), Marshal.GetLastWin32Error());
            return shortPath.ToString();
        }

        [DllImport("kernel32.dll", EntryPoint = "GetShortPathNameW", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern uint GetShortPathName(string longPath, StringBuilder shortPath, [MarshalAs(UnmanagedType.U4)]int buffer);
    }
}