using System.IO;
using System.Text;
using LinkMe.Domain.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Files
{
    [TestClass]
    public class SaveFileTests
        : FilesTests
    {
        [TestMethod]
        public void TestSaveFileDifferentName()
        {
            const string contents = "Will this be saved?";
            const string fileName1 = "Sample1.txt";
            const string fileName2 = "Sample2.txt";
            const FileType fileType = FileType.Resume;

            // Save.

            var fileReference1 = SaveFile(fileType, contents, fileName1);
            var fileReference2 = SaveFile(fileType, contents, fileName2);

            // Check.

            AssertAreNotEqual(fileReference1, fileName1, fileReference2, fileName2);

            // File data should be reused for the same content and type.

            AssertAreEqual(fileReference1.FileData, fileType, contents, fileReference2.FileData, fileType, contents);
        }

        [TestMethod]
        public void TestSaveFileDifferentContents()
        {
            const string contents1 = "Will this be saved1?";
            const string contents2 = "Will this be saved2?";
            const string fileName = "Sample.txt";
            const FileType fileType = FileType.Resume;

            // Save.

            var fileReference1 = SaveFile(fileType, contents1, fileName);
            var fileReference2 = SaveFile(fileType, contents2, fileName);

            // Check.

            AssertAreNotEqual(fileReference1, fileName, fileReference2, fileName);
            AssertAreNotEqual(fileReference1.FileData, fileType, contents1, fileReference2.FileData, fileType, contents2);
        }

        [TestMethod]
        public void TestSaveFileDifferentType()
        {
            const string contents = "Will this be saved?";
            const string fileName = "Sample.txt";
            const FileType fileType1 = FileType.Resume;
            const FileType fileType2 = FileType.ProfilePhoto;

            // Save.

            var fileReference1 = SaveFile(fileType1, contents, fileName);
            var fileReference2 = SaveFile(fileType2, contents, fileName);

            // Check.

            AssertAreNotEqual(fileReference1, fileName, fileReference2, fileName);
            AssertAreNotEqual(fileReference1.FileData, fileType1, contents, fileReference2.FileData, fileType2, contents);
        }

        [TestMethod]
        public void TestSaveFileTwice()
        {
            const FileType fileType = FileType.Resume;
            const string contents = "Will this be saved?";
            const string fileName = "Sample.txt";

            // Save it one time.

            FileReference fileReference1;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(contents)))
            {
                fileReference1 = _filesCommand.SaveFile(fileType, new StreamFileContents(stream), fileName);
            }

            // Save it again.

            FileReference fileReference2;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(contents)))
            {
                fileReference2 = _filesCommand.SaveFile(fileType, new StreamFileContents(stream), fileName);
            }

            // Check.

            AssertAreEqual(fileReference1, fileName, fileReference2, fileName);
            AssertAreEqual(fileReference1.FileData, fileType, contents, fileReference2.FileData, fileType, contents);
        }

        [TestMethod]
        public void TestSaveTempFile()
        {
            const string content = "This is the content of the file";
            const string fileName = "Sample.txt";

            string filePath;
            using (var tempFiles = _filesCommand.SaveTempFile(content, fileName))
            {
                filePath = tempFiles.FilePaths[0];
                Assert.AreEqual(0, filePath.IndexOf(Path.GetTempPath()));
                Assert.IsTrue(File.Exists(filePath));
            }

            Assert.IsFalse(File.Exists(filePath));
        }

        private static void AssertAreEqual(FileReference fileReference1, string fileName1, FileReference fileReference2, string fileName2)
        {
            Assert.AreEqual(fileReference1.Id, fileReference2.Id);
            AssertFileReference(fileReference1, fileName1, fileReference2, fileName2);
        }

        private static void AssertAreNotEqual(FileReference fileReference1, string fileName1, FileReference fileReference2, string fileName2)
        {
            Assert.AreNotEqual(fileReference1.Id, fileReference2.Id);
            AssertFileReference(fileReference1, fileName1, fileReference2, fileName2);
        }

        private static void AssertFileReference(FileReference fileReference1, string fileName1, FileReference fileReference2, string fileName2)
        {
            if (fileName1 == fileName2)
            {
                Assert.AreEqual(fileReference1.FileName, fileReference2.FileName);
                Assert.AreEqual(fileReference1.FileName, fileName1);
                Assert.AreEqual(fileReference2.FileName, fileName2);
            }
            else
            {
                Assert.AreNotEqual(fileReference1.FileName, fileReference2.FileName);
                Assert.AreEqual(fileReference1.FileName, fileName1);
                Assert.AreEqual(fileReference2.FileName, fileName2);
            }
        }

        private static void AssertAreEqual(FileData fileData1, FileType fileType1, string fileContents1, FileData fileData2, FileType fileType2, string fileContents2)
        {
            Assert.AreEqual(fileData1.Id, fileData2.Id);
            AssertFileData(fileData1, fileType1, fileContents1, fileData2, fileType2, fileContents2);
        }

        private static void AssertAreNotEqual(FileData fileData1, FileType fileType1, string fileContents1, FileData fileData2, FileType fileType2, string fileContents2)
        {
            Assert.AreNotEqual(fileData1.Id, fileData2.Id);
            AssertFileData(fileData1, fileType1, fileContents1, fileData2, fileType2, fileContents2);
        }

        private static void AssertFileData(FileData fileData1, FileType fileType1, string fileContents1, FileData fileData2, FileType fileType2, string fileContents2)
        {
            if (fileType1 == fileType2)
            {
                Assert.AreEqual(fileData1.FileType, fileData2.FileType);
                Assert.AreEqual(fileData1.FileType, fileType1);
                Assert.AreEqual(fileData1.FileType, fileType2);
            }
            else
            {
                Assert.AreNotEqual(fileData1.FileType, fileData2.FileType);
                Assert.AreEqual(fileData1.FileType, fileType1);
                Assert.AreEqual(fileData2.FileType, fileType2);
            }

            if (fileContents1 == fileContents2)
            {
                Assert.AreEqual(fileData1.ContentLength, fileData2.ContentLength);
                Assert.AreEqual(fileData1.ContentLength, fileContents1.Length);
                Assert.AreEqual(fileData1.ContentLength, fileContents2.Length);
                AssertAreEqual(fileData1.ContentHash, fileData2.ContentHash);
            }
            else
            {
                Assert.AreEqual(fileData1.ContentLength, fileContents1.Length);
                Assert.AreEqual(fileData1.ContentLength, fileContents2.Length);
                AssertAreNotEqual(fileData1.ContentHash, fileData2.ContentHash);
            }
        }

        private static void AssertAreEqual(byte[] contentsHash1, byte[] contentsHash2)
        {
            Assert.AreEqual(contentsHash1.Length, contentsHash2.Length);
            for (var index = 0; index < contentsHash1.Length; ++index)
                Assert.AreEqual(contentsHash1[index], contentsHash2[index]);
        }

        private static void AssertAreNotEqual(byte[] contentsHash1, byte[] contentsHash2)
        {
            if (contentsHash1.Length != contentsHash2.Length)
                return;

            for (var index = 0; index < contentsHash1.Length; ++index)
            {
                if (contentsHash1[index] != contentsHash2[index])
                    return;
            }

            Assert.Fail();
        }
    }
}