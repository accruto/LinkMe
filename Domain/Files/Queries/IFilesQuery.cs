using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Files.Queries
{
    public interface IFilesQuery
    {
        FileReference GetFileReference(Guid id);
        IList<FileReference> GetFileReferences(IEnumerable<Guid> ids, Range range);

        Stream OpenFile(FileReference fileReference);
        Stream OpenZippedFiles(IEnumerable<FileReference> fileReferences);
    }
}