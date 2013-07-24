using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class HasViewedTests
        : CriteriaTests
    {
        private ReadOnlyUrl _candidatesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _candidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates");
        }

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.HasViewed = null;
            TestDisplay(criteria);
            criteria.HasViewed = false;
            TestDisplay(criteria);
            criteria.HasViewed = true;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestHasViewed()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            Get(GetSearchUrl());
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();
            AssertMembers(members);

            // Have not viewed anyone yet.

            TestHasViewed(new Member[0], members);

            // View them now.

            View(members[1].Id);
            TestHasViewed(new[] { members[1] }, new[] { members[0], members[2], members[3] });

            // Only the first one is viewed.

            View(members[3].Id, members[2].Id);
            TestHasViewed(new[] { members[1], members[3] }, new[] { members[0], members[2] });

            View(members[2].Id, members[3].Id);
            TestHasViewed(new[] { members[1], members[2], members[3] }, new[] { members[0] });

            View(members[0].Id);
            TestHasViewed(new[] { members[0], members[1], members[2], members[3] }, new Member[0]);
        }

        private void View(params Guid[] memberIds)
        {
            var url = _candidatesUrl.AsNonReadOnly();
            if (memberIds.Length > 0)
                url.QueryString.Add("currentCandidateId", memberIds[0].ToString());
            foreach (var memberId in memberIds)
                url.QueryString.Add("candidateId", memberId.ToString());
            Get(url);
        }

        private void TestHasViewed(IEnumerable<Member> hasViewed, IEnumerable<Member> hasNotViewed)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.HasViewed = null;
            Get(GetSearchUrl(criteria));
            AssertMembers(hasViewed.Concat(hasNotViewed).OrderBy(m => m.FirstName).ToArray());

            criteria.HasViewed = false;
            Get(GetSearchUrl(criteria));
            AssertMembers(hasNotViewed.OrderBy(m => m.FirstName).ToArray());

            criteria.HasViewed = true;
            Get(GetSearchUrl(criteria));
            AssertMembers(hasViewed.OrderBy(m => m.FirstName).ToArray());
        }
    }
}