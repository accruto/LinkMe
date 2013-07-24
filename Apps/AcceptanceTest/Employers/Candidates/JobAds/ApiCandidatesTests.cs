using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds
{
    [TestClass]
    public class ApiCandidatesTests
        : ApiJobAdsTests
    {
        [TestMethod]
        public void TestShortlistCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateCandidate(index);

            // Log in and shortlist candidates.

            LogIn(employer);
            AssertModel(0, 0, 0, JobAds());

            // Shortlist.

            AssertModel(0, 1, 0, ShortlistCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, members.Take(1).ToArray(), ApplicantStatus.Shortlisted);

            AssertModel(0, 2, 0, ShortlistCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, members.Take(2).ToArray(), ApplicantStatus.Shortlisted);

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, members, ApplicantStatus.Shortlisted);

            // Shortlist all again.

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, members, ApplicantStatus.Shortlisted);
        }

        [TestMethod]
        public void TestShortlistNewCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateNewCandidate(jobAd, index);

            // Log in and shortlist candidates.

            LogIn(employer);
            AssertModel(4, 0, 0, JobAds());

            // Shortlist.

            AssertModel(3, 1, 0, ShortlistCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, members.Skip(1).ToArray(), members.Take(1).ToArray(), new Member[0], new Member[0]);

            AssertModel(2, 2, 0, ShortlistCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, members.Skip(2).ToArray(), members.Take(2).ToArray(), new Member[0], new Member[0]);

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);

            // Shortlist all again.

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestShortlistRejectedCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateRejectedCandidate(employer, jobAd, index);

            // Log in and shortlist candidates.

            LogIn(employer);
            AssertModel(0, 0, 4, JobAds());

            // Shortlist.

            AssertModel(0, 1, 3, ShortlistCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, new Member[0], members.Take(1).ToArray(), members.Skip(1).ToArray(), new Member[0]);

            AssertModel(0, 2, 2, ShortlistCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, new Member[0], members.Take(2).ToArray(), members.Skip(2).ToArray(), new Member[0]);

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);

            // Shortlist all again.

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestShortlistRemovedCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateRemovedCandidate(employer, jobAd, index);

            // Log in and shortlist candidates.

            LogIn(employer);
            AssertModel(0, 0, 0, JobAds());

            // Shortlist.

            AssertModel(0, 1, 0, ShortlistCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, new Member[0], members.Take(1).ToArray(), new Member[0], members.Skip(1).ToArray());

            AssertModel(0, 2, 0, ShortlistCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, new Member[0], members.Take(2).ToArray(), new Member[0], members.Skip(2).ToArray());

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);

            // Shortlist all again.

            AssertModel(0, 4, 0, ShortlistCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestRejectNewCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateNewCandidate(jobAd, index);

            // Log in and shortlist candidates.

            LogIn(employer);
            AssertModel(4, 0, 0, JobAds());

            // Reject.

            AssertModel(3, 0, 1, RejectCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, members.Skip(1).ToArray(), new Member[0], members.Take(1).ToArray(), new Member[0]);

            AssertModel(2, 0, 2, RejectCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, members.Skip(2).ToArray(), new Member[0], members.Take(2).ToArray(), new Member[0]);

            AssertModel(0, 0, 4, RejectCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], new Member[0], members, new Member[0]);

            // Reject all again.

            AssertModel(0, 0, 4, RejectCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], new Member[0], members, new Member[0]);
        }

        [TestMethod]
        public void TestRejectShortlistedCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateShortlistedCandidate(employer, jobAd, index);

            // Log in and shortlist candidates.

            LogIn(employer);
            AssertModel(0, 4, 0, JobAds());

            // Reject.

            AssertModel(0, 3, 1, RejectCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, new Member[0], members.Skip(1).ToArray(), members.Take(1).ToArray(), new Member[0]);

            AssertModel(0, 2, 2, RejectCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, new Member[0], members.Skip(2).ToArray(), members.Take(2).ToArray(), new Member[0]);

            AssertModel(0, 0, 4, RejectCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], new Member[0], members, new Member[0]);

            // Shortlist all again.

            AssertModel(0, 0, 4, RejectCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], new Member[0], members, new Member[0]);
        }

        [TestMethod]
        public void TestCannotRejectRemovedCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateRemovedCandidate(employer, jobAd, index);

            // Log in and shortlist candidates.

            LogIn(employer);
            AssertModel(0, 0, 0, JobAds());

            // Reject.

            AssertModel(0, 0, 0, RejectCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, new Member[0], new Member[0], new Member[0], members);

            AssertModel(0, 0, 0, RejectCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, new Member[0], new Member[0], new Member[0], members);

            AssertModel(0, 0, 0, RejectCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], new Member[0], new Member[0], members);

            // Reject all again.

            AssertModel(0, 0, 0, RejectCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], new Member[0], new Member[0], members);
        }

        [TestMethod]
        public void TestCannotRemoveNewCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateNewCandidate(jobAd, index);

            // Log in and remove candidates.

            LogIn(employer);
            AssertModel(4, 0, 0, JobAds());

            // Remove.

            AssertModel(4, 0, 0, RemoveCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, members, new Member[0], new Member[0], new Member[0]);

            AssertModel(4, 0, 0, RemoveCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, members, new Member[0], new Member[0], new Member[0]);

            AssertModel(4, 0, 0, RemoveCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, members, new Member[0], new Member[0], new Member[0]);

            // Remove all again.

            AssertModel(4, 0, 0, RemoveCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, members, new Member[0], new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestCannotRemoveShortlistedCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateShortlistedCandidate(employer, jobAd, index);

            // Log in and remove candidates.

            LogIn(employer);
            AssertModel(0, 4, 0, JobAds());

            // Remove.

            AssertModel(0, 4, 0, RemoveCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);

            AssertModel(0, 4, 0, RemoveCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);

            AssertModel(0, 4, 0, RemoveCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);

            // Remove all again.

            AssertModel(0, 4, 0, RemoveCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], members, new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestRemoveRejectedCandidates()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Create members and apply for job.

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateRejectedCandidate(employer, jobAd, index);

            // Log in and remove candidates.

            LogIn(employer);
            AssertModel(0, 0, 4, JobAds());

            // Remove.

            AssertModel(0, 0, 3, RemoveCandidates(jobAd.Id, members[0]));
            AssertCandidates(jobAd, new Member[0], new Member[0], members.Skip(1).ToArray(), members.Take(1).ToArray());

            AssertModel(0, 0, 2, RemoveCandidates(jobAd.Id, members[1]));
            AssertCandidates(jobAd, new Member[0], new Member[0], members.Skip(2).ToArray(), members.Take(2).ToArray());

            AssertModel(0, 0, 0, RemoveCandidates(jobAd.Id, members[2], members[3]));
            AssertCandidates(jobAd, new Member[0], new Member[0], new Member[0], members);

            // Reject all again.

            AssertModel(0, 0, 0, RemoveCandidates(jobAd.Id, members));
            AssertCandidates(jobAd, new Member[0], new Member[0], new Member[0], members);
        }

        [TestMethod]
        public void TestUndoShortlistNewCandidate()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = CreateNewCandidate(jobAd, 0);

            // Log in and shortlist candidate.

            LogIn(employer);
            AssertModel(1, 0, 0, JobAds());

            // Shortlist.

            AssertModel(0, 1, 0, ShortlistCandidates(jobAd.Id, member));
            AssertCandidates(jobAd, new Member[0], new[] { member }, new Member[0], new Member[0]);

            // Undo shortlist.

            AssertModel(1, 0, 0, UndoShortlistCandidates(jobAd.Id, ApplicantStatus.New, member));
            AssertCandidates(jobAd, new[] { member }, new Member[0], new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestUndoRejectNewCandidate()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = CreateNewCandidate(jobAd, 0);

            // Log in and shortlist candidate.

            LogIn(employer);
            AssertModel(1, 0, 0, JobAds());

            // Shortlist.

            AssertModel(0, 0, 1, RejectCandidates(jobAd.Id, member));
            AssertCandidates(jobAd, new Member[0], new Member[0], new[] { member }, new Member[0]);

            // Undo reject.

            AssertModel(1, 0, 0, UndoRejectCandidates(jobAd.Id, ApplicantStatus.New, member));
            AssertCandidates(jobAd, new[] { member }, new Member[0], new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestUndoRejectShortlistedCandidate()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = CreateShortlistedCandidate(employer, jobAd, 0);

            // Log in and shortlist candidate.

            LogIn(employer);
            AssertModel(0, 1, 0, JobAds());

            // Reject.

            AssertModel(0, 0, 1, RejectCandidates(jobAd.Id, member));
            AssertCandidates(jobAd, new Member[0], new Member[0], new[] { member }, new Member[0]);

            // Undo reject.

            AssertModel(0, 1, 0, UndoRejectCandidates(jobAd.Id, ApplicantStatus.Shortlisted, member));
            AssertCandidates(jobAd, new Member[0], new[] { member }, new Member[0], new Member[0]);
        }

        [TestMethod]
        public void TestUndoShortlistRejectedCandidate()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = CreateRejectedCandidate(employer, jobAd, 0);

            // Log in and shortlist candidate.

            LogIn(employer);
            AssertModel(0, 0, 1, JobAds());

            // Shortlist.

            AssertModel(0, 1, 0, ShortlistCandidates(jobAd.Id, member));
            AssertCandidates(jobAd, new Member[0], new[] { member }, new Member[0], new Member[0]);

            // Undo shortlist.

            AssertModel(0, 0, 1, UndoShortlistCandidates(jobAd.Id, ApplicantStatus.Rejected, member));
            AssertCandidates(jobAd, new Member[0], new Member[0], new[] { member }, new Member[0]);
        }

        [TestMethod]
        public void TestUndoRemoveRejectedCandidate()
        {
            // Create employer and job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = CreateRejectedCandidate(employer, jobAd, 0);

            // Log in and shortlist candidate.

            LogIn(employer);
            AssertModel(0, 0, 1, JobAds());

            // Remove.

            AssertModel(0, 0, 0, RemoveCandidates(jobAd.Id, member));
            AssertCandidates(jobAd, new Member[0], new Member[0], new Member[0], new[] { member });

            // Undo remove.

            AssertModel(0, 0, 1, UndoRemoveCandidates(jobAd.Id, member));
            AssertCandidates(jobAd, new Member[0], new Member[0], new[] { member }, new Member[0]);
        }

        private JsonJobAdModel ShortlistCandidates(Guid jobAdId, params Member[] members)
        {
            return CallApi("shortlistcandidates", jobAdId, members, null);
        }

        private JsonJobAdModel UndoShortlistCandidates(Guid jobAdId, ApplicantStatus previousStatus, params Member[] members)
        {
            return CallApi("undoshortlistcandidates", jobAdId, members, previousStatus);
        }

        private JsonJobAdModel RejectCandidates(Guid jobAdId, params Member[] members)
        {
            return CallApi("rejectcandidates", jobAdId, members, null);
        }

        private JsonJobAdModel UndoRejectCandidates(Guid jobAdId, ApplicantStatus previousStatus, params Member[] members)
        {
            return CallApi("undorejectcandidates", jobAdId, members, previousStatus);
        }

        private JsonJobAdModel RemoveCandidates(Guid jobAdId, params Member[] members)
        {
            return CallApi("removecandidates", jobAdId, members, null);
        }

        private JsonJobAdModel UndoRemoveCandidates(Guid jobAdId, params Member[] members)
        {
            return CallApi("undoremovecandidates", jobAdId, members, null);
        }

        private JsonJobAdModel CallApi(string api, Guid jobAdId, IEnumerable<Member> members, ApplicantStatus? previousStatus)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }
            if (previousStatus != null)
                parameters.Add("previousStatus", previousStatus.Value.ToString());

            var url = new ReadOnlyApplicationUrl(_baseJobAdsUrl, jobAdId + "/" + api);
            var response = Post(url, parameters);
            return new JavaScriptSerializer().Deserialize<JsonJobAdModel>(response);
        }

        private static void AssertModel(int expectedNewCount, int expectedShortlistedCount, int expectedRejectedCount, JsonJobAdModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedNewCount, model.JobAd.ApplicantCounts.New);
            Assert.AreEqual(expectedShortlistedCount, model.JobAd.ApplicantCounts.ShortListed);
            Assert.AreEqual(expectedRejectedCount, model.JobAd.ApplicantCounts.Rejected);
        }

        private Member CreateCandidate(int index)
        {
            return CreateMember(index);
        }

        private Member CreateNewCandidate(JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            return member;
        }

        private Member CreateShortlistedCandidate(IEmployer employer, JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            _jobAdApplicationsCommand.ShortlistApplicants(employer, jobAd, new[] { member.Id });
            return member;
        }

        private Member CreateRejectedCandidate(IEmployer employer, JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            _jobAdApplicationsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            return member;
        }

        private Member CreateRemovedCandidate(IEmployer employer, JobAdEntry jobAd, int index)
        {
            var member = CreateMember(index);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            _jobAdApplicationsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            _jobAdApplicationsCommand.RemoveApplicants(employer, jobAd, new[] { member.Id });
            return member;
        }
    }
}