using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates.PhoneNumbers
{
    [TestClass]
    public abstract class PhoneNumberTests
        : CandidateTests
    {
        private ReadOnlyUrl _basePhoneNumbersUrl;

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        [TestInitialize]
        public void PhoneNumberTestsInitialize()
        {
            _basePhoneNumbersUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/candidates/");
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected void Allocate(Guid employerId, int? quantity)
        {
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employerId, InitialQuantity = quantity });
        }

        protected void AccessMember(Employer employer, Member member)
        {
            var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.Unlock);
        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        protected CandidateResponseModel PhoneNumbers(Guid memberId)
        {
            return Deserialize<CandidateResponseModel>(Post(GetPhoneNumbersUrl(memberId)));
        }

        private ReadOnlyUrl GetPhoneNumbersUrl(Guid candidateId)
        {
            return new ReadOnlyApplicationUrl(_basePhoneNumbersUrl, candidateId.ToString().AddUrlSegments("phonenumbers"));
        }
    }
}