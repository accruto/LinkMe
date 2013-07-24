using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class RemoveJobTests
        : ApiTests
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private ReadOnlyUrl _removeJobUrl;

        private const string CompanyFormat = "Company {0}";
        private const string TitleFormat = "Title {0}";
        private const string DescriptionFormat = "Description {0}";

        [TestInitialize]
        public void TestInitialize()
        {
            _removeJobUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/employmenthistory/removejob");
        }

        [TestMethod]
        public void TestRemoveJob()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var job1 = CreateJob(1);
            var job2 = CreateJob(2);
            var resume = new Resume { Jobs = new List<Job> { job1, job2 } };
            _candidateResumesCommand.CreateResume(candidate, resume);
            LogIn(member);

            // Remove one.

            resume.Jobs = new List<Job> { job2 };
            var parameters = GetParameters(job1);
            AssertJsonSuccess(RemoveJob(parameters));
            AssertMember(member, candidate, resume, true);

            // Remove another.

            resume.Jobs = null;
            parameters = GetParameters(job2);
            AssertJsonSuccess(RemoveJob(parameters));
            AssertMember(member, candidate, resume, true);
        }

        private static NameValueCollection GetParameters(IJob job)
        {
            return new NameValueCollection
            {
                {"Id", job.Id.ToString()},
            };
        }

        private JsonResponseModel RemoveJob(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_removeJobUrl, parameters));
        }

        private static Job CreateJob(int index)
        {
            var now = DateTime.Now;
            return new Job
            {
                Id = Guid.NewGuid(),
                Company = string.Format(CompanyFormat, index),
                Title = string.Format(TitleFormat, index),
                Description = string.Format(DescriptionFormat, index),
                Dates = new PartialDateRange(new PartialDate(now.AddYears(-1 * (index + 2)).Year), new PartialDate(now.AddYears(-1 * (index + 1)).Year)),
            };
        }

        protected override JsonResponseModel Call()
        {
            return RemoveJob(new NameValueCollection());
        }
    }
}
