using System;
using System.IO;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Search
{
    [TestClass]
    public class HasAppliedTests
        : ExecuteJobAdSearchTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IInternalApplicationsCommand _internalApplicationsCommand = Resolve<IInternalApplicationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private readonly string[] IndustryIds = new[] { "b9826ed4-aeb9-456a-87f7-62e2619127b2", "e7b9bb14-b0e3-4fa1-b055-22751c3f7de2", "728d4d98-c0ca-43bd-b413-a8db6028733e", "99d6200a-b25a-458d-a6ad-f719d5cd1ae7", "5282b3ba-7d63-45a8-8b60-3f1ca7c8881d", "e33f0e21-9842-4f4b-8b19-3981d63676ce", "a5ab1c53-ab29-4c54-9941-c9c83ca8599c", "2e92585c-da9b-40fe-bc90-185451e70abb", "d22fa128-fbd4-4844-8084-587431cc262a", "cd749206-37a1-4ef0-b7f7-e437baa323a3" };
        private const string Country = "Australia";
        private const string Location = "Sydney";

        [TestMethod]
        public void TestApplySearchIgnoreHasApplied()
        {
            TestHasApplied(true, true, null);
        }

        [TestMethod]
        public void TestApplySearchHasApplied()
        {
            TestHasApplied(true, true, true);
        }

        [TestMethod]
        public void TestApplySearchHasNotApplied()
        {
            TestHasApplied(true, true, false);
        }

        [TestMethod]
        public void TestNotApplySearchIgnoreHasApplied()
        {
            TestHasApplied(false, true, null);
        }

        [TestMethod]
        public void TestNotApplySearchHasApplied()
        {
            TestHasApplied(false, true, true);
        }

        [TestMethod]
        public void TestNotApplySearchHasNotApplied()
        {
            TestHasApplied(false, true, false);
        }

        [TestMethod]
        public void TestApplyNotSearchIgnoreHasApplied()
        {
            TestHasApplied(true, false, null);
        }

        [TestMethod]
        public void TestApplyNotSearchHasApplied()
        {
            TestHasApplied(true, false, true);
        }

        [TestMethod]
        public void TestApplyNotSearchHasNotApplied()
        {
            TestHasApplied(true, false, false);
        }

        [TestMethod]
        public void TestNotApplyNotSearchIgnoreHasApplied()
        {
            TestHasApplied(false, false, null);
        }

        [TestMethod]
        public void TestNotApplyNotSearchHasApplied()
        {
            TestHasApplied(false, false, true);
        }

        [TestMethod]
        public void TestNotApplyNotSearchHasNotApplied()
        {
            TestHasApplied(false, false, false);
        }

        private void TestHasApplied(bool apply, bool search, bool? hasApplied)
        {
            var jobPoster = CreateJobPoster();
            var jobAd1 = CreateJobAd(jobPoster, 1);
            var jobAd2 = CreateJobAd(jobPoster, 2);

            var member = CreateMember();
            if (apply)
                _internalApplicationsCommand.SubmitApplication(member, jobAd2, null);

            // Search.

            var criteria = CreateCriteria(hasApplied);
            var execution = _executeJobAdSearchCommand.Search(search ? member : null, criteria, null);

            var expectedJobAdIds = (hasApplied == null || !search)
                ? new[] { jobAd1.Id, jobAd2.Id }
                : hasApplied.Value
                    ? (apply ? new[] { jobAd2.Id } : new Guid[0])
                    : (apply ? new[] { jobAd1.Id } : new[] { jobAd1.Id, jobAd2.Id });

            Assert.AreEqual(expectedJobAdIds.Length, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.JobAdIds.CollectionEqual(expectedJobAdIds));
        }

        private Member CreateMember()
        {
            var member = _membersCommand.CreateTestMember(0);
            var candidate = new Candidate { Id = member.Id };
            _candidatesCommand.CreateCandidate(candidate);

            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();
            FileReference fileReference;
            using (var stream = new MemoryStream(data))
            {
                fileReference = _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }

            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            _candidateResumesCommand.CreateResume(candidate, resume);
            return member;
        }

        private JobAdSearchCriteria CreateCriteria(bool? hasApplied)
        {
            return new JobAdSearchCriteria
            {
                IndustryIds = IndustryIds.Select(i => new Guid(i)).ToArray(),
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location),
                Distance = 50,
                SortCriteria = new JobAdSearchSortCriteria
                {
                    SortOrder = JobAdSortOrder.CreatedTime,
                },
                Recency = new TimeSpan(2592000000000),
                HasApplied = hasApplied,
            };
        }

        private JobAd CreateJobAd(JobPoster jobPoster, int index)
        {
            return CreateJobAd(
                jobPoster,
                index,
                j =>
                {
                    j.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
                    j.Description.Industries = new[] { _industriesQuery.GetIndustry(IndustryIds[0]) };
                });
        }
    }
}
