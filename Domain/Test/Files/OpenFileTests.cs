using System.IO;
using System.Linq;
using Ionic.Zip;
using LinkMe.Domain.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Files
{
    [TestClass]
    public class OpenFileTests
        : FilesTests
    {
        [TestMethod]
        public void TestOpenFile()
        {
            const string contents = "Will this be saved?";
            const string fileName = "Sample1.txt";
            const FileType fileType = FileType.Resume;
            var fileReference = SaveFile(fileType, contents, fileName);

            Assert.AreEqual(contents, OpenFile(fileReference));
            var openedContents = OpenZippedFile(fileReference);
            Assert.AreEqual(1, openedContents.Length);
            Assert.AreEqual(contents, openedContents[0]);
        }

        [TestMethod]
        public void TestOpenFiles()
        {
            const string contents1 = "Will this be saved 1?";
            const string fileName1 = "Sample1.txt";
            const string contents2 = "Will this be saved 2?";
            const string fileName2 = "Sample2.txt";
            const FileType fileType = FileType.Resume;
            var fileReference1 = SaveFile(fileType, contents1, fileName1);
            var fileReference2 = SaveFile(fileType, contents2, fileName2);

            Assert.AreEqual(contents1, OpenFile(fileReference1));
            Assert.AreEqual(contents2, OpenFile(fileReference2));
            var openedContents = OpenZippedFile(fileReference1, fileReference2);
            Assert.AreEqual(2, openedContents.Length);
            Assert.AreEqual(contents1, openedContents[0]);
            Assert.AreEqual(contents2, openedContents[1]);
        }

        [TestMethod]
        public void TestOpen2FilesSameName()
        {
            const string fileName = "Sample.txt";
            const string contents1 = "Will this be saved 1?";
            const string contents2 = "Will this be saved 2?";
            const FileType fileType = FileType.Resume;
            var fileReference1 = SaveFile(fileType, contents1, fileName);
            var fileReference2 = SaveFile(fileType, contents2, fileName);

            Assert.AreEqual(contents1, OpenFile(fileReference1));
            Assert.AreEqual(contents2, OpenFile(fileReference2));

            // Opening a zipped file with files of same name changes the names.

            fileReference2.FileName = fileReference2.FileName + " (2)";
            var openedContents = OpenZippedFile(fileReference1, fileReference2);
            Assert.AreEqual(2, openedContents.Length);
            Assert.AreEqual(contents1, openedContents[0]);
            Assert.AreEqual(contents2, openedContents[1]);
        }

        [TestMethod]
        public void TestOpenMoreFilesSameName()
        {
            const string fileName1 = "Sample1.txt";
            const string fileName2 = "Sample2.txt";

            const string contents1 = "Will this be saved 1?";
            const string contents2 = "Will this be saved 2?";
            const string contents3 = "Will this be saved 3?";
            const string contents4 = "Will this be saved 4?";
            const string contents5 = "Will this be saved 5?";

            const FileType fileType = FileType.Resume;
            var fileReference1 = SaveFile(fileType, contents1, fileName1);
            var fileReference2 = SaveFile(fileType, contents2, fileName1);
            var fileReference3 = SaveFile(fileType, contents3, fileName2);
            var fileReference4 = SaveFile(fileType, contents4, fileName2);
            var fileReference5 = SaveFile(fileType, contents5, fileName2);

            Assert.AreEqual(contents1, OpenFile(fileReference1));
            Assert.AreEqual(contents2, OpenFile(fileReference2));
            Assert.AreEqual(contents3, OpenFile(fileReference3));
            Assert.AreEqual(contents4, OpenFile(fileReference4));
            Assert.AreEqual(contents5, OpenFile(fileReference5));

            // Opening a zipped file with files of same name changes the names.

            fileReference2.FileName = fileReference2.FileName + " (2)";
            fileReference4.FileName = fileReference4.FileName + " (2)";
            fileReference5.FileName = fileReference5.FileName + " (3)";

            var openedContents = OpenZippedFile(fileReference1, fileReference2, fileReference3, fileReference4, fileReference5);
            Assert.AreEqual(5, openedContents.Length);
            Assert.AreEqual(contents1, openedContents[0]);
            Assert.AreEqual(contents2, openedContents[1]);
            Assert.AreEqual(contents3, openedContents[2]);
            Assert.AreEqual(contents4, openedContents[3]);
            Assert.AreEqual(contents5, openedContents[4]);
        }

        [TestMethod]
        public void TestOpenFilesNameAlreadyExists()
        {
            const string fileName1 = "Sample.txt";
            const string fileName2 = "Sample.txt (2)";

            const string contents1 = "Will this be saved 1?";
            const string contents2 = "Will this be saved 2?";
            const string contents3 = "Will this be saved 3?";
            const FileType fileType = FileType.Resume;
            var fileReference1 = SaveFile(fileType, contents1, fileName1);
            var fileReference2 = SaveFile(fileType, contents2, fileName2);
            var fileReference3 = SaveFile(fileType, contents3, fileName1);

            Assert.AreEqual(contents1, OpenFile(fileReference1));
            Assert.AreEqual(contents2, OpenFile(fileReference2));
            Assert.AreEqual(contents3, OpenFile(fileReference3));

            // Opening a zipped file with files of same name changes the names.

            fileReference3.FileName = fileReference3.FileName + " (3)";
            var openedContents = OpenZippedFile(fileReference1, fileReference2, fileReference3);
            Assert.AreEqual(3, openedContents.Length);
            Assert.AreEqual(contents1, openedContents[0]);
            Assert.AreEqual(contents2, openedContents[1]);
            Assert.AreEqual(contents3, openedContents[2]);
        }

        protected string[] OpenZippedFile(params FileReference[] fileReferences)
        {
            using (var stream = _filesQuery.OpenZippedFiles(fileReferences))
            {
                using (var zipFile = ZipFile.Read(stream))
                {
                    Assert.AreEqual(fileReferences.Length, zipFile.Entries.Count);
                    return (from r in fileReferences select OpenFile(zipFile, r.FileName)).ToArray();
                }
            }
        }

        private static string OpenFile(ZipFile zipFile, string fileName)
        {
            var entry = (from e in zipFile.Entries where e.FileName == fileName select e).Single();
            
            using (var memoryStream = new MemoryStream())
            {
                entry.Extract(memoryStream);
                memoryStream.Position = 0;
                var reader = new StreamReader(memoryStream);
                return reader.ReadToEnd();
            }
        }
    }
}
