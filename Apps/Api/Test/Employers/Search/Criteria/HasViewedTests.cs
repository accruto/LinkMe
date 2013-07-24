using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Criteria
{
    [TestClass]
    public class HasViewedTests
        : SearchTests
    {
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestMethod]
        public void TestHasViewed()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Have not viewed anyone yet.

            TestHasViewed(new Member[0], members);

            // View them now.

            _employerMemberViewsCommand.ViewMember(_app, employer, members[1]);
            TestHasViewed(new[] { members[1] }, new[] { members[0], members[2], members[3] });

            _employerMemberViewsCommand.ViewMember(_app, employer, members[3]);
            TestHasViewed(new[] { members[1], members[3] }, new[] { members[0], members[2] });

            _employerMemberViewsCommand.ViewMember(_app, employer, members[2]);
            TestHasViewed(new[] { members[1], members[2], members[3] }, new[] { members[0] });

            _employerMemberViewsCommand.ViewMember(_app, employer, members[0]);
            TestHasViewed(new[] { members[0], members[1], members[2], members[3] }, new Member[0]);
        }

        private void TestHasViewed(IEnumerable<Member> hasViewed, IEnumerable<Member> hasNotViewed)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.HasViewed = null;
            var model = Search(criteria);
            AssertMembers(model, hasViewed.Concat(hasNotViewed).OrderBy(m => m.FirstName).ToArray());

            criteria.HasViewed = false;
            model = Search(criteria);
            AssertMembers(model, hasNotViewed.OrderBy(m => m.FirstName).ToArray());

            criteria.HasViewed = true;
            model = Search(criteria);
            AssertMembers(model, hasViewed.OrderBy(m => m.FirstName).ToArray());
        }
    }
}