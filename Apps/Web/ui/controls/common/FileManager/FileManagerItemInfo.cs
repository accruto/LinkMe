using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class FileManagerItemInfo
    {
        private string _fileManagerPath;
        private string _virtualPath;
        private string _physicalPath;
        private FileInfo _file;
        private DirectoryInfo _directory;

        public string FileManagerPath
        {
            get { return _fileManagerPath; }
        }

        [Obsolete("this property is obsolete, use VirtualPath instead")]
        public string AbsolutePath
        {
            get { return VirtualPath; }
        }

        public string VirtualPath
        {
            get { return _virtualPath; }
        }

        internal FileInfo File
        {
            get
            {
                if (_file == null)
                    _file = new FileInfo(PhysicalPath);
                return _file;
            }
        }

        internal DirectoryInfo Directory
        {
            get
            {
                if (_directory == null)
                    _directory = new DirectoryInfo(PhysicalPath);
                return _directory;
            }
        }

        public string PhysicalPath
        {
            get { return _physicalPath; }
        }

        internal FileManagerItemInfo(string fileManagerPath, string virtualPath, string phisicalPath)
        {
            _fileManagerPath = fileManagerPath;
            _virtualPath = virtualPath;
            _physicalPath = phisicalPath;
        }

        internal void EnsureDirectoryExists()
        {
            if (!Directory.Exists)
                Directory.Create();
        }
    }
}
