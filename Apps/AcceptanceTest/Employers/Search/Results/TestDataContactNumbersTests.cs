using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results
{
    [TestClass, Ignore]
    public class TestDataContactNumbersTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
 
        private const string MobileNumber = "0401138976";
        private const string WorkPhone = "0289697775";
        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestContactNumbers()
        {
            var employer0 = CreateEmployer(0, 100);
            CreateEmployer(1, (int?)null);
            for (int i = 0; i < 28; i++)
            {
                var member = CreateMember(i);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                _candidateResumesCommand.AddTestResume(candidate);
                if (i > 14) Contact(employer0, member);
            }
        }

        private Employer CreateEmployer(int index, int? credits)
        {
            var employer = CreateEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = credits, OwnerId = employer.Id });
            return employer;
        }

        private Employer CreateEmployer(int index, IOrganisation organisation)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, organisation);
        }

        private Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            member.PhoneNumbers = new List<PhoneNumber>();
            switch (index % 7)
            {
                case 0:
                    member.PhoneNumbers.Add(new PhoneNumber{ Number = MobileNumber, Type = PhoneNumberType.Mobile });
                    break;
                case 1:
                    member.PhoneNumbers.Add(new PhoneNumber { Number = WorkPhone, Type = PhoneNumberType.Work });
                    break;
                case 2:
                    member.PhoneNumbers.Add(new PhoneNumber { Number = MobileNumber, Type = PhoneNumberType.Mobile });
                    member.PhoneNumbers.Add(new PhoneNumber { Number = WorkPhone, Type = PhoneNumberType.Work });
                    break;
                case 3:
                    member.PhoneNumbers.Add(new PhoneNumber { Number = WorkPhone, Type = PhoneNumberType.Work });
                    member.PhoneNumbers.Add(new PhoneNumber { Number = MobileNumber, Type = PhoneNumberType.Mobile });
                    break;
                case 4:
                    member.PhoneNumbers.Add(new PhoneNumber());
                    member.PhoneNumbers.Add(new PhoneNumber { Number = MobileNumber, Type = PhoneNumberType.Mobile });
                    break;
                case 5:
                    member.PhoneNumbers.Add(new PhoneNumber());
                    member.PhoneNumbers.Add(new PhoneNumber { Number = WorkPhone, Type = PhoneNumberType.Work });
                    break;
                case 6:
                    member.PhoneNumbers.Add(new PhoneNumber());
                    member.PhoneNumbers.Add(new PhoneNumber());
                    break;
            }

            if (index / 7 % 2 == 1)
                member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);

            _memberAccountsCommand.UpdateMember(member);
            return member;
        }

        private void Contact(IEmployer employer, Member member)
        {
            var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.Unlock);
        }
    }
}
