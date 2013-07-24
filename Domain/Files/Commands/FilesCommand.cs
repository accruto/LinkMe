using System;
using System.IO;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Domain.Files.Commands
{
    public class FilesCommand
        : IFilesCommand
    {
        private readonly IFilesRepository _repository;
        private readonly IFilesStorageRepository _storageRepository;

        public FilesCommand(IFilesRepository repository, IFilesStorageRepository storageRepository)
        {
            _repository = repository;
            _storageRepository = storageRepository;
        }

        FileReference IFilesCommand.SaveFile(FileType fileType, FileContents fileContents, string fileName)
        {
            if (fileContents == null)
                throw new ArgumentNullException("fileContents");
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("The file name must be specified.", "fileName");

            // Create the file reference for this file.

            bool isExistingFile;
            var fileReference = CreateFileReference(fileType, fileName, fileContents.Type, fileContents.Length, fileContents.Hash, out isExistingFile);

            // If the file already exists then there is no need to save it.

            if (!isExistingFile)
                _storageRepository.SaveFile(fileContents, fileReference);

            return fileReference;
        }

        TempFileCollection IFilesCommand.SaveTempFile(string fileContents, string fileName)
        {
            return _storageRepository.SaveTempFile(fileContents, fileName);
        }

        TempFileCollection IFilesCommand.SaveTempFile(byte[] fileContents, string fileName)
        {
            return _storageRepository.SaveTempFile(fileContents, fileName);
        }

        /// <summary>
        /// Gets an existing FileReference or creates a new one with the specified data and metadata. This method
        /// filters out duplicates on two levels:
        /// 1) The same data is not stored in the file system twice. Instead multiple FileReference instances
        /// are given the same FileData instance (which corresponds to one physical file being stored).
        /// 2) The same metadata is not stored in the FileReference table twice. If the metadata matches exactly
        /// this method returns the existing FileReference.
        /// </summary>
        private FileReference CreateFileReference(FileType fileType, string fileName, string defaultContentType, int contentLength, byte[] contentHash, out bool isExistingFile)
        {
            fileName = Path.GetFileName(fileName); // Strip the path, if any.
            var mediaType = MediaType.GetMediaTypeFromExtension(Path.GetExtension(fileName), defaultContentType);

            // First look for an existing file reference.

            var fileReference = _repository.GetFileReference(fileType, fileName, mediaType, contentLength, contentHash);
            if (fileReference != null)
            {
                isExistingFile = true;
                return fileReference;
            }

            // Look for data corresponding to the file.

            var fileData = _repository.GetFileData(fileType, contentLength, contentHash);
            if (fileData == null)
            {
                // Need to create and save.

                isExistingFile = false;
                fileData = new FileData
                               {
                                   Id = Guid.NewGuid(),
                                   FileType = fileType,
                                   FileExtension = Path.GetExtension(fileName).ToLower(),
                                   ContentLength = contentLength,
                                   ContentHash = contentHash
                               };

                _repository.CreateFileData(fileData);
            }
            else
            {
                isExistingFile = true;
            }

            fileReference = new FileReference
                                {
                                    Id = Guid.NewGuid(),
                                    CreatedTime = DateTime.Now,
                                    MediaType = mediaType,
                                    FileName = fileName,
                                    FileData = fileData
                                };

            _repository.CreateFileReference(fileReference);
            return fileReference;
        }
    }
}