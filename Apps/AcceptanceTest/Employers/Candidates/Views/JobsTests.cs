using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class JobsTests
        : ViewsTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly IParseResumeXmlCommand _parseResumeXmlCommand = Resolve<IParseResumeXmlCommand>();
        private static readonly string DownloadResumeFile = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\TestResume5.xml", RuntimeEnvironment.GetSourceFolder());

        [TestMethod]
        public void TestInvalidDownloadResume()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            var xml = ReadFile(DownloadResumeFile);
            var resume = _parseResumeXmlCommand.ParseResumeXml(xml).Resume;
            resume.LastUpdatedTime = DateTime.Now;
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.CreateResume(candidate, resume);

            var employer = CreateEmployer(0);
            LogIn(employer);

            // Check that all job titles and employers are visible.

            TestCandidateUrls(member, () => AssertJobs(resume.Jobs));
        }

        private void AssertJobs(ICollection<Job> expectedJobs)
        {
            var jobs = GetJobs();
            Assert.AreEqual(expectedJobs.Count, jobs.Count);

            foreach (var expectedJob in expectedJobs)
            {
                var expectedTitle = expectedJob.Title;
                var expectedCompany = expectedJob.Company;
                var expectedDates = expectedJob.Dates;

                var job = (from j in jobs
                           where j.Title == expectedTitle && j.Company == expectedCompany && Equals(j.Dates, expectedDates)
                           select j).SingleOrDefault();

                if (job == null)
                    Assert.Fail("Cannot find job.");

                jobs.Remove(job);
            }

            Assert.AreEqual(0, jobs.Count);
        }

        private IList<Job> GetJobs()
        {
            var jobs = new List<Job>();
            Job job = null;

            var jobsNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='Employment-details']//table[@class='jobs']");
            foreach (var xmlJobsTr in jobsNode.SelectNodes("tr"))
            {
                var dateRangeNode = xmlJobsTr.SelectSingleNode("td/span[@class='date-range']");
                if (dateRangeNode != null)
                {
                    if (job == null)
                        job = new Job();
                    var startDate = dateRangeNode.SelectSingleNode("span[@class='start-date']").InnerText;
                    var endDate = dateRangeNode.SelectSingleNode("span[@class='end-date']").InnerText;
                    job.Dates = GetDateTimeRange(startDate, endDate);
                    continue;
                }

                var titleNode = xmlJobsTr.SelectSingleNode("td/span[@class='title']");
                if (titleNode != null)
                {
                    if (job == null)
                        job = new Job();
                    job.Title = HttpUtility.HtmlDecode(titleNode.SelectSingleNode("a").InnerText.Trim());
                    if (job.Title == "Job title not specified")
                        job.Title = null;
                    continue;
                }

                var companyNode = xmlJobsTr.SelectSingleNode("td/span[@class='company']");
                if (companyNode != null)
                {
                    if (job == null)
                        job = new Job();
                    job.Company = HttpUtility.HtmlDecode(companyNode.InnerText.Trim());
                    if (job.Company == "<Employer hidden>")
                        job.Company = null;

                    jobs.Add(job);
                    job = null;
                    continue;
                }
            }

            return jobs;
        }

        private static PartialDateRange GetDateTimeRange(string startDate, string endDate)
        {
            var start = string.IsNullOrEmpty(startDate)
                ? null 
                : startDate == "N/A"
                    ? (PartialDate?)null
                    : PartialDate.Parse(startDate);

            if (string.IsNullOrEmpty(endDate) || endDate == "N/A")
                return null;

            if (endDate == "Current")
            {
                return start == null
                    ? new PartialDateRange()
                    : new PartialDateRange(start.Value);
            }

            return new PartialDateRange(start, PartialDate.Parse(endDate));
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