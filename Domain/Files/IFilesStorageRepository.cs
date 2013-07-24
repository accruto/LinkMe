using System.Collections.Generic;
using System.IO;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Files
{
    public interface IFilesStorageRepository
    {
        void SaveFile(FileContents fileContents, FileReference fileReference);
        Stream OpenFile(FileReference fileReference);
        Stream OpenZippedFiles(IEnumerable<FileReference> fileReferences);
        TempFileCollection SaveTempFile(string fileContents, string fileName);
        TempFileCollection SaveTempFile(byte[] fileContents, string fileName);
    }
}
