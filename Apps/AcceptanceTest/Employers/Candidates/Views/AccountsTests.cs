using System;
using System.Net;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class AccountsTests
        : ViewsTests
    {
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();

        [TestMethod]
        public void TestDisabled()
        {
            var member = CreateMember(0);
            _userAccountsCommand.DisableUserAccount(member, Guid.NewGuid());

            TestCandidateUrls(member);
        }

        [TestMethod]
        public void TestHidden()
        {
            var member = CreateMember(0);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);
            _memberAccountsCommand.UpdateMember(member);

            TestCandidateUrls(member);
        }

        private void TestCandidateUrls(IMember member)
        {
            var candidateUrl = GetCandidateUrl(member, _candidatesCommand.GetCandidate(member.Id));

            Get(HttpStatusCode.NotFound, candidateUrl);
            AssertUrl(candidateUrl);
            AssertPageContains("The candidate cannot be found.");

            var candidatesUrl = GetCandidatesUrl(member.Id);
            Get(HttpStatusCode.NotFound, candidatesUrl);
            AssertUrl(candidatesUrl);
            AssertPageContains("The candidate cannot be found.");
        }
    }
}
