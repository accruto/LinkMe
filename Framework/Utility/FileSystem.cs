using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using LinkMe.Environment;

namespace LinkMe.Framework.Utility
{
	/// <summary>
	/// Performs operations on strings that contain file or directory path information.
	/// </summary>
	public static class FileSystem
	{
		public const char SearchPatternSeparatorChar = ';';
        private const int MaxFileNameLength = 255;

	    /// <summary>
        /// Converts a path (<paramref name="relativePath"/>) relative to the current working directory
        /// to an absolute path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        public static string GetAbsolutePath(string relativePath)
        {
            //return GetAbsolutePath(relativePath, Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            return GetAbsolutePath(relativePath, System.Environment.CurrentDirectory);
        }

	    /// <summary>
		/// Converts a path (<paramref name="relativePath"/>) relative to another path
		/// (<paramref name="relativeTo"/>) to an absolute path.
		/// </summary>
		/// <param name="relativePath">The relative path.</param>
		/// <param name="relativeTo">The absolute path that <paramref name="relativePath"/> is relative to.</param>
		/// <returns>The absolute path obtained by combining <paramref name="relativePath"/> and
		/// <paramref name="relativeTo"/>.</returns>
		/// <remarks>
		/// <paramref name="relativeTo"/> must be an absolute path to a directory, not a file name, otherwise
		/// this method will return incorrect results. No check is made to determine whether the path refers
		/// to a file or directory, so the path does not need to actually exist in the file system.
		/// 
		/// If <paramref name="relativePath"/> is already an absolute path it is returned unchanged.
		/// </remarks>
		public static string GetAbsolutePath(string relativePath, string relativeTo)
		{
			if (relativePath == null)
				throw new ArgumentNullException("relativePath");
			if (relativeTo == null)
				throw new ArgumentNullException("relativeTo");

			var relativeToRoot = Path.GetPathRoot(relativeTo);
			if (relativeToRoot.Length == 0 || relativeToRoot == Path.DirectorySeparatorChar.ToString())
				throw new ApplicationException("The 'relative to' path must be absolute.");

			if (relativePath == string.Empty)
				return relativeTo;

			var pathRoot = Path.GetPathRoot(relativePath);
			if (pathRoot.Length != 0)
			{
			    return pathRoot == Path.DirectorySeparatorChar.ToString() ? Path.Combine(relativeToRoot, relativePath.TrimStart('\\')) : relativePath;
			}

	        // Go up one directory for each .. in the relative path.

			var absoluteDirInfo = new DirectoryInfo(relativeTo);

			var parentDirPath = ".." + Path.DirectorySeparatorChar;
			var relative = relativePath;

			while (relative.StartsWith(parentDirPath))
			{
				absoluteDirInfo = absoluteDirInfo.Parent;
				if (absoluteDirInfo == null)
				{
					throw new ApplicationException(string.Format("Relative path '{0}' is not valid"
						+ " relative to '{1}', because it goes up too many levels.", relativePath, relativeTo));
				}
                relative = relative.Substring(parentDirPath.Length);
			}

			return Path.Combine(absoluteDirInfo.FullName, relative);
		}

		/// <summary>
		/// Converts an absolute path to a relative path.
		/// </summary>
		/// <param name="absolutePath">The absolute path to convert.</param>
		/// <param name="relativeTo">The absolute path that the returned path should be made relative to.</param>
		/// <returns>A relative path that is equivalent to <paramref name="absolutePath"/> when evaluated
		/// relative to <paramref name="relativeTo"/>.</returns>
		/// <remarks>
		/// <paramref name="relativeTo"/> must be an absolute path to a directory, not a file name, otherwise
		/// this method will return incorrect results. No check is made to determine whether the path refers
		/// to a file or directory, so the path does not need to actually exist in the file system.
		/// 
		/// If <paramref name="absolutePath"/> is already a relative path it is returned unchanged.
		/// 
		/// If the path roots (root directories) of <paramref name="absolutePath"/> and
		/// <paramref name="relativeTo"/> are different then <paramref name="absolutePath"/> is returned
		/// unchanged.
		/// </remarks>
		public static string GetRelativePath(string absolutePath, string relativeTo)
		{
			if (absolutePath == null)
				throw new ArgumentNullException("absolutePath");
			if (relativeTo == null)
				throw new ArgumentNullException("relativeTo");

			var relativeToRoot = Path.GetPathRoot(relativeTo);
			if (relativeToRoot.Length == 0 || relativeToRoot == Path.DirectorySeparatorChar.ToString())
				throw new ApplicationException("The 'relative to' path must be absolute.");

			var pathRoot = Path.GetPathRoot(absolutePath);
			if (pathRoot.Length == 0 || pathRoot == Path.DirectorySeparatorChar.ToString())
				return absolutePath; // Path is not rooted, so just return it.

			if (string.Compare(pathRoot, relativeToRoot, true) != 0)
				return absolutePath; // Root directories are different, so there is no relative path.

			var pathDirs = Path.GetDirectoryName(absolutePath).Split(Path.DirectorySeparatorChar);
			var relativeToDirs = relativeTo.TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

			var index = 0;
			while (index < pathDirs.Length && index < relativeToDirs.Length
				&& string.Compare(pathDirs[index], relativeToDirs[index], true) == 0)
			{
				index++;
			}

			var sb = new StringBuilder();

			// Go up the directory tree to where 'path' and 'relativeTo' meet.

			if (index < relativeToDirs.Length)
			{
				var upOneLevel = ".." + Path.DirectorySeparatorChar;
				for (var temp = index; temp < relativeToDirs.Length; temp++)
				{
					sb.Append(upOneLevel);
				}
			}

			// Go down the tree to 'path'.

			if (index < pathDirs.Length)
			{
				sb.Append(string.Join(Path.DirectorySeparatorChar.ToString(),
					pathDirs, index, pathDirs.Length - index));
				sb.Append(Path.DirectorySeparatorChar);
			}

			sb.Append(Path.GetFileName(absolutePath));

			return sb.ToString();
		}

		/// <summary>
		/// Determines whether a path is an absolute path.
		/// </summary>
		/// <param name="path">The path to check.</param>
		/// <returns>True if the path is an absolute path, false if it is a relative path.</returns>
		/// <remarks>
		/// This method is similar to, but not the same as <see cref="Path.IsPathRooted"/>. If the path is
		/// relative to the root (ie. begins with "\") then <see cref="Path.IsPathRooted"/> returns true,
		/// but this method returns false.
		/// </remarks>
		public static bool IsAbsolutePath(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");
			if (path.Length == 0)
				throw new ArgumentException("The path to check is an empty string.", "path");

			var pathRoot = Path.GetPathRoot(path);
			return (pathRoot.Length > 0 && pathRoot != Path.DirectorySeparatorChar.ToString());
		}

		/// <summary>
		/// Create a zero-length temporary file in the specified directory and returns the name of the file.
		/// </summary>
		/// <param name="dirPath">The directory in which to create a temporary file.</param>
		/// <returns>The absolute path of the created temporary file.</returns>
		/// <remarks>
		/// The specified directory must exist, otherwise an exception is thrown.
		/// </remarks>
		public static string GetTempFileName(string dirPath)
		{
			const string method = "GetTempFileName";
			const string prefix = "tmp";

			if (dirPath == null)
				throw new Exceptions.NullParameterException(typeof(FileSystem), method, "dirPath");
			if (dirPath.Length == 0 || dirPath.Length > Constants.Win32.MAX_PATH - 14)
			{
				throw new Exceptions.ParameterStringLengthOutOfRangeException(typeof(FileSystem), method,
					"dirPath", dirPath, 1, Constants.Win32.MAX_PATH - 14);
			}

			if (!Directory.Exists(dirPath))
				throw new DirectoryNotFoundException("Directory '" + dirPath + "' does not exist.");

			var sb = new StringBuilder(Constants.Win32.MAX_PATH);
			if (Win32.UnsafeNativeMethods.GetTempFileName(dirPath, prefix, 0, sb) == 0)
				throw new System.ComponentModel.Win32Exception();

			return sb.ToString();
		}

		/// <summary>
		/// Returns the names of files in the specified directory that match the specified search patterns.
		/// </summary>
		/// <param name="path">The directory to search.</param>
		/// <param name="searchPattern">The search string to match against the names of files in
		/// <paramref name="path"/>. Separate multiple search patterns with ';'.</param>
		/// <returns>A string array containing the names of files in the specified directory that match the
		/// specified search pattern.</returns>
		/// <remarks>This method is similar to <see cref="Directory.GetFiles"/>, except that it allows
		/// multiple search patterns to be specified, separated by ';'. For example "*.dll;*.exe" will
		/// find both .dll and .exe files. No duplicates will be returned if the search patterns overlap.</remarks>
		public static string[] GetFiles(string path, string searchPattern)
		{
			if (string.IsNullOrEmpty(searchPattern))
				return Directory.GetFiles(path);

			var patterns = searchPattern.Split(SearchPatternSeparatorChar);
			if (patterns.Length == 1)
				return Directory.GetFiles(path, searchPattern);

			// Multiple search patterns specified - search for them all and add the results, eliminating
			// duplicates.

			var hashtable = new Hashtable();

			foreach (var pattern in patterns)
			{
				var files = Directory.GetFiles(path, pattern);
				foreach (var file in files)
				{
					hashtable[file] = null;
				}
			}

			var found = new string[hashtable.Values.Count];
			hashtable.Keys.CopyTo(found, 0);

			return found;
		}

		/// <summary>
		/// Returns the names of files in the specified directory and its sub-directories, recursively.
		/// </summary>
		/// <param name="path">The directory from which to retrieve the files.</param>
		/// <returns>A string array of file names in the specified directory.</returns>
		/// <remarks>
		/// This method is similar to <see cref="System.IO.Directory.GetFiles"/>, but recurses into
		/// sub-directories and allows multiple search patterns separated by ';'. For further details see the
		/// <see cref="GetFiles"/> method.
		/// </remarks>
		public static string[] GetFilesRecursive(string path)
		{
			return GetFilesRecursive(path, "*");
		}

		/// <summary>
		/// Returns the names of files in the specified directory and its sub-directories, recursively,
		/// that match the specified search pattern.
		/// </summary>
		/// <param name="path">The directory from which to retrieve the files.</param>
		/// <param name="searchPattern">The search string to match against the names of files in path. The
		/// parameter cannot end in two periods ("..") or contain two periods ("..") followed by
		/// <see cref="System.IO.Path.DirectorySeparatorChar"/> or <see cref="System.IO.Path.AltDirectorySeparatorChar"/>,
		/// nor can it contain any of the characters in <see cref="System.IO.Path.InvalidPathChars"/>.</param>
		/// <returns>A string array containing the names of files in the specified directory that match the
		/// specified search pattern.</returns>
		/// <remarks>
		/// This method is similar to <see cref="System.IO.Directory.GetFiles"/>, but recurses into
		/// sub-directories and allows multiple search patterns separated by ';'. For further details see the
		/// <see cref="GetFiles"/> method.
		/// </remarks>
		public static string[] GetFilesRecursive(string path, string searchPattern)
		{
			const string method = "GetFilesRecursive";

			if (path == null)
				throw new Exceptions.NullParameterException(typeof(FileSystem), method, "path");

			if (string.IsNullOrEmpty(searchPattern))
			{
				searchPattern = "*";
			}

			var list = new ArrayList();
			GetFilesRecursive(path, searchPattern, list);

			return (string[])list.ToArray(typeof(string));
		}

		public static string GetRootedPath(string drive, string path)
		{
		    if ( string.IsNullOrEmpty(path) || path[0] != Path.DirectorySeparatorChar )
				return path;
		    return Path.GetPathRoot(drive).TrimEnd(Path.DirectorySeparatorChar) + path;
		}

	    public static string GetValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("The filename must be specified.", "filename");

            var invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).ToArray();

            var cleanFilename = fileName.Trim();
            var index = cleanFilename.IndexOfAny(invalidChars);

            while (index != -1)
            {
                cleanFilename = cleanFilename.Remove(index, 1);
                if (index == cleanFilename.Length)
                    break;

                index = cleanFilename.IndexOfAny(invalidChars, index);
            }

            if (cleanFilename.Length == 0)
            {
                throw new ArgumentException("The input filename, '" + fileName
                    + "', consists entirely of invalid characters.", "filename");
            }

            cleanFilename = TextUtil.Truncate(cleanFilename, MaxFileNameLength);

            return cleanFilename;
        }

        private static void GetFilesRecursive(string path, string searchPattern, ArrayList list)
		{
			Debug.Assert(path != null && searchPattern != null && list != null,
				"path != null && searchPattern != null && list != null");

			try
			{
				list.AddRange(GetFiles(path, searchPattern));
			}
			catch (UnauthorizedAccessException)
			{
				return; // Cannot access this directory - continue searching.
			}

			var dirs = Directory.GetDirectories(path);

			foreach (var dir in dirs)
			{
				GetFilesRecursive(dir, searchPattern, list);
			}
		}

		/// <summary>
		/// Copy a folder, all sub-folders and all files.
		/// </summary>
		public static void CopyFolder(string sourcePath, string destinationPath, bool overwrite)
		{
			Copy(new DirectoryInfo(sourcePath), new DirectoryInfo(destinationPath), overwrite);
		}

		private static void Copy(DirectoryInfo sourceDirectory, DirectoryInfo destinationDirectory, bool overwrite)
		{
			// Make sure the destination folder exists.

            if (!destinationDirectory.Exists)
				destinationDirectory.Create();
			
			// Iterate.

			foreach (var subDirectory in sourceDirectory.GetDirectories())
				Copy(subDirectory, new DirectoryInfo(Path.Combine(destinationDirectory.FullName, subDirectory.Name)), overwrite);

			foreach (var file in sourceDirectory.GetFiles())
				file.CopyTo(Path.Combine(destinationDirectory.FullName, file.Name), overwrite);
		}

        public static string GetSourcePath(string path)
        {
            return IsAbsolutePath(path)
                ? path
                : GetAbsolutePath(path, RuntimeEnvironment.GetSourceFolder());
        }
	}
}
