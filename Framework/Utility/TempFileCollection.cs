using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace LinkMe.Framework.Utility
{
    public class TempFileCollection
        : IDisposable
    {
        private string _tempPath;
        private List<string> _filePaths = new List<string>();

        public TempFileCollection()
        {
        }

        public TempFileCollection(HttpFileCollection files)
            : this(files, 0)
        {
        }

        /// <summary>
        /// Creates a TempFileCollection from a collection of files uploaded through HTTP. The uploaded files are saved to
        /// a temporary folder. Duplicate file names are handled.
        /// </summary>
        public TempFileCollection(HttpFileCollection files, int maxFileSizeInBytes)
        {
            if (files == null || files.Count == 0)
                return;

            for (int index = 0; index < files.Count; index++)
            {
                HttpPostedFile file = files.Get(index);
                if (!string.IsNullOrEmpty(file.FileName) &&
                    (maxFileSizeInBytes == 0 || file.ContentLength <= maxFileSizeInBytes))
                {
                    string filePath = GetNewTempFilePath(file.FileName);
                    file.SaveAs(filePath);
                    _filePaths.Add(filePath);
                }
            }
        }

        /// <summary>
        /// Creates a TempFileCollection from a single file uploaded through HTTP. The uploaded file is saved to a
        /// temporary folder.
        /// </summary>
        public TempFileCollection(HttpPostedFile singlePostedFile, int maxFileSizeInBytes)
        {
            if (singlePostedFile == null)
                return;

            if (!string.IsNullOrEmpty(singlePostedFile.FileName) && maxFileSizeInBytes == 0 ||
                singlePostedFile.ContentLength <= maxFileSizeInBytes)
            {
                string filePath = GetNewTempFilePath(singlePostedFile.FileName);

                singlePostedFile.SaveAs(filePath);
                _filePaths.Add(filePath);
            }
        }

        /// <summary>
        /// Creates a TempFileCollection from a stream. The source stream is copied to a temporary file with
        /// the specified name.
        /// </summary>
        public TempFileCollection(string fileName, Stream sourceStream)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("The file name must be specified.");
            if (sourceStream == null)
                throw new ArgumentNullException("sourceStream");

            string filePath = GetNewTempFilePath(fileName);

            using (var tempFile = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                StreamUtil.CopyStream(sourceStream, tempFile);
            }

            _filePaths.Add(filePath);
        }

        ~TempFileCollection()
        {
            DisposeInternal();
        }

        #region IDisposable members

        public void Dispose()
        {
            DisposeInternal();
            GC.SuppressFinalize(this);
        }

        #endregion

        public IList<string> FilePaths
        {
            get { return _filePaths; }
        }

        /// <summary>
        /// Creates a new, temporary file with the specified name in a temporary directory and
        /// returns its full path. Similar to Path.GetTempFileName(), except that this allows the file name
        /// to be specified. The caller must close the returned FileStream object.
        /// </summary>
        /// <returns>An open FileStream for the newly created temporary file.</returns>
        public FileStream AddNewTempFile(string fileName)
        {
            string filePath = GetNewTempFilePath(fileName);
            FileStream stream = File.Create(filePath);
            _filePaths.Add(filePath);
            return stream;
        }

        private string GetTempDirectoryPath()
        {
            if (_tempPath == null)
            {
                _tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n"));
                Directory.CreateDirectory(_tempPath);
            }

            return _tempPath;
        }

        private void DisposeInternal()
        {
            if (_tempPath != null)
            {
                if (Directory.Exists(_tempPath))
                {
                    try
                    {
                        Directory.Delete(_tempPath, true);
                    }
                    catch (Exception)
                    {
                    }
                }

                _tempPath = null;
                _filePaths = null;
            }
        }

        private string GetNewTempFilePath(string originalFileName)
        {
            string fileName = Path.GetFileName(originalFileName); // Must be done BEFORE stripping invalid chars
            fileName = FileSystem.GetValidFileName(fileName);

            if (fileName.Length == 0)
            {
                // For the unlikely case that all filename characters are invalid.
                fileName = Guid.NewGuid().ToString("n");
            }

            string filePath = Path.Combine(GetTempDirectoryPath(), fileName);

            // In the unlikely case that the user supplied multiple files with the same name
            // store them in separate subdirectories.

            if (File.Exists(filePath))
            {
                string extraDir = Path.Combine(GetTempDirectoryPath(), Guid.NewGuid().ToString("n"));
                Directory.CreateDirectory(extraDir);
                filePath = Path.Combine(extraDir, fileName);
            }

            return filePath;
        }
    }
}
