using System.IO;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Lens
{
    [TestClass]
    public class PropertiesTests
        : TestClass
    {
        private readonly IParseResumeXmlCommand _parseResumeXmlCommand = Resolve<IParseResumeXmlCommand>();
        private static readonly string ResumeFile = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\TestResume1.xml", RuntimeEnvironment.GetSourceFolder());
        private const string CurrentJobLensXml = @"
            <ResDoc>
                <contact></contact>
                <resume>
                    <experience>
                        <job>
                            <title><![CDATA[My Current Job]]></title>
                            <description><![CDATA[Some description]]></description>
                            <daterange>
                                <start><![CDATA[October 2005]]></start>
                                <end><![CDATA[current]]></end>
                            </daterange>
                        </job>
                        <job>
                            <title><![CDATA[My Current Job]]></title>
                                <description><![CDATA[Some description]]></description>
                                <daterange>
                                    <start><![CDATA[February 2005]]></start>
                                    <end><![CDATA[current]]></end>
                                </daterange>
                        </job>
                        <job>
                            <title><![CDATA[Ends today]]></title>
                            <description><![CDATA[But is not current!]]></description>
                            <daterange>
                                <start><![CDATA[March 2005]]></start>
                                <end><![CDATA[April 2012]]></end>
                            </daterange>
                        </job>
                    </experience>
                </resume>
            </ResDoc>";

        [TestMethod]
        public void TestSkills()
        {
            var resume = GetResume();
            var skills = resume.Skills;
            Assert.AreEqual("second skills\r\nskill\r\nsubskills", skills);
            resume.Skills = "newSkill";
            Assert.AreEqual("newSkill", resume.Skills);
        }

        [TestMethod]
        public void TestObjective()
        {
            var resume = GetResume();
            var objective = resume.Objective;
            Assert.AreEqual("first objective\r\nsecond objective", objective);
            resume.Objective = "newObjective";
            Assert.AreEqual("newObjective", resume.Objective);
        }

        [TestMethod]
        public void TestSummary()
        {
            var resume = GetResume();
            var summary = resume.Summary;
            Assert.AreEqual("SUMMARY OF INFORMATION SYSTEMS EXPERIENCE", summary);
            resume.Summary = "newSummary";
            Assert.AreEqual("newSummary", resume.Summary);
        }

        [TestMethod]
        public void TestOther()
        {
            var resume = GetResume();
            var other = resume.Other;
            Assert.AreEqual("Other Stuff", other);
            resume.Other = "newOther";
            Assert.AreEqual("newOther", resume.Other);
        }

        [TestMethod]
        public void TestCitizenship()
        {
            var resume = GetResume();
            var citizenship = resume.Citizenship;
            Assert.AreEqual("Australian\r\nNew Zealand", citizenship);
            resume.Citizenship = "newCitizenship";
            Assert.AreEqual("newCitizenship", resume.Citizenship);
        }

        [TestMethod]
        public void TestAffiliations()
        {
            var resume = GetResume();
            var affiliations = resume.Affiliations;
            Assert.AreEqual("CPA\r\nMBA\r\nCPA status with the Australian Society of Certified Practising Accountants", affiliations);
            resume.Affiliations = "newAffiliations";
            Assert.AreEqual("newAffiliations", resume.Affiliations);
        }

        [TestMethod]
        public void TestGetJobs()
        {
            var resume = GetResume();
            var jobs = resume.Jobs;
            Assert.AreEqual(3, jobs.Count);
        }

        [TestMethod]
        public void TestCurrentJob()
        {
            var resume = _parseResumeXmlCommand.ParseResumeXml(CurrentJobLensXml).Resume;

            var job = resume.Jobs[0];
            Assert.AreEqual(new PartialDate(2005, 10), job.Dates.Start);
            Assert.IsNull(job.Dates.End);

            job = resume.Jobs[1];
            Assert.AreEqual(new PartialDate(2005, 2), job.Dates.Start);
            Assert.IsNull(job.Dates.End);

            job = resume.Jobs[2];
            Assert.AreEqual(new PartialDate(2005, 3), job.Dates.Start);
            Assert.AreEqual(new PartialDate(2012, 4), job.Dates.End);
        }

        [TestMethod]
        public void TestGetSchools()
        {
            var resume = GetResume();
            var schools = resume.Schools;
            Assert.AreEqual(1, schools.Count);
        }

        private Resume GetResume()
        {
            using (TextReader tr = new StreamReader(ResumeFile))
            {
                var xml = tr.ReadToEnd();
                return _parseResumeXmlCommand.ParseResumeXml(xml).Resume;
            }
        }
    }
}