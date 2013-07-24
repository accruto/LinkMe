using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using LinkMe.Web.Areas.Employers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers
{
    [TestClass]
    public abstract class CandidateListsTests
        : WebTestClass
    {
        protected IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        protected ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        protected IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();

        private HtmlDropDownListTester _sortOrderDropDownList;
        private HtmlRadioButtonTester _sortOrderIsAscendingRadioButton;
        private HtmlRadioButtonTester _sortOrderIsDescendingRadioButton;

        private ReadOnlyUrl _baseBlockListsUrl;

        [TestInitialize]
        public void CandidateListsTestsInitialize()
        {
            _sortOrderDropDownList = new HtmlDropDownListTester(Browser, "SortOrder");
            _sortOrderIsAscendingRadioButton = new HtmlRadioButtonTester(Browser, "SortOrderIsAscending");
            _sortOrderIsDescendingRadioButton = new HtmlRadioButtonTester(Browser, "SortOrderIsDescending");

            _baseBlockListsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/blocklists/");
        }

        protected ReadOnlyUrl GetBlockListUrl(BlockListType blockListType)
        {
            return new ReadOnlyApplicationUrl(_baseBlockListsUrl, blockListType.ToString());
        }

        protected static ReadOnlyUrl Get(ReadOnlyUrl baseUrl, MemberSortOrder sortOrder, bool isAscending)
        {
            var url = baseUrl.AsNonReadOnly();
            url.QueryString.Add("SortOrder", sortOrder.ToString());
            url.QueryString.Add("SortOrderDirection", isAscending ? "SortOrderIsAscending" : "SortOrderIsDescending");
            return url;
        }

        protected static ReadOnlyUrl Get(ReadOnlyUrl baseUrl, DetailLevel detailLevel)
        {
            var url = baseUrl.AsNonReadOnly();
            url.QueryString.Add("DetailLevel", detailLevel.ToString());
            return url;
        }

        protected static ReadOnlyUrl Get(ReadOnlyUrl baseUrl, int? page, int? items)
        {
            var queryString = new QueryString();
            if (page != null)
                queryString.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));
            if (items != null)
                queryString.Add("items", items.Value.ToString(CultureInfo.InvariantCulture));

            var url = baseUrl.AsNonReadOnly();
            url.QueryString.Add(queryString);
            return url;
        }

        protected void AssertMembers(params Member[] members)
        {
            AssertMembers(false, members);
        }

        protected void AssertMembers(bool assertOrder, params Member[] members)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='expanded_search-results']/div[position()=1]");
            Assert.AreEqual(members.Length, nodes == null ? 0 : nodes.Count);

            if (nodes != null)
            {
                if (assertOrder)
                {
                    for (var index = 0; index < members.Length; ++index)
                        Assert.AreEqual(members[index].Id.ToString(), nodes[index].Attributes["data-memberid"].Value);
                }
                else
                {
                    foreach (var node in nodes)
                    {
                        var value = node.Attributes["data-memberid"].Value;
                        Assert.IsTrue((from m in members where m.Id.ToString() == value select m).Any());
                    }
                }
            }
        }

        protected void AssertCandidate(Member member)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='resume-header']/div");
            Assert.IsNotNull(node);
            Assert.AreEqual(member.Id.ToString(), node.Attributes["data-memberid"].Value);
        }

        protected virtual Employer CreateEmployer()
        {
            return CreateEmployer(0);
        }

        protected virtual Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        protected virtual Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected void AssertSortOrders(MemberSortOrder[] sortOrders, MemberSortOrder selectedValue, bool isAscending)
        {
            Assert.AreEqual(sortOrders.Length, _sortOrderDropDownList.Items.Count);
            for (var index = 0; index < sortOrders.Length; ++index)
            {
                var sortOrder = sortOrders[index];
                var item = _sortOrderDropDownList.Items[index];

                Assert.AreEqual(GetText(sortOrder), item.Text);
                Assert.AreEqual(sortOrder.ToString(), item.Value);
                Assert.AreEqual(sortOrder == selectedValue, item.IsSelected);
            }

            if (isAscending)
            {
                Assert.IsTrue(_sortOrderIsAscendingRadioButton.IsChecked);
                Assert.IsFalse(_sortOrderIsDescendingRadioButton.IsChecked);
            }
            else
            {
                Assert.IsFalse(_sortOrderIsAscendingRadioButton.IsChecked);
                Assert.IsTrue(_sortOrderIsDescendingRadioButton.IsChecked);
            }
        }

        private static string GetText(MemberSortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case MemberSortOrder.DateUpdated:
                    return "Date updated";

                case MemberSortOrder.FirstName:
                    return "First name";

                default:
                    return sortOrder.ToString();
            }
        }
    }
}