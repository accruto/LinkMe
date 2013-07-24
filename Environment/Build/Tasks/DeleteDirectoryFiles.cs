using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    public class DeleteDirectoryFiles
        : Task
    {
        private string _directory;
        private ITaskItem[] _files;

        [Required]
        public string Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }

        [Required]
        public ITaskItem[] Files
        {
            get { return _files; }
            set { _files = value; }
        }

        public override bool Execute()
        {
            foreach (var file in _files)
            {
                var path = FilePath.GetAbsolutePath(file.ItemSpec, _directory);
                DeleteFile(path);
                DeleteFile(Path.ChangeExtension(path, ".pdb"));
                DeleteFile(Path.ChangeExtension(path, ".xml"));
            }

            return true;
        }

        private static void DeleteFile(string path)
        {
            var info = new FileInfo(path);
            if (info.Exists)
            {
                try
                {
                    info.Delete();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to delete '" + info.FullName + "'.", ex);
                }
            }
        }
    }
}