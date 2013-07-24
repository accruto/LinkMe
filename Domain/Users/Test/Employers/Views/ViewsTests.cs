using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Contacts.Commands;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public abstract class ViewsTests
        : TestClass
    {
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IEmployerCreditsCommand _employerCreditsCommand = Resolve<IEmployerCreditsCommand>();
        protected readonly IEmployerCreditsQuery _employerCreditsQuery = Resolve<IEmployerCreditsQuery>();
        protected readonly IRecruitersQuery _recruitersQuery = Resolve<IRecruitersQuery>();
        protected readonly IJobAdApplicantsQuery _jobAdApplicantsQuery = Resolve<IJobAdApplicantsQuery>();
        protected readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        protected readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        protected readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        protected readonly IEmployerMemberContactsCommand _employerMemberContactsCommand = Resolve<IEmployerMemberContactsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        protected readonly IExercisedCreditsQuery _exercisedCreditsQuery = Resolve<IExercisedCreditsQuery>();

        protected readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void ViewsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Member CreateMember(int index)
        {
            var member = _membersCommand.CreateTestMember(index);
            _candidatesCommand.CreateCandidate(new Candidate { Id = member.Id });
            return member;
        }

        protected IEmployer CreateEmployer(bool allocateContactCredits, int? contactQuantity, bool allocateApplicantCredits, int? applicantQuantity)
        {
            var employer = CreateEmployer();
            if (allocateContactCredits)
                AllocateCredits<ContactCredit>(employer, contactQuantity);
            if (allocateApplicantCredits)
                AllocateCredits<ApplicantCredit>(employer, applicantQuantity);
            return employer;
        }

        protected virtual IEmployer CreateEmployer()
        {
            return null;
        }

        protected virtual void AllocateCredits<T>(IEmployer employer, int? quantity) where T : Credit
        {
        }

        protected void SubmitApplication(IMember member, JobAdEntry jobAd)
        {
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
        }

        protected void AssertView(IEmployer employer, Member member, CanContactStatus canContact, bool phoneVisible, ProfessionalContactDegree contactDegree)
        {
            if (employer == null)
            {
                canContact = CanContactStatus.YesIfHadCredit;
                contactDegree = ProfessionalContactDegree.NotContacted;
            }

            var canContactByPhone = phoneVisible ? canContact : CanContactStatus.No;

            Assert.AreEqual(canContact, _employerMemberViewsQuery.CanContact(employer, member));

            var view = _employerMemberViewsQuery.GetProfessionalView(employer, member.Id);
            Assert.AreEqual(canContact, view.CanContact());
            Assert.AreEqual(canContactByPhone, view.CanContactByPhone());
            Assert.AreEqual(contactDegree, view.EffectiveContactDegree);
            Assert.AreEqual(canContact, _employerMemberViewsQuery.GetProfessionalView(employer, member.Id).CanContact());
            Assert.AreEqual(canContact, _employerMemberViewsQuery.GetProfessionalView(employer, member).CanContact());
            Assert.AreEqual(canContact, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member.Id })[member.Id].CanContact());

            Assert.AreEqual(canContact, _employerMemberViewsQuery.GetEmployerMemberView(employer, member.Id).CanContact());
            Assert.AreEqual(canContact, _employerMemberViewsQuery.GetEmployerMemberViews(employer, new[] { member.Id })[member.Id].CanContact());

            Assert.AreEqual(contactDegree, _employerMemberViewsQuery.GetProfessionalView(employer, member.Id).EffectiveContactDegree);
            Assert.AreEqual(contactDegree, _employerMemberViewsQuery.GetProfessionalView(employer, member).EffectiveContactDegree);
            Assert.AreEqual(contactDegree, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member.Id })[member.Id].EffectiveContactDegree);

            view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            Assert.AreEqual(canContact, view.CanContact());
            Assert.AreEqual(canContactByPhone, view.CanContactByPhone());
            Assert.AreEqual(contactDegree, view.EffectiveContactDegree);

            view = _employerMemberViewsQuery.GetProfessionalViews(employer, new[] {member.Id})[member.Id];
            Assert.AreEqual(canContact, view.CanContact());
            Assert.AreEqual(canContactByPhone, view.CanContactByPhone());
            Assert.AreEqual(contactDegree, view.EffectiveContactDegree);

            view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member.Id);
            Assert.AreEqual(canContact, view.CanContact());
            Assert.AreEqual(canContactByPhone, view.CanContactByPhone());
            Assert.AreEqual(contactDegree, view.EffectiveContactDegree);

            view = _employerMemberViewsQuery.GetEmployerMemberViews(employer, new[] {member.Id})[member.Id];
            Assert.AreEqual(canContact, view.CanContact());
            Assert.AreEqual(canContactByPhone, view.CanContactByPhone());
            Assert.AreEqual(contactDegree, view.EffectiveContactDegree);
        }
    }
}