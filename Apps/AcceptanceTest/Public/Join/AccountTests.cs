using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public class AccountTests
        : JoinTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        [TestMethod]
        public void TestDuplicateLoginId()
        {
            // Create an existing member with the email address.

            _memberAccountsCommand.CreateTestMember(EmailAddress);

            // Try to join with that same email address.

            Get(GetJoinUrl());
            SubmitJoin();
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            AssertUrl(GetPersonalDetailsUrl(instanceId));
            AssertErrorMessage("The username is already being used.");
        }
    }
}
