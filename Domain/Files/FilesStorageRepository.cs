using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Files
{
    public class FilesStorageRepository
        : IFilesStorageRepository
    {
        private readonly string _rootPath;

        public FilesStorageRepository(string rootPath)
        {
            _rootPath = rootPath;
        }

        void IFilesStorageRepository.SaveFile(FileContents fileContents, FileReference fileReference)
        {
            var filePath = GetFilePath(fileReference.FileData);

            try
            {
                EnsureFolderExists(filePath.Folder);
                fileContents.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to save file '{0}'.", filePath.FullPath), ex);
            }
        }

        Stream IFilesStorageRepository.OpenFile(FileReference fileReference)
        {
            var filePath = GetFilePath(fileReference.FileData);
            try
            {
                return new FileStream(filePath.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to open file " + filePath.FullPath, ex);
            }
        }

        Stream IFilesStorageRepository.OpenZippedFiles(IEnumerable<FileReference> fileReferences)
        {
            using (var zipFile = new ZipFile())
            {
                var fileNames = new List<string>();
                foreach (var fileReference in fileReferences)
                    zipFile.AddFile(GetFilePath(fileReference.FileData).FullPath).FileName = GetFileName(fileNames, fileReference.FileName);

                var stream = new MemoryStream();
                zipFile.Save(stream);

                stream.Position = 0;
                return stream;
            }
        }

        private static string GetFileName(ICollection<string> fileNames, string fileName)
        {
            // Make names unique.

            if (fileNames.Contains(fileName))
            {
                var index = 2;
                var newFileName = fileName + " (" + index + ")";
                while (fileNames.Contains(newFileName))
                {
                    ++index;
                    newFileName = fileName + " (" + index + ")";
                }

                fileNames.Add(newFileName);
                return newFileName;
            }

            fileNames.Add(fileName);
            return fileName;
        }

        TempFileCollection IFilesStorageRepository.SaveTempFile(string fileContents, string fileName)
        {
            var tempCollection = new TempFileCollection();
            try
            {
                using (var stream = tempCollection.AddNewTempFile(fileName))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(fileContents);
                    }
                }
            }
            catch
            {
                tempCollection.Dispose();
                throw;
            }

            return tempCollection;
        }

        TempFileCollection IFilesStorageRepository.SaveTempFile(byte[] fileContents, string fileName)
        {
            var tempCollection = new TempFileCollection();
            try
            {
                using (var stream = tempCollection.AddNewTempFile(fileName))
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        writer.Write(fileContents);
                    }
                }
            }
            catch
            {
                tempCollection.Dispose();
                throw;
            }

            return tempCollection;
        }

        private FilePath GetFilePath(FileData file)
        {
            var relativePath = Path.Combine(file.FileType.ToString(), GetFolderName(file.Id));
            return new FilePath
            {
                Folder = Path.Combine(_rootPath, relativePath),
                FileName = file.Id + file.FileExtension
            };
        }

        private static string GetFolderName(Guid guid)
        {
            var folderName = guid.ToString();
            return folderName.Remove(1, folderName.Length - 2);
        }

        private static void EnsureFolderExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch (IOException ex)
                {
                    // It's possible, though highly unlikely, that another thread created the same
                    // directory just now, which is fine.

                    if (!Directory.Exists(folderPath))
                        throw new ApplicationException("Failed to create folder '" + folderPath + "'.", ex);
                }
            }
        }
    }
}
