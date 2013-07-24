using System;
using System.Collections.Generic;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class MemberViewingTests
        : TestClass
    {
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void MemberViewingTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNotViewMember()
        {
            var member = CreateMember(1);
            var employer = CreateEmployer(1);

            AssertViewings(employer, new IMember[0], new[]{member});
        }

        [TestMethod]
        public void TestViewMember()
        {
            var member = CreateMember(1);
            var employer = CreateEmployer(1);

            _employerMemberViewsCommand.ViewMember(_app, employer, member);
            AssertViewings(employer, new[] { member }, new IMember[0]);
        }

        [TestMethod]
        public void TestMultipleViewMember()
        {
            var member = CreateMember(1);
            var employer = CreateEmployer(1);

            _employerMemberViewsCommand.ViewMember(_app, employer, member);
            _employerMemberViewsCommand.ViewMember(_app, employer, member);
            _employerMemberViewsCommand.ViewMember(_app, employer, member);
            AssertViewings(employer, new[] { member }, new IMember[0]);
        }

        [TestMethod]
        public void TestMultipleMembers()
        {
            const int count = 5;
            var members = new IMember[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            var employer = CreateEmployer(1);
            _employerMemberViewsCommand.ViewMember(_app, employer, members[1]);
            _employerMemberViewsCommand.ViewMember(_app, employer, members[1]);
            _employerMemberViewsCommand.ViewMember(_app, employer, members[2]);
            _employerMemberViewsCommand.ViewMember(_app, employer, members[3]);
            _employerMemberViewsCommand.ViewMember(_app, employer, members[3]);

            AssertViewings(employer, new[] { members[1], members[2], members[3] }, new[] { members[0], members[4] });
        }

        private Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        private IEmployer CreateEmployer(int index)
        {
            return _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        private void AssertViewings(IEmployer employer, ICollection<IMember> expectedViewedMembers, IEnumerable<IMember> expectedNotViewedMembers)
        {
            foreach (var viewedMember in expectedViewedMembers)
                Assert.IsTrue(_employerMemberViewsQuery.HasViewedMember(employer, viewedMember.Id));
            foreach (var notViewedMember in expectedNotViewedMembers)
                Assert.IsFalse(_employerMemberViewsQuery.HasViewedMember(employer, notViewedMember.Id));
            
            var viewedMembers = _employerMemberViewsQuery.GetViewedMemberIds(employer);
            Assert.AreEqual(expectedViewedMembers.Count, viewedMembers.Count);
            foreach (var expectedViewedMember in expectedViewedMembers)
                Assert.IsTrue(viewedMembers.Contains(expectedViewedMember.Id));
            foreach (var expectedNotViewedMember in expectedNotViewedMembers)
                Assert.IsFalse(viewedMembers.Contains(expectedNotViewedMember.Id));
        }
    }
}