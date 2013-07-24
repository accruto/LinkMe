using System.Collections.Generic;
using System.IO;
using LinkMe.Framework.Utility.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Files
{
    [TestClass]
    public class FileFormatterTest
    {
        [TestMethod]
        public void TestFormatList()
        {
            var result1 = new TestCsvResult("name1", "recruiter1", false);
            var result2 = new TestCsvResult("name2", "recruiter2", false);
            var list = new List<TestCsvResult> {result1, result2};

            using (var memoryStream = new MemoryStream())
            {
                var formatter = new FileFormatter(new DelimitedFormatProvider(Delimiter.Comma));
                formatter.Format(list, memoryStream);
                
                var streamReader = new StreamReader(memoryStream);
                memoryStream.Position = 0;
                var contents = streamReader.ReadToEnd();
                Assert.AreEqual(
                    "\"OrganisationName\",\"EmployerRole\",\"SendNewsletters\""
                    + System.Environment.NewLine
                    + "\"name1\",\"recruiter1\",\"False\""
                    + System.Environment.NewLine
                    + "\"name2\",\"recruiter2\",\"False\"", contents);
            }
        }

        public class TestCsvResult
        {
            private string _organisationName;
            private string _employerRole;
            private bool _sendNewsletters;
            private string _notACsvField;

            public TestCsvResult(string organisationName, string employerRole, bool sendNewsletters)
            {
                _organisationName = organisationName;
                _employerRole = employerRole;
                _sendNewsletters = sendNewsletters;
                _notACsvField = "You should not see this in CSV!";
            }

            [FileItemOrder(1)]
            public string OrganisationName
            {
                get { return _organisationName; }
                set { _organisationName = value; }
            }

            [FileItemOrder(2)]
            public string EmployerRole
            {
                get { return _employerRole; }
                set { _employerRole = value; }
            }

            [FileItemOrder(3)]
            public bool SendNewsletters
            {
                get { return _sendNewsletters; }
                set { _sendNewsletters = value; }
            }

            public string NotACsvField
            {
                get { return _notACsvField; }
                set { _notACsvField = value; }
            }
        }
    }
}