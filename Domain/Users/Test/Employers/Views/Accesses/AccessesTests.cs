using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Contacts.Commands;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Accesses
{
    [TestClass]
    public abstract class AccessesTests
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
        public void AccessesTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void CreateAllocation(Guid ownerId, int? quantity)
        {
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = quantity, OwnerId = ownerId });
        }

        protected void AssertCheckCanAccessMemberThrow(IEmployer employer, Member member, int expectedAvailable, int expectedRequired)
        {
            try
            {
                CheckCanAccessMember(employer, member);
                Assert.Fail();
            }
            catch (InsufficientCreditsException ex)
            {
                Assert.AreEqual(expectedAvailable, ex.Available);
                Assert.AreEqual(expectedRequired, ex.Required);
            }
        }

        protected void AssertAccessMemberThrow(IEmployer employer, Member member, int expectedAvailable, int expectedRequired)
        {
            try
            {
                AccessMember(employer, member);
                Assert.Fail();
            }
            catch (InsufficientCreditsException ex)
            {
                Assert.AreEqual(expectedAvailable, ex.Available);
                Assert.AreEqual(expectedRequired, ex.Required);
            }
        }

        protected void CheckCanAccessMember(IEmployer employer, Member member)
        {
            _employerMemberViewsCommand.CheckCanAccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
        }

        protected void AccessMember(IEmployer employer, Member member)
        {
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
        }
    }
}
