using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    public class DeleteDirectoryContents
        : Task
    {
        private ITaskItem[] _directories;

        [Required]
        public ITaskItem[] Directories
        {
            get { return _directories; }
            set { _directories = value; }
        }

        public override bool Execute()
        {
            foreach (var directory in Directories)
            {
                var info = new DirectoryInfo(directory.ItemSpec);
                if (!info.Exists)
                {
                    Log.LogError("Directory '{0}' does not exist.", directory.ItemSpec);
                    return false;
                }

                Log.LogMessage("Deleting all contents of directory '{0}'", directory.ItemSpec);

                foreach (var subinfo in info.GetFileSystemInfos())
                {
                    try
                    {
                        var subdirectory = subinfo as DirectoryInfo;
                        if (subdirectory != null)
                        {
                            // Don't try to delete "System Volume Information" (we run into this on the build server).

                            if (!(subdirectory.Name == "System Volume Information" && (subdirectory.Attributes & FileAttributes.System) == FileAttributes.System))
                                subdirectory.Delete(true);
                        }
                        else
                        {
                            subinfo.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Failed to delete '" + subinfo.FullName + "'.", ex);
                    }
                }
            }

            return true;
        }
    }
}