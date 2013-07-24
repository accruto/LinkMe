using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Files.Commands
{
    public interface IFilesCommand
    {
        FileReference SaveFile(FileType fileType, FileContents fileContents, string fileName);
        TempFileCollection SaveTempFile(string fileContents, string fileName);
        TempFileCollection SaveTempFile(byte[] fileContents, string fileName);
    }
}