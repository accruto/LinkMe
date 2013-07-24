using System.IO;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Lens;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Lens
{
    /// <summary>
    /// These are real resumes from production that were giving errors when they were deserialized.
    /// Instead, every effort should be made to ensure they are parsed without error, ignoring fields that are giving errors etc.
    /// </summary>
    [TestClass]
    public class DeserializeSampleTests
        : TestClass
    {
        private readonly IResumesCommand _resumesCommand = Resolve<IResumesCommand>();
        private readonly IParseResumeXmlCommand _parseResumeXmlCommand = Resolve<IParseResumeXmlCommand>();

        [TestInitialize]
        public void DeserializeSampleTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUnrepresentableDate()
        {
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\UnrepresentableDate.xml", RuntimeEnvironment.GetSourceFolder());
            var xml = ReadFile(filePath);

            var parsedResume = _parseResumeXmlCommand.ParseResumeXml(xml);
            _resumesCommand.CreateResume(parsedResume.Resume);
        }

        private static string ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}