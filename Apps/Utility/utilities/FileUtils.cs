using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Framework.Utility;

namespace LinkMe.Utility.Utilities
{
	public static class FileUtils
	{
        private const char ListFileLineCommentChar = ';';
        // Place this value in the script list (on a line by itself) to start processing from the next
        // line, rather than the start of the file (ie. ignore everything before this line).
        // Useful for restarting a long script from the point of failure while testing.
        private const string ListFileStartSentinel = "*START*";
        // Place this value in the script list (on a line by itself) to stop processing at this point.
        // Useful for testing.
        private const string ListFileEndSentinel = "*END*";

	    public static IList<string> ReadFileList(string listFilePath)
        {
            string scriptDir = Path.GetDirectoryName(listFilePath);

            var scriptList = new List<string>();

            using (var reader = new StreamReader(listFilePath))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = line.Trim();

                    if (string.Equals(line, ListFileStartSentinel, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Found a start sentinel - ignore everything before it.
                        scriptList.Clear();
                    }
                    else if (string.Equals(line, ListFileEndSentinel, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Found a end sentinel - ignore everything after it.
                        break;
                    }
                    else if (line.Length > 0 && line[0] != ListFileLineCommentChar)
                    {
                        string filePath = Path.Combine(scriptDir, line);

                        // Check that every file exists now, before we run any of them.

                        if (!File.Exists(filePath))
                        {
                            throw new FileNotFoundException(string.Format("File '{0}', listed in '{1}', does not exist.",
                                filePath, listFilePath), filePath);
                        }

                        scriptList.Add(filePath);
                    }

                    line = reader.ReadLine();
                }
            }

            return scriptList;
        }

	    /// <summary>
		/// Return the full path of a file relative to another file. Eg. "bin\some.dll" relative to
		/// "C:\temp\file.txt" is "C:\temp\bin\some.dll".
		/// </summary>
		public static string GetPathRelativeToFilePath(string relativePath, string relativeToFilePath)
		{
			if (string.IsNullOrEmpty(relativePath))
				return relativePath;

			if (string.IsNullOrEmpty(relativeToFilePath))
			{
				throw new ArgumentException("The 'relative to file' path must be specified.",
					"relativeToFilePath");
			}
			if (!FileSystem.IsAbsolutePath(relativeToFilePath))
			{
				throw new ArgumentException("The 'relativeToFilePath' argument must be an absolute path.",
					relativeToFilePath);
			}

            return FileSystem.GetAbsolutePath(relativePath, Path.GetDirectoryName(relativeToFilePath));
		}
    }
}
