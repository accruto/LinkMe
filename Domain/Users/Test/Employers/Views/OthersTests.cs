using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    public abstract class OthersTests
        : ViewsTests
    {
        [TestMethod]
        public void TestOthersContact()
        {
            TestOthersContact(Contact);
        }

        [TestMethod]
        public void TestOthersApply()
        {
            // Applicant.

            var employer = CreateEmployer(false, null, true, null);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            var member = CreateMember(0);
            SubmitApplication(member, jobAd);

            // Check who has access.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);

            foreach (var otherEmployer in CreateOtherEmployers(employer, true))
                AssertView(otherEmployer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);
            foreach (var otherEmployer in CreateOtherEmployers(employer, false))
                AssertView(otherEmployer, member, CanContactStatus.YesIfHadCredit, true, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestOthersContactApply()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(true, null, true, null);

            // Contact.

            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));

            // Apply.

            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            SubmitApplication(member, jobAd);

            // Check who has access.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);

            foreach (var otherEmployer in CreateOtherEmployers(employer, true))
                AssertView(otherEmployer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);
            foreach (var otherEmployer in CreateOtherEmployers(employer, false))
                AssertView(otherEmployer, member, CanContactStatus.YesIfHadCredit, true, ProfessionalContactDegree.NotContacted);
        }

        [TestMethod]
        public void TestOthersApplyContact()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(true, null, true, null);

            // Apply.

            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            SubmitApplication(member, jobAd);

            // Contact.

            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));

            // Check who has access.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);

            foreach (var otherEmployer in CreateOtherEmployers(employer, true))
                AssertView(otherEmployer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Applicant);
            foreach (var otherEmployer in CreateOtherEmployers(employer, false))
                AssertView(otherEmployer, member, CanContactStatus.YesIfHadCredit, true, ProfessionalContactDegree.NotContacted);
        }

        protected abstract IEmployer[] CreateOtherEmployers(IEmployer employer, bool canAccess);

        private Guid? Contact(IEmployer employer, Member member)
        {
            return _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));
        }

        private void TestOthersContact(Func<IEmployer, Member, Guid?> contact)
        {
            var member = CreateMember(1);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.PhoneNumbers);
            _membersCommand.UpdateMember(member);
            var employer = CreateEmployer(true, null, false, null);

            // Contact.

            contact(employer, member);

            // Check who has access.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Contacted);

            var otherCanAccess = CreateOtherEmployers(employer, true);
            var otherCannotAccess = CreateOtherEmployers(employer, false);

            foreach (var otherEmployer in otherCanAccess)
                AssertView(otherEmployer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Contacted);
            foreach (var otherEmployer in otherCannotAccess)
                AssertView(otherEmployer, member, CanContactStatus.YesIfHadCredit, true, ProfessionalContactDegree.NotContacted);

            // Contact again.

            var exercisedCreditId = contact(employer, member);
            Assert.IsNull(exercisedCreditId);

            // Check who has access again.

            AssertView(employer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Contacted);

            foreach (var otherEmployer in otherCanAccess)
                AssertView(otherEmployer, member, CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Contacted);
            foreach (var otherEmployer in otherCannotAccess)
                AssertView(otherEmployer, member, CanContactStatus.YesIfHadCredit, true, ProfessionalContactDegree.NotContacted);
        }
    }
}