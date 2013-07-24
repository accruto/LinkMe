using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Files.Queries
{
    public class FilesQuery
        : IFilesQuery
    {
        private readonly IFilesRepository _repository;
        private readonly IFilesStorageRepository _storageRepository;

        public FilesQuery(IFilesRepository repository, IFilesStorageRepository storageRepository)
        {
            _repository = repository;
            _storageRepository = storageRepository;
        }

        FileReference IFilesQuery.GetFileReference(Guid id)
        {
            return _repository.GetFileReference(id);
        }

        IList<FileReference> IFilesQuery.GetFileReferences(IEnumerable<Guid> ids, Range range)
        {
            return _repository.GetFileReferences(ids, range);
        }

        Stream IFilesQuery.OpenFile(FileReference fileReference)
        {
            if (fileReference == null)
                throw new ArgumentNullException("fileReference");

            return _storageRepository.OpenFile(fileReference);
        }

        Stream IFilesQuery.OpenZippedFiles(IEnumerable<FileReference> fileReferences)
        {
            return _storageRepository.OpenZippedFiles(fileReferences);
        }
    }
}