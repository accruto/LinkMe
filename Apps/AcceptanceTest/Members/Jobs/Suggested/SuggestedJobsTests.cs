using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Suggested
{
    [TestClass]
    public abstract class SuggestedJobsTests
        : JobsTests
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private const string Title = "Archeologist";
        private const string Content = "This is the content";
        private const string Country = "Australia";
        private const string Location = "Melbourne VIC 3000";

        [TestMethod]
        public void TestNoSuggestedJobs()
        {
            var member = CreateMember(0);
            LogIn(member);

            // Get the page itself.

            var url = GetSuggestedJobsUrl();
            Get(url);
            AssertUrl(url);

            AssertNoJobs();
        }

        [TestMethod]
        public void TestSuggestedJobs()
        {
            var employer = CreateEmployer();
            var jobAd = CreateSuggestedJobAd(employer.Id, DateTime.Now);

            var member = CreateMember();
            LogIn(member);

            var url = GetSuggestedJobsUrl();
            Get(url);
            AssertUrl(url);

            AssertJobAds(GetIds(GetSuggestedJobAdNodes()), jobAd.Id);
        }

        [TestMethod]
        public void TestJobSuggestedJobs()
        {
            var employer = CreateEmployer();
            var jobAd = CreateSuggestedJobAd(employer.Id, DateTime.Now);

            // The other job needs to be closed.

            var otherJob = CreateJobAd(employer);
            _jobAdsCommand.CloseJobAd(otherJob);

            var member = CreateMember();
            LogIn(member);

            var url = GetJobAdUrl(otherJob.Id);
            Get(url);

            AssertJobAds(GetIds(GetJobSuggestedJobAdNodes()), jobAd.Id);
        }

        [TestMethod]
        public void TestNewSuggestedJob()
        {
            var employer = CreateEmployer();
            CreateSuggestedJobAd(employer.Id, DateTime.Now.AddDays(-1));

            // The other job needs to be closed.

            var otherJob = CreateJobAd(employer);
            _jobAdsCommand.CloseJobAd(otherJob);

            var member = CreateMember();
            LogIn(member);

            var url = GetJobAdUrl(otherJob.Id);
            Get(url);

            var nodes = GetJobSuggestedJobAdNodes();
            Assert.AreEqual(1, nodes.Count);
            AssertIsNew(true, nodes[0]);
        }

        [TestMethod]
        public void TestNotNewSuggestedJob()
        {
            var employer = CreateEmployer();
            CreateSuggestedJobAd(employer.Id, DateTime.Now.AddDays(-5));

            // The other job needs to be closed.

            var otherJob = CreateJobAd(employer);
            _jobAdsCommand.CloseJobAd(otherJob);

            var member = CreateMember();
            LogIn(member);

            var url = GetJobAdUrl(otherJob.Id);
            Get(url);

            var nodes = GetJobSuggestedJobAdNodes();
            Assert.AreEqual(1, nodes.Count);
            AssertIsNew(false, nodes[0]);
        }

        protected abstract void AssertIsNew(bool expectedIsNew, HtmlNode node);
        protected abstract IList<HtmlNode> GetJobSuggestedJobAdNodes();
        protected abstract IList<HtmlNode> GetSuggestedJobAdNodes();
        protected abstract void AssertNoJobs();

        private static IEnumerable<Guid> GetIds(IEnumerable<HtmlNode> nodes)
        {
            return from j in nodes
                   select new Guid(j.Attributes["id"].Value);
        }

        private static void AssertJobAds(IEnumerable<Guid> jobAdIds, params Guid[] expectedJobAdIds)
        {
            Assert.IsTrue(jobAdIds.CollectionEqual(expectedJobAdIds));
        }

        protected static ReadOnlyUrl GetSuggestedJobsUrl()
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/suggested");
        }

        protected static ReadOnlyUrl GetPartialSuggestedJobsUrl()
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/suggested/partial");
        }

        protected Member CreateMember()
        {
            var member = CreateMember(0);
            member.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            _membersCommand.UpdateMember(member);

            // Give them a resume.

            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = new Resume
            {
                Jobs = new List<Job>
                {
                    new Job { Title = Title }
                }
            };

            _candidateResumesCommand.CreateResume(candidate, resume);
            return member;
        }

        protected JobAd CreateSuggestedJobAd(Guid employerId, DateTime createdTime)
        {
            var jobAd = new JobAd
            {
                CreatedTime = createdTime,
                PosterId = employerId,
                Title = Title,
                Description = new JobAdDescription
                {
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location),
                    Content = Content
                },
            };

            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }
    }
}
