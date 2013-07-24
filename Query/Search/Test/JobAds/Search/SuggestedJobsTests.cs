using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Search
{
    [TestClass]
    public class SuggestedJobsTests
        : ExecuteJobAdSearchTests
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        private const string JobTitle = "Archeologist";
        private const string Country = "Australia";
        private const string Location = "Melbourne VIC 3000";

        [TestMethod]
        public void TestSuggestedJobAds()
        {
            var jobAd = CreateJobAd();
            var member = CreateMember();

            var execution = _executeJobAdSearchCommand.SearchSuggested(member, null, null);
            Assert.AreEqual(1, execution.Results.JobAdIds.Count);
            Assert.AreEqual(jobAd.Id, execution.Results.JobAdIds[0]);
        }

        private JobAd CreateJobAd()
        {
            var jobPoster = CreateJobPoster();
            return CreateJobAd(
                jobPoster,
                0,
                j =>
                {
                    j.Title = JobTitle;
                    j.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
                });
        }

        private IMember CreateMember()
        {
            var member = _membersCommand.CreateTestMember(0);
            member.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            _membersCommand.UpdateMember(member);

            var candidate = new Candidate { Id = member.Id };
            _candidatesCommand.CreateCandidate(candidate);

            // Give them a resume.

            var resume = new Resume
            {
                Jobs = new List<Job>
                {
                    new Job { Title = JobTitle }
                }
            };

            _candidateResumesCommand.CreateResume(candidate, resume);
            return member;
        }
    }
}
