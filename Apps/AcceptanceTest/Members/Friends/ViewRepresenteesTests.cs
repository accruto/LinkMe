using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class ViewRepresenteesTests
        : RepresentativesTests
    {
        private const string NoRepresenteesText = "You are currently not representing anyone.";

        [TestMethod]
        public void TestNoRepresentees()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            GetPage<ViewRepresentees>();

            AssertPageContains(NoRepresenteesText);
        }

        [TestMethod]
        public void TestRepresentee()
        {
            var representative = _memberAccountsCommand.CreateTestMember(0);
            var member = _memberAccountsCommand.CreateTestMember(1);

            _networkingCommand.CreateFirstDegreeLink(member.Id, representative.Id);
            _representativesCommand.CreateRepresentative(member.Id, representative.Id);

            LogIn(representative);
            GetPage<ViewRepresentees>();

            AssertPageDoesNotContain(NoRepresenteesText);
            AssertRepresentees(new[] {member}, new Member[0]);
        }

        [TestMethod]
        public void TestRepresentees()
        {
            var representative = _memberAccountsCommand.CreateTestMember(0);

            // Create some representees.

            var representees = new Member[5];
            for (var index = 0; index < representees.Length; ++index)
            {
                representees[index] = _memberAccountsCommand.CreateTestMember(index + 1);
                representees[index].FirstName = (char)('A' + 2*index) + representees[index].FirstName;
                _memberAccountsCommand.UpdateMember(representees[index]);

                _networkingCommand.CreateFirstDegreeLink(representees[index].Id, representative.Id);
                _representativesCommand.CreateRepresentative(representees[index].Id, representative.Id);
            }

            // Create soem non-representees.

            var nonRepresentees = new Member[3];
            for (var index = 0; index < nonRepresentees.Length; ++index)
            {
                nonRepresentees[index] = _memberAccountsCommand.CreateTestMember(index + representees.Length + 1);
                nonRepresentees[index].FirstName = (char)('A' + 2 * index) + nonRepresentees[index].FirstName;
                _memberAccountsCommand.UpdateMember(nonRepresentees[index]);
            }

            LogIn(representative);
            GetPage<ViewRepresentees>();

            AssertPageDoesNotContain(NoRepresenteesText);
            AssertRepresentees(representees, nonRepresentees);

            // Choose specific letters.

            GetPage<ViewRepresentees>(ViewRepresentees.NameParameter, representees[0].FirstName[0].ToString());
            AssertRepresentees(representees.Take(1), nonRepresentees.Concat(representees.Skip(1)));
        }

        private void AssertRepresentees(IEnumerable<Member> representees, IEnumerable<Member> nonRepresentees)
        {
            foreach (var representee in representees)
                AssertPageContains(representee.FullName);
            foreach (var nonRepresentee in nonRepresentees)
                AssertPageDoesNotContain(nonRepresentee.FullName);
        }
    }
}
