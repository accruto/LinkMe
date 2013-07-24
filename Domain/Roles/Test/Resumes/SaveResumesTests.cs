using System.IO;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Resumes
{
    [TestClass, Ignore]
    public class SaveResumesTests
    {
        [TestMethod]
        public void SaveAllTestResumes()
        {
            // This method saves all the test resumes so they can be used in other tests, both automated and manual.
            // Should only need to be run when the list changes etc.

            foreach (var resume in TestResume.AllResumes)
                Save(resume);
        }

        private static void Save(TestResume resume)
        {
            var data = resume.GetData();
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\" + resume.FileName, RuntimeEnvironment.GetSourceFolder());

            using (var stream = new MemoryStream(data))
            {
                stream.Position = 0;
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    StreamUtil.CopyStream(stream, fileStream);
                }
            }
        }
    }
}
