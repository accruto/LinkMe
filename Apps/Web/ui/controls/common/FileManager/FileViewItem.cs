using System.Globalization;
using System.IO;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    sealed class FileViewItem
    {
        string _thumbnailImage;
        string _smallImage;
        string _largeImage;
        string _size;
        string _type;
        string _modified;
        string _cliendID;
        readonly FileSystemInfo _fsi;
        readonly FileView _fileView;

        public FileSystemInfo FileSystemInfo
        {
            get { return _fsi; }
        }

        public bool IsDirectory
        {
            get { return _fsi is DirectoryInfo; }
        }

        public string ClientID
        {
            get { return _cliendID; }
            set { _cliendID = value; }
        }

        public bool CanBeRenamed
        {
            get { return true; }
        }

        public string SmallImage
        {
            get
            {
                if (_smallImage == null)
                    _smallImage = _fileView.Controller.GetItemSmallImage(_fsi);
                return _smallImage;
            }
        }

        public string LargeImage
        {
            get
            {
                if (_largeImage == null)
                    _largeImage = _fileView.Controller.GetItemLargeImage(_fsi);
                return _largeImage;
            }
        }

        public string ThumbnailImage
        {
            get
            {
                if (_thumbnailImage == null)
                    _thumbnailImage = _fileView.Controller.GetItemThumbnailImage(_fsi, _fileView.CurrentDirectory);
                return _thumbnailImage;
            }
        }

        public string Info
        {
            get { return string.Empty; }
        }

        public string Size
        {
            get
            {
                if (_size == null)
                    _size = _fsi is DirectoryInfo ? "&nbsp;" : FileSizeToString(((FileInfo)_fsi).Length);
                return _size;
            }
        }

        public string Type
        {
            get
            {
                if (_type == null)
                    _type = GetItemType(_fsi);
                return _type;
            }
        }

        public string Modified
        {
            get
            {
                if (_modified == null)
                    _modified = _fsi.LastWriteTime.ToString("g", null);
                return _modified;
            }
        }

        public string Name
        {
            get { return _fsi.Name; }
        }

        internal FileViewItem(FileSystemInfo fsi, FileView fileView)
        {
            this._fsi = fsi;
            this._fileView = fileView;
        }

        string GetItemType(FileSystemInfo fsi)
        {
            if (fsi is DirectoryInfo)
                return _fileView.Controller.GetResourceString("File_Folder", "File Folder");
            else
            {
                FileInfo file = (FileInfo)fsi;
                FileType ft = _fileView.Controller.GetFileType(file);
                if (ft != null && ft.Name.Length > 0)
                    return ft.Name;
                else
                {
                    return file.Extension.ToUpper(CultureInfo.InvariantCulture).TrimStart('.') + " File";
                }
            }

        }

        static string FileSizeToString(long size)
        {
            if (size < 1024)
                return size.ToString(null, null) + " B";
            else if (size < 1048576)
                return (size / 1024).ToString(null, null) + " KB";
            else
                return (size / 1048576).ToString(null, null) + " MB";

        }
    }
}
