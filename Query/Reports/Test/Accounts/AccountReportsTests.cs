using System;
using System.Collections.Generic;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Query.Reports.Accounts.Queries;

namespace LinkMe.Query.Reports.Test.Accounts
{
    public abstract class AccountReportsTests
        : TestClass
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();
        protected readonly IUserAccountsRepository _userAccountsRepository = Resolve<IUserAccountsRepository>();

        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress = "homer@test.linkme.net.au";
        private const string Country = "Australia";

        protected Member CreateMember(DateTime createdTime)
        {
            var member = new Member
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddresses = new List<EmailAddress> {new EmailAddress {Address = EmailAddress}},
                CreatedTime = createdTime,
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), null) },
                IsEnabled = true,
                IsActivated = false,
            };

            _membersCommand.CreateMember(member);
            return member;
        }
    }
}