using System;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Management.Test.Communications.Members.Newsletter
{
    [TestClass]
    public abstract class NewsletterTests
        : WebTestClass
    {
        private const string EmailAddress = "homer@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmployerLoginId = "monty";

        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        protected readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        protected readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void NewsletterTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void GetNewsletter(Guid memberId)
        {
            Get(new ReadOnlyApplicationUrl("~/communications/definitions/membernewsletteremail", new ReadOnlyQueryString("userId", memberId.ToString())));
        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(EmailAddress, FirstName, LastName);
        }

        protected Member CreateMember(string emailAddress, string firstName, string lastName)
        {
            return _memberAccountsCommand.CreateTestMember(emailAddress, firstName, lastName);
        }

        protected Member CreateMember(CandidateStatus status, string desiredJobTitle)
        {
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress, FirstName, LastName);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.Status = status;
            if (!string.IsNullOrEmpty(desiredJobTitle))
                candidate.DesiredJobTitle = desiredJobTitle;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        protected Employer CreateEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(EmployerLoginId, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1000 });
            return employer;
        }
    }
}