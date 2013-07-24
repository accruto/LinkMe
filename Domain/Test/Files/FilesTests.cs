using System.IO;
using System.Text;
using LinkMe.Domain.Data;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Data;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Files
{
    public abstract class FilesTests
        : TestClass
    {
        protected readonly IFilesCommand _filesCommand;
        protected readonly IFilesQuery _filesQuery;

        protected FilesTests()
        {
            var repository = new FilesRepository(Resolve<IDataContextFactory>());
            var storageRepository = new FilesStorageRepository("C:\\LinkMe\\UserFiles");
            _filesCommand = new FilesCommand(repository, storageRepository);
            _filesQuery = new FilesQuery(repository, storageRepository);
        }

        [TestInitialize]
        public void FilesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected FileReference SaveFile(FileType fileType, string contents, string fileName)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(contents)))
            {
                return _filesCommand.SaveFile(fileType, new StreamFileContents(stream), fileName);
            }
        }

        protected string OpenFile(FileReference fileReference)
        {
            using (var stream = _filesQuery.OpenFile(fileReference))
            {
                var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }
    }
}
