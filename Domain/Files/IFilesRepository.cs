using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Files
{
    public interface IFilesRepository
    {
        FileReference GetFileReference(Guid id);
        IList<FileReference> GetFileReferences(IEnumerable<Guid> ids, Range range);
        FileReference GetFileReference(FileType fileType, string fileName, string mediaType, int contentLength, byte[] contentHash);

        FileData GetFileData(FileType fileType, int contentLength, byte[] contentHash);
        void CreateFileReference(FileReference fileReference);
        void CreateFileData(FileData fileData);
    }
}
