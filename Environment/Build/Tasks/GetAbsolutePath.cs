using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    public class GetAbsolutePath
        : Task
    {
        private string _relativePath;
        private string _relativeToFolder;
        private string _absolutePath;

        public string RelativePath
        {
            get { return _relativePath ?? string.Empty; }
            set { _relativePath = value; }
        }

        public string RelativeToFolder
        {
            get { return _relativeToFolder ?? string.Empty; }
            set { _relativeToFolder = value; }
        }

        [Output]
        public string AbsolutePath
        {
            get { return _absolutePath ?? string.Empty; }
        }

        public override bool Execute()
        {
            try
            {
                // If it is in fact a url, eg for SVN, then convert.

                var isUrl = false;
                var relativeToFolder = _relativeToFolder;

                if (_relativeToFolder.StartsWith("http://"))
                {
                    isUrl = true;
                    relativeToFolder = _relativeToFolder.Replace("http://", "c:\\").Replace("/", "\\");
                }

                var absolutePath = FilePath.GetAbsolutePath(_relativePath, relativeToFolder);

                _absolutePath = isUrl ? absolutePath.Replace("c:\\", "http://").Replace("\\", "/") : absolutePath;

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }
    }
}