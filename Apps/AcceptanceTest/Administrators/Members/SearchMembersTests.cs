using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Members
{
    [TestClass]
    public class SearchMembersTests
        : MembersTests
    {
        private HtmlButtonTester _searchButton;

        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlDropDownListTester _countDropDownList;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _searchButton = new HtmlButtonTester(Browser, "search");

            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _countDropDownList = new HtmlDropDownListTester(Browser, "Count");
        }

        [TestMethod]
        public void TestDefaults()
        {
            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetSearchMembersUrl());

            Assert.IsTrue(_searchButton.IsVisible);
            Assert.IsTrue(_firstNameTextBox.IsVisible);
            Assert.AreEqual(string.Empty, _firstNameTextBox.Text);
            Assert.IsTrue(_lastNameTextBox.IsVisible);
            Assert.AreEqual(string.Empty, _lastNameTextBox.Text);
            Assert.IsTrue(_emailAddressTextBox.IsVisible);
            Assert.AreEqual(string.Empty, _emailAddressTextBox.Text);
            Assert.IsTrue(_countDropDownList.IsVisible);
            Assert.AreEqual(200.ToString(CultureInfo.InvariantCulture), _countDropDownList.SelectedItem.Text);
        }

        [TestMethod]
        public void TestNoSearchCriteria()
        {
            var members = CreateMembers(4);

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetSearchMembersUrl());

            // Search.

            _searchButton.Click();
            AssertMembers(members);
        }

        [TestMethod]
        public void TestSearchCriteria()
        {
            var members = CreateMembers(4);

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetSearchMembersUrl());

            // Search first name.

            _firstNameTextBox.Text = "Something";
            _searchButton.Click();
            AssertNoMembers();
            _firstNameTextBox.Text = members[0].FirstName;
            _searchButton.Click();
            AssertMembers(members.Take(1).ToList());

            // Search last name.

            _firstNameTextBox.Text = string.Empty;
            _lastNameTextBox.Text = "Something";
            _searchButton.Click();
            AssertNoMembers();
            _lastNameTextBox.Text = members[0].LastName;
            _searchButton.Click();
            AssertMembers(members.Take(1).ToList());

            // Search email address.

            _lastNameTextBox.Text = string.Empty;
            _emailAddressTextBox.Text = "Something";
            _searchButton.Click();
            AssertNoMembers();
            _emailAddressTextBox.Text = members[0].GetBestEmailAddress().Address;
            _searchButton.Click();
            AssertMembers(members.Take(1).ToList());
        }

        private IList<Member> CreateMembers(int count)
        {
            var members = new List<Member>();

            for (var index = 0; index < count; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                members.Add(member);
            }

            return members;
        }

        private void AssertMembers(IList<Member> members)
        {
            AssertPageContains(members.Count + " result" + (members.Count == 1 ? "" : "s") + " found.");

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='search-results']/tbody/tr");
            Assert.IsNotNull(nodes);
            Assert.AreEqual(members.Count, nodes.Count);

            for (var index = 0; index < members.Count; ++index)
            {
                var member = members[index];
                var node = nodes[index];

                var a = node.SelectSingleNode("td[position()=1]/a");
                Assert.IsNotNull(a);
                Assert.AreEqual(member.GetBestEmailAddress().Address, a.InnerText);
                var href = GetMemberUrl(member).Path.ToLower();
                Assert.IsNotNull(a.Attributes);
                Assert.AreEqual(href.ToLower(), a.Attributes["href"].Value.ToLower());

                Assert.AreEqual(member.FullName, node.SelectSingleNode("td[position()=2]").InnerText);
                var status = member.IsEnabled ? (member.IsActivated ? "Activated" : "Deactivated") : "Disabled";
                Assert.AreEqual(status, node.SelectSingleNode("td[position()=3]").InnerText);
            }
        }

        private void AssertNoMembers()
        {
            AssertPageContains("No results found.");

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='members_list list']/tbody/tr");
            Assert.AreEqual(0, nodes == null ? 0 : nodes.Count);
        }
    }
}