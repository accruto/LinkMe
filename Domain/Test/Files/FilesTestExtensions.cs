using System.IO;
using System.Reflection;
using System.Text;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;

namespace LinkMe.Domain.Test.Files
{
    public static class FilesTestExtensions
    {
        private const string ContentsFormat = "These are the contents for file {0}";
        private const string FileNameFormat = "TestFileName{0}.txt";
        private const string PhotoFileNameFormat = "photo{0}.jpg";

        public static FileReference CreateTestFile(this IFilesCommand filesCommand, FileType fileType)
        {
            return filesCommand.CreateTestFile(1, fileType);
        }

        public static FileReference CreateTestFile(this IFilesCommand filesCommand, int index, FileType fileType)
        {
            using (var stream = new MemoryStream(new ASCIIEncoding().GetBytes(string.Format(ContentsFormat, index))))
            {
                return filesCommand.SaveFile(fileType, new StreamFileContents(stream), string.Format(FileNameFormat, index));
            }
        }

        public static FileReference CreateTestPhoto(this IFilesCommand filesCommand, int index, FileType fileType)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(FilesTestExtensions), "TestPhoto.jpg"))
            {
                return filesCommand.SaveFile(fileType, new StreamFileContents(stream), string.Format(PhotoFileNameFormat, index));
            }
        }
    }
}