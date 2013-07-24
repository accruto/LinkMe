using System;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class SetCurrentTests
        : ApiTests
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IResumesRepository _resumesRepository = Resolve<IResumesRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();

        private ReadOnlyUrl _setCurrentUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _setCurrentUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/setcurrent");
        }

        [TestMethod]
        public void TestSetCurrent()
        {
            var today = DateTime.Now.Date;
            var oneWeekAgo = today.AddDays(-7);
            var twoWeeksAgo = today.AddDays(-14);

            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = new Resume { Other = "Other" };
            _candidateResumesCommand.CreateResume(candidate, resume);

            // Set last update times.

            candidate.LastUpdatedTime = oneWeekAgo;
            _candidatesRepository.UpdateCandidate(candidate);

            resume.LastUpdatedTime = twoWeeksAgo;
            _resumesRepository.UpdateResume(resume);

            LogIn(member);

            AssertJsonSuccess(SetCurrent());
            AssertMember(member, candidate, resume, true);

            // Check the times.

            var updatedCandidate = _candidatesQuery.GetCandidate(candidate.Id);
            Assert.AreNotEqual(oneWeekAgo, updatedCandidate.LastUpdatedTime.Date);

            // It would be nice to check down to finer granularity but rounding etc gets in the way.  Just check that it has changed.

            Assert.IsTrue((DateTime.Now - candidate.LastUpdatedTime).TotalMinutes > 1);
            Assert.IsTrue((DateTime.Now - updatedCandidate.LastUpdatedTime).TotalMinutes <= 1);

            var updatedResume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.AreNotEqual(twoWeeksAgo, updatedResume.LastUpdatedTime.Date);

            Assert.IsTrue((DateTime.Now - resume.LastUpdatedTime).TotalMinutes > 1);
            Assert.IsTrue((DateTime.Now - updatedResume.LastUpdatedTime).TotalMinutes <= 1);
        }

        [TestMethod]
        public void TestSetCurrentNoResume()
        {
            var today = DateTime.Now.Date;
            var oneWeekAgo = today.AddDays(-7);

            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Set last update time.

            candidate.LastUpdatedTime = oneWeekAgo;
            _candidatesRepository.UpdateCandidate(candidate);

            LogIn(member);

            AssertJsonSuccess(SetCurrent());
            AssertMember(member, candidate, null, true);

            // Check the times.

            var updatedCandidate = _candidatesQuery.GetCandidate(candidate.Id);
            Assert.AreNotEqual(oneWeekAgo, updatedCandidate.LastUpdatedTime.Date);

            // It would be nice to check down to finer granularity but rounding etc gets in the way.  Just check that it has changed.

            Assert.IsTrue((DateTime.Now - candidate.LastUpdatedTime).TotalMinutes > 1);
            Assert.IsTrue((DateTime.Now - updatedCandidate.LastUpdatedTime).TotalMinutes <= 1);
        }

        private JsonResponseModel SetCurrent()
        {
            return Deserialize<JsonResponseModel>(Post(_setCurrentUrl));
        }

        protected override JsonResponseModel Call()
        {
            return SetCurrent();
        }
    }
}
