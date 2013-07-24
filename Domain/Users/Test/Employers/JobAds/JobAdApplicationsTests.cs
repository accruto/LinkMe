using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    [TestClass]
    public class JobAdApplicationsTests
        : JobAdsTests
    {
        private readonly IJobAdApplicationSubmissionsQuery _jobAdApplicationSubmissionsQuery = Resolve<IJobAdApplicationSubmissionsQuery>();

        [TestMethod]
        public void TestGetApplications()
        {
            var employer = CreateEmployer();
            var applicantId = Guid.NewGuid();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            AssertNoApplication(employer, applicantId, jobAd);

            // Create an application.

            var application = new InternalApplication { ApplicantId = applicantId };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            AssertApplication(employer, applicantId, jobAd);
        }

        [TestMethod]
        public void TestGetApplicationsWithCandidateFolder()
        {
            // Create an application.

            var employer = CreateEmployer();
            var applicantId = Guid.NewGuid();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var application = new InternalApplication { ApplicantId = applicantId };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            
            // Create a candidate folder.

            _candidateFoldersCommand.CreatePrivateFolder(employer, new CandidateFolder { Name = "MyFolder" });

            AssertApplication(employer, applicantId, jobAd);
        }

        [TestMethod]
        public void TestGetApplicationsWithDefaultCandidateFolder()
        {
            // Create an application.

            var employer = CreateEmployer();
            var applicantId = Guid.NewGuid();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var application = new InternalApplication { ApplicantId = applicantId };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            // Create the default candidate folder.

            _candidateFoldersQuery.GetShortlistFolder(employer);

            AssertApplication(employer, applicantId, jobAd);
        }

        [TestMethod]
        public void TestDeleteApplication()
        {
            var employer = CreateEmployer();
            var applicantId = Guid.NewGuid();

            // Create an application.

            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var application = new InternalApplication { ApplicantId = applicantId };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd1, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd1, application);

            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            application = new InternalApplication { ApplicantId = applicantId };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd2, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd2, application);

            AssertApplications(employer, applicantId, jobAd1, jobAd2);

            // Delete the first.

            _jobAdApplicationSubmissionsCommand.RevokeApplication(jobAd1, applicantId);
            AssertApplication(employer, applicantId, jobAd2);

            // Delete the second.

            _jobAdApplicationSubmissionsCommand.RevokeApplication(jobAd2, applicantId);
            AssertNoApplication(employer, applicantId, jobAd1, jobAd2);
        }

        [TestMethod]
        public void TestAddApplicant()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var list = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);

            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new Guid[0]);

            // Add an applicant.

            var member1 = _membersCommand.CreateTestMember(1);
            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] {member1.Id});
            AssertCounts(employer, list, 0, 1, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id }, new Guid[0], new Guid[0]);

            // Add them again.

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] {member1.Id});
            AssertCounts(employer, list, 0, 1, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id }, new Guid[0], new Guid[0]);

            // Add another applicant.

            var member2 = _membersCommand.CreateTestMember(2);
            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] {member2.Id});
            AssertCounts(employer, list, 0, 2, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id }, new Guid[0], new Guid[0]);

            // Remove the first.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member1.Id });
            AssertCounts(employer, list, 0, 1, 1);
            AssertStatuses(jobAd, new Guid[0], new[] { member2.Id }, new[] { member1.Id }, new Guid[0]);

            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member1.Id });
            AssertCounts(employer, list, 0, 1, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member2.Id }, new Guid[0], new[] { member1.Id });

            // Remove the second.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member2.Id });
            AssertCounts(employer, list, 0, 0, 1);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new[] { member2.Id }, new[] { member1.Id });
            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member2.Id });
            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new[] { member1.Id, member2.Id });
        }

        [TestMethod]
        public void TestApplicantStatusChange()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var list = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);

            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new Guid[0]);

            // Add an applicant.

            var member = _membersCommand.CreateTestMember(1);
            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] {member.Id});
            AssertCounts(employer, list, 0, 1, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member.Id }, new Guid[0], new Guid[0]);
            AssertNoApplication(employer, member.Id, jobAd);

            // Try to reject.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            AssertCounts(employer, list, 0, 0, 1);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new[] { member.Id }, new Guid[0]);
            AssertNoApplication(employer, member.Id, jobAd);

            // Try to remove.

            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member.Id });
            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new[] { member.Id });
            AssertNoApplication(employer, member.Id, jobAd);

            // Now simulate the applicant applying for the job.

            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            AssertCounts(employer, list, 1, 0, 0);
            AssertStatuses(jobAd, new[] { member.Id }, new Guid[0], new Guid[0], new Guid[0]);
            AssertApplication(employer, member.Id, jobAd);
        }

        [TestMethod]
        public void TestApplicationStatusChange()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var list = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);

            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new Guid[0]);

            var member = _membersCommand.CreateTestMember(1);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            // Ensure initially new.

            AssertCounts(employer, list, 1, 0, 0);
            AssertStatuses(jobAd, new[] { member.Id }, new Guid[0], new Guid[0], new Guid[0]);
            AssertApplication(employer, member.Id, jobAd);

            // Try to shortlist.

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member.Id });
            AssertCounts(employer, list, 0, 1, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member.Id }, new Guid[0], new Guid[0]);
            AssertApplication(employer, member.Id, jobAd);

            // Try to reject.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member.Id });
            AssertCounts(employer, list, 0, 0, 1);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new[] { member.Id }, new Guid[0]);
            AssertApplication(employer, member.Id, jobAd);

            // Try to remove.

            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member.Id });
            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new[] { member.Id });
            AssertApplication(employer, member.Id, jobAd);

            // Attempt to re-shortlist.

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member.Id });
            AssertCounts(employer, list, 0, 1, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member.Id }, new Guid[0], new Guid[0]);
            AssertApplication(employer, member.Id, jobAd);
        }

        [TestMethod]
        public void TestMultipleStatusChanges()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var list = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);

            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new Guid[0]);

            var member1 = _membersCommand.CreateTestMember(1);
            var application = new InternalApplication { ApplicantId = member1.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            var member2 = _membersCommand.CreateTestMember(2);
            application = new InternalApplication { ApplicantId = member2.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            var member3 = _membersCommand.CreateTestMember(3);
            application = new InternalApplication { ApplicantId = member3.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            AssertCounts(employer, list, 3, 0, 0);
            AssertStatuses(jobAd, new[] { member1.Id, member2.Id, member3.Id }, new Guid[0], new Guid[0], new Guid[0]);

            // Should still be three news as can't remove new applicants directly.

            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member1.Id, member3.Id });
            AssertCounts(employer, list, 3, 0, 0);
            AssertStatuses(jobAd, new[] { member1.Id, member2.Id, member3.Id }, new Guid[0], new Guid[0], new Guid[0]);

            // Remove after rejecting.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member1.Id, member3.Id });
            AssertCounts(employer, list, 1, 0, 2);
            AssertStatuses(jobAd, new[] { member2.Id }, new Guid[0], new[] { member1.Id, member3.Id }, new Guid[0]);

            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member1.Id, member3.Id });
            AssertCounts(employer, list, 1, 0, 0);
            AssertStatuses(jobAd, new[] { member2.Id }, new Guid[0], new Guid[0], new[] { member1.Id, member3.Id });

            // Should be able to shortlist them all. New -> Shortlisted and Removed -> Shortlisted are both valid.

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member1.Id, member2.Id, member3.Id });
            AssertCounts(employer, list, 0, 3, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id, member3.Id }, new Guid[0], new Guid[0]);

            // Remove a member; should have no effect.

            var member4 = _membersCommand.CreateTestMember(4);
            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member4.Id });
            AssertCounts(employer, list, 0, 3, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id, member3.Id }, new Guid[0], new Guid[0]);

            // Shortlist, reject then remove applicant.

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member4.Id });
            AssertCounts(employer, list, 0, 4, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id, member3.Id, member4.Id }, new Guid[0], new Guid[0]);

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member4.Id });
            AssertCounts(employer, list, 0, 3, 1);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id, member3.Id }, new[] { member4.Id }, new Guid[0]);

            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member4.Id });
            AssertCounts(employer, list, 0, 3, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id, member3.Id }, new Guid[0], new[] { member4.Id });

            // Now have the candidate apply.

            application = new InternalApplication { ApplicantId = member4.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            AssertCounts(employer, list, 1, 3, 0);
            AssertStatuses(jobAd, new[] { member4.Id }, new[] { member1.Id, member2.Id, member3.Id }, new Guid[0], new Guid[0]);

            // Reject then remove then re-shortlist an applicant.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member2.Id });
            AssertCounts(employer, list, 1, 2, 1);
            AssertStatuses(jobAd, new[] { member4.Id }, new[] { member1.Id, member3.Id }, new[] { member2.Id }, new Guid[0]);

            _jobAdApplicantsCommand.RemoveApplicants(employer, jobAd, new[] { member2.Id });
            AssertCounts(employer, list, 1, 2, 0);
            AssertStatuses(jobAd, new[] { member4.Id }, new[] { member1.Id, member3.Id }, new Guid[0], new[] { member2.Id });

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member2.Id });
            AssertCounts(employer, list, 1, 3, 0);
            AssertStatuses(jobAd, new[] { member4.Id }, new[] { member1.Id, member2.Id, member3.Id }, new Guid[0], new Guid[0]);
        }

        [TestMethod]
        public void TestApplicantStatusUndoChange()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var list = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);

            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new Guid[0]);

            // Add an applicant.

            var member1 = _membersCommand.CreateTestMember(1);
            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member1.Id });
            AssertCounts(employer, list, 0, 1, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id }, new Guid[0], new Guid[0]);

            // Undo the shortlisting.

            _jobAdApplicantsCommand.UndoShortlistApplicants(employer, jobAd, new[] {member1.Id}, null);
            AssertCounts(employer, list, 0, 0, 0);
            AssertStatuses(jobAd, new Guid[0], new Guid[0], new Guid[0], new Guid[0]);

            // Redo a shortlist for another member.

            var member2 = _membersCommand.CreateTestMember(2);
            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { member2.Id, member1.Id });
            AssertCounts(employer, list, 0, 2, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id }, new Guid[0], new Guid[0]);

            // Try to reject.

            _jobAdApplicantsCommand.RejectApplicants(employer, jobAd, new[] { member1.Id });
            AssertCounts(employer, list, 0, 1, 1);
            AssertStatuses(jobAd, new Guid[0], new[] { member2.Id }, new[] { member1.Id }, new Guid[0]);

            // Undo the rejection.

            _jobAdApplicantsCommand.UndoRejectApplicants(employer, jobAd, new[] { member1.Id }, ApplicantStatus.Shortlisted);
            AssertCounts(employer, list, 0, 2, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id }, new Guid[0], new Guid[0]);

            // Now simulate the applicant applying for the job.

            var application = new InternalApplication { ApplicantId = member1.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            AssertCounts(employer, list, 1, 1, 0);
            AssertStatuses(jobAd, new[] { member1.Id }, new[] { member2.Id }, new Guid[0], new Guid[0]);

            // Short list the member who applied.

            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] { application.ApplicantId });
            AssertCounts(employer, list, 0, 2, 0);
            AssertStatuses(jobAd, new Guid[0], new[] { member1.Id, member2.Id }, new Guid[0], new Guid[0]);

            // Undo the most recent shortlist.

            _jobAdApplicantsCommand.UndoShortlistApplicants(employer, jobAd, new[] { member1.Id }, null);
            AssertCounts(employer, list, 1, 1, 0);
            AssertStatuses(jobAd, new[] { member1.Id }, new[] { member2.Id }, new Guid[0], new Guid[0]);
        }

        private void AssertNoApplication(IEmployer employer, Guid applicantId, params IJobAd[] jobAds)
        {
            Assert.AreEqual(0, _jobAdApplicantsQuery.GetApplications(employer, applicantId).Count);

            foreach (var jobAd in jobAds)
                Assert.IsFalse(_jobAdApplicationSubmissionsQuery.HasSubmittedApplication(applicantId, jobAd.Id));

            Assert.AreEqual(0, _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId).Count);
            Assert.AreEqual(0, _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId, from j in jobAds select j.Id).Count);
        }

        private void AssertApplication(IEmployer employer, Guid applicantId, IJobAd jobAd)
        {
            var applications = _jobAdApplicantsQuery.GetApplications(employer, applicantId);
            Assert.AreEqual(1, applications.Count);
            Assert.AreEqual(applicantId, applications[0].ApplicantId);
            Assert.AreEqual(jobAd.Id, applications[0].PositionId);

            Assert.IsTrue(_jobAdApplicationSubmissionsQuery.HasSubmittedApplication(applicantId, jobAd.Id));

            var jobAdIds = _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId);
            Assert.AreEqual(1, jobAdIds.Count);
            Assert.AreEqual(jobAd.Id, jobAdIds[0]);

            jobAdIds = _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId, new[] { jobAd.Id });
            Assert.AreEqual(1, jobAdIds.Count);
            Assert.AreEqual(jobAd.Id, jobAdIds[0]);
        }

        private void AssertApplications(IEmployer employer, Guid applicantId, IJobAd jobAd1, IJobAd jobAd2)
        {
            var applications = _jobAdApplicantsQuery.GetApplications(employer, applicantId);
            Assert.AreEqual(2, applications.Count);
            Assert.IsTrue((from a in applications where a.PositionId == jobAd1.Id && a.ApplicantId == applicantId select a).Any());
            Assert.IsTrue((from a in applications where a.PositionId == jobAd2.Id && a.ApplicantId == applicantId select a).Any());

            Assert.IsTrue(_jobAdApplicationSubmissionsQuery.HasSubmittedApplication(applicantId, jobAd1.Id));
            Assert.IsTrue(_jobAdApplicationSubmissionsQuery.HasSubmittedApplication(applicantId, jobAd2.Id));

            var jobAdIds = _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId);
            Assert.AreEqual(2, jobAdIds.Count);
            Assert.IsTrue((from j in jobAdIds where j == jobAd1.Id select j).Any());
            Assert.IsTrue((from j in jobAdIds where j == jobAd2.Id select j).Any());

            jobAdIds = _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId, new[] { jobAd1.Id });
            Assert.AreEqual(1, jobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, jobAdIds[0]);

            jobAdIds = _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId, new[] { jobAd2.Id });
            Assert.AreEqual(1, jobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, jobAdIds[0]);

            jobAdIds = _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(applicantId, new[] { jobAd1.Id, jobAd2.Id });
            Assert.AreEqual(2, jobAdIds.Count);
            Assert.IsTrue((from j in jobAdIds where j == jobAd1.Id select j).Any());
            Assert.IsTrue((from j in jobAdIds where j == jobAd2.Id select j).Any());
        }

        private void AssertStatuses(IJobAd jobAd, IEnumerable<Guid> newApplicantIds, IEnumerable<Guid> shortlistedApplicantIds, IEnumerable<Guid> rejectedApplicantIds, IList<Guid> removedApplicants)
        {
            Assert.IsTrue(newApplicantIds.CollectionEqual(_jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.New)));
            Assert.IsTrue(shortlistedApplicantIds.CollectionEqual(_jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.Shortlisted)));
            Assert.IsTrue(rejectedApplicantIds.CollectionEqual(_jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.Rejected)));
            Assert.IsTrue(removedApplicants.CollectionEqual(_jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.Removed)));
        }
    }
}