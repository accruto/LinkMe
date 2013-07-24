using System;
using System.Collections.Generic;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Networking;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Utility.Validation;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class FindFriendsTests
        : WebFormDataTestCase
    {
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();

        private const string FriendsListStart = "<div class=\"friends-container\">";
        private const string NameStart = "<span class=\"text-heading\">";
        private const string NameEnd = "</span>";

        private const string EmailFormat = "user{0}@test.linkme.net.au";
        private const string Password = "password";

        private HtmlTextBoxTester _txtQuery;
        private HtmlTextBoxTester _txtEmail;
        private HtmlButtonTester _btnSearch;
        private HtmlLinkTester _lnkAddToFriends0;
        private HtmlLinkTester _lnkAddToFriends1;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _txtQuery = new HtmlTextBoxTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_txtQuery");
            _txtEmail = new HtmlTextBoxTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_txtEmail");

            _btnSearch = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_btnSearch");

            _lnkAddToFriends0 = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_BottomContent") + "_ucResultList_rptContacts_ctl00_ucContactsListDetails_lnkAddToFriends");
            _lnkAddToFriends1 = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_BottomContent") + "_ucResultList_rptContacts_ctl01_ucContactsListDetails_lnkAddToFriends");
        }

        [TestMethod]
        public void TestSearch()
        {
            var member = CreateMember();
            CreateFriends(null, "Barney", "Gumble", 0, 3, true);
            CreateFriends(null, "Lenny", "Leonard", 3, 2, true);

            LogIn(member);
            Browse(null, null);

            // Search.

            PerformSearch("Barney Gumble", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformSearch("Lenny Leonard", "Lenny Leonard", "Lenny Leonard");
        }

        [TestMethod]
        public void TestSearchFriends()
        {
            var member = CreateMember();
            CreateFriends(member, "Barney", "Gumble", 0, 3, false);
            CreateFriends(member, "Lenny", "Leonard", 3, 2, false);

            LogIn(member);
            Browse(null, null);

            // Search.

            PerformSearch("Barney Gumble", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformSearch("Lenny Leonard", "Lenny Leonard", "Lenny Leonard");
        }

        [TestMethod]
        public void TestSearchDeactivatedFriends()
        {
            var member = CreateMember();
            CreateFriends(null, "Barney", "Gumble", 0, 3, true);
            var deactivatedFriend = CreateFriends(null, "Lenny", "Leonard", 3, 1, true)[0];
            deactivatedFriend.IsActivated = false;
            _memberAccountsCommand.UpdateMember(deactivatedFriend);

            LogIn(member);
            Browse(null, null);

            // Deactivated user should not come up in search.

            PerformSearch("Barney Gumble", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformSearch("Lenny Leonard");
        }

        [TestMethod]
        public void TestSearchFriendsFriends()
        {
            var member = CreateMember();
            var friend = CreateFriends(member, "Carl", "Carlson", 0, 1, false)[0];
            CreateFriends(friend, "Barney", "Gumble", 1, 3, false);
            CreateFriends(friend, "Lenny", "Leonard", 4, 2, false);

            LogIn(member);
            Browse(null, null);

            // Search.

            PerformSearch("Barney Gumble", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformSearch("Lenny Leonard", "Lenny Leonard", "Lenny Leonard");
        }

        [TestMethod]
        public void TestSearchDeactivatedFriendsFriends()
        {
            var member = CreateMember();
            var friend = CreateFriends(member, "Carl", "Carlson", 0, 1, false)[0];
            CreateFriends(friend, "Barney", "Gumble", 1, 3, false);
            var deactivatedFriendsFriend = CreateFriends(friend, "Lenny", "Leonard", 4, 1, false)[0];
            deactivatedFriendsFriend.IsActivated = false;
            _memberAccountsCommand.UpdateMember(deactivatedFriendsFriend);

            LogIn(member);
            Browse(null, null);

            // Deactivated user should not come up in search.

            PerformSearch("Barney Gumble", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformSearch("Lenny Leonard");
        }

        [TestMethod]
        public void TestSearchNames()
        {
            var member = CreateMember();
            CreateFriends(null, "Barney", "Gumble", 0, 3, true);
            CreateFriends(null, "Barn", "Gum", 3, 2, true);
            CreateFriends(null, "Space First", "Space Last", 5, 1, true);
            LogIn(member);

            // Full name.

            PerformBrowse("Barney Gumble", "Barney Gumble", "Barney Gumble", "Barney Gumble");

            // Partial first and last name.

            PerformBrowse("barne gumb", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformBrowse("bar gum", "Barn Gum", "Barn Gum", "Barney Gumble", "Barney Gumble", "Barney Gumble");

            // Partial first name.

            PerformBrowse("barne", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformBrowse("bar", "Barn Gum", "Barn Gum", "Barney Gumble", "Barney Gumble", "Barney Gumble");

            // Partial last name.

            PerformBrowse("gumb", "Barney Gumble", "Barney Gumble", "Barney Gumble");
            PerformBrowse("gum", "Barn Gum", "Barn Gum", "Barney Gumble", "Barney Gumble", "Barney Gumble");

            // Some extra symbols.

            PerformBrowse("gumb!");
            AssertErrorMessage(ValidationErrorMessages.INVALID_NAME_SEARCH_CRITERIA + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);

            PerformBrowse("  barne  $%  gumb");
            AssertErrorMessage(ValidationErrorMessages.INVALID_NAME_SEARCH_CRITERIA + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);

            // - is a valid name character, so no matches.

            PerformBrowse("barne-gumb");
            AssertPageDoesNotContain(ValidationErrorMessages.INVALID_NAME_SEARCH_CRITERIA + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);

            // A person with spaces in their first and last name.

            PerformBrowse("Space First Space Last", "Space First Space Last");
            PerformBrowse("Space First Space", "Space First Space Last");
            PerformBrowse("Space First", "Space First Space Last");
            PerformBrowse("Space", "Space First Space Last");
            PerformBrowse("Space Last", "Space First Space Last");
            PerformBrowse("Space Space Last", "Space First Space Last");
            PerformBrowse("Space First Space", "Space First Space Last");
        }

        [TestMethod]
        public void TestExactMatch()
        {
            var member = CreateMember();
            CreateFriends(null, "Barney", "Gumble", 0, 3, true);
            LogIn(member);

            // Search exactly.

            PerformBrowse("Barney Gumble", true, "Barney Gumble", "Barney Gumble", "Barney Gumble");

            // Search with partial names but look for exact match.

            PerformBrowse("bar gum", true);

            // Search with partial names but look for non-exact match.

            PerformBrowse("bar gum", false, "Barney Gumble", "Barney Gumble", "Barney Gumble");
        }

        [TestMethod]
        public void TestSearchForYourself()
        {
            var member = CreateMember();
            LogIn(member);

            // Check that you can search for yourself.

            PerformBrowse(member.FullName, member.FullName);
            AssertPageContains("This is you");
        }

        [TestMethod]
        public void TestPermissions()
        {
            //var searcher = PreLoadTestUserProfiles(false, false, true, false, true, false, false);
            //LogIn(searcher.GetLoginId());

            var member = CreateMember();
            var friend = CreateFriends(member, "Barney", "Gumble", 0, 1, true)[0];
            LogIn(member);

            // Create a member with no link to the searcher and name hidden to public.

            const string emailAddress = "tofind@test.linkme.net.au";
            const string firstName = "II";
            const string lastName = "Am Hid`ing";
            const string fullName = firstName + " " + lastName;

            var toFind = _memberAccountsCommand.CreateTestMember(emailAddress, firstName, lastName);

            // Hidden.

            PerformBrowse(fullName);

            // Enable public access to their name.

            toFind.VisibilitySettings.Personal.PublicVisibility |= PersonalVisibility.Name;
            _memberAccountsCommand.UpdateMember(toFind);

            PerformBrowse(fullName, fullName);

            // Can't be found by "Am" as substring match (<3 characters).

            PerformBrowse("Am");

            // But can be found by first name only.

            PerformBrowse(firstName, fullName);

            // Disable public access to their name, but add them as a friend's friend.

            toFind.VisibilitySettings.Personal.PublicVisibility &= ~PersonalVisibility.Name;
            _memberAccountsCommand.UpdateMember(toFind);
            _networkingCommand.CreateFirstDegreeLink(friend.Id, toFind.Id);

            PerformBrowse(fullName, fullName);

            // Disable second-degree access to their name.

            toFind.VisibilitySettings.Personal.SecondDegreeVisibility &= ~PersonalVisibility.Name;
            _memberAccountsCommand.UpdateMember(toFind);

            PerformBrowse(fullName);

            // Same thing with an invite - the hidden user sends an invite to the searcher, which grants
            // the searher temporary friend access. Initially make the invite sent over 28 days ago - no access.

            var invitation = new NetworkingInvitation { InviterId = toFind.Id, InviteeId = member.Id };
            _networkingInvitationsCommand.CreateInvitation(invitation);

            invitation.Status = RequestStatus.Pending;
            invitation.LastSentTime = DateTime.Now.AddDays(28 * -1).AddSeconds(-1);
            _networkingInvitationsCommand.UpdateInvitation(invitation);

            PerformBrowse(fullName);

            // Now make the invite sent 27 days ago and the user should be found.

            invitation.LastSentTime = DateTime.Now.AddDays(28 * -1 + 1);
            _networkingInvitationsCommand.UpdateInvitation(invitation);

            PerformBrowse(fullName, fullName);

            // Decline the invitation - the user should be hidden again.

            invitation.Status = RequestStatus.Declined;
            _networkingInvitationsCommand.UpdateInvitation(invitation);

            PerformBrowse(fullName);

            // Add to searcher's friends.

            _networkingCommand.CreateFirstDegreeLink(member.Id, toFind.Id);

            PerformBrowse(fullName, fullName);

            // Disable first-degree access to their name (users can't do this in practice).

            toFind.VisibilitySettings.Personal.FirstDegreeVisibility &= ~PersonalVisibility.Name;
            _memberAccountsCommand.UpdateMember(toFind);

            PerformBrowse(fullName);
        }

        [TestMethod]
        public void TestSearchErrors()
        {
            var member = CreateMember();
            LogIn(member);

            // Search with no criteria.

            Browse("", "");
            _btnSearch.Click();
            AssertPage<FindFriends>();
            Assert.AreEqual("", _txtQuery.Text);

            // Search with non-searchable characters.

            _txtQuery.Text = " $ %^ !";
            _btnSearch.Click();
            AssertPage<FindFriends>();
            Assert.AreEqual(" $ %^ !", _txtQuery.Text);
            AssertErrorMessage(ValidationErrorMessages.INVALID_NAME_SEARCH_CRITERIA + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);
        }

        [TestMethod]
        public void TestSearchQuery()
        {
            // Test MiniFindFriends queries where the textbox can contain either a name or email, which is determined by the presence of the @ symbol

            const string nameSearchTerm = "Bart";
            var emailSearchTerm = string.Format(EmailFormat, "0");

            var member = CreateMember();
            CreateFriends(member, "Bart", "Simpson", 0, 2, true);

            LogIn(member);

            GetPage<FindFriends>(FindFriends.GenericQueryParameter, nameSearchTerm);
            AssertPage<FindFriends>();
            AssertResults("Bart Simpson", "Bart Simpson");
            Assert.AreEqual(nameSearchTerm, _txtQuery.Text);

            GetPage<FindFriends>(FindFriends.GenericQueryParameter, emailSearchTerm);
            AssertPage<FindFriends>();
            AssertResults("Bart Simpson");
            Assert.AreEqual(emailSearchTerm, _txtEmail.Text);
        }

        [TestMethod]
        public void TestSearchQueryErrors()
        {
            var member = CreateMember();
            LogIn(member);

            // Use empty query parameter.

            Browse("", "");
            AssertPage<FindFriends>();
            Assert.AreEqual("", _txtQuery.Text);
            Assert.AreEqual("", _txtEmail.Text);
            AssertErrorMessage(string.Format(ValidationErrorMessages.REQUIRED_FIELD_MISSING_1, "search term"));

            // Use non-searchable characters in query string.

            Browse(" $ %^ !", null);
            AssertPage<FindFriends>();
            Assert.AreEqual(" $ %^ !", _txtQuery.Text);
            Assert.AreEqual("", _txtEmail.Text);
            AssertErrorMessage(ValidationErrorMessages.INVALID_NAME_SEARCH_CRITERIA + " " + ValidationErrorMessages.PLEASE_TRY_AGAIN);

            Browse(null, " $ %^ !");
            AssertPage<FindFriends>();
            Assert.AreEqual("", _txtQuery.Text);
            Assert.AreEqual(" $ %^ !", _txtEmail.Text);
            AssertErrorMessage("The email address must be valid and have less than 320 characters. " + ValidationErrorMessages.PLEASE_TRY_AGAIN);

            Browse(" $ %^ !", " $ %^ !");
            AssertPage<FindFriends>();
            Assert.AreEqual(" $ %^ !", _txtEmail.Text);
            Assert.AreEqual(" $ %^ !", _txtQuery.Text);
            AssertErrorMessage("Please enter a name or email address, not both");
        }

        [TestMethod]
        public void TestAddToFriendsLink()
        {
            var member = CreateMember();
            LogIn(member);

            // Create someone already a member and someone who is not.

            CreateFriends(null, "Barney", "Gumble", 0, 1, true);
            CreateFriends(member, "Barn", "Gum", 1, 1, false);

            Browse("bar gum", null);
            Assert.AreEqual("bar gum", _txtQuery.Text);
            AssertResults("Barn Gum", "Barney Gumble");

            // Check that there is no link for the friend but one for the non-friend.

            Assert.IsFalse(_lnkAddToFriends0.IsVisible);
            Assert.IsTrue(_lnkAddToFriends1.IsVisible);
        }

        [TestMethod]
        public void TestSearchByEmail()
        {
            var member = CreateMember();
            var firstDegreeFriend = _memberAccountsCommand.CreateTestMember(1);
            firstDegreeFriend.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.EmailAddress);
            _memberAccountsCommand.UpdateMember(firstDegreeFriend);

            _networkingCommand.CreateFirstDegreeLink(member.Id, firstDegreeFriend.Id);

            // First Degree

            LogIn(member);

            SearchEmailAddressAndAssertResults(firstDegreeFriend);

            // Second Degree
            var secondDegreeFriend = _memberAccountsCommand.CreateTestMember(2);
            secondDegreeFriend.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(secondDegreeFriend);
            _networkingCommand.CreateFirstDegreeLink(firstDegreeFriend.Id, secondDegreeFriend.Id);

            SearchEmailAddressAndAssertResults(secondDegreeFriend);

            // Remove access to name so member should not be found, as per spec - feature #3071
            secondDegreeFriend.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(secondDegreeFriend);

            _txtEmail.Text = secondDegreeFriend.GetBestEmailAddress().Address;
            _btnSearch.Click();
            AssertNoResults();

            // Public Access
            var randomPerson = _memberAccountsCommand.CreateTestMember(3);
            randomPerson.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.EmailAddress);
            randomPerson.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(randomPerson);

            SearchEmailAddressAndAssertResults(randomPerson);

            randomPerson.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(randomPerson);

            _txtEmail.Text = randomPerson.GetBestEmailAddress().Address;
            _btnSearch.Click();
            AssertNoResults();
        }

        private void PerformSearch(string query, params string[] expectedResults)
        {
            // Search.

            _txtQuery.Text = query;
            _btnSearch.Click();

            // Assert.

            Assert.AreEqual(query, _txtQuery.Text);
            if (expectedResults.Length > 0)
            {
                AssertPageContains(query + "</a>");
                AssertResults(expectedResults);
            }
            else
            {
                AssertNoResults();
                AssertPageDoesNotContain(query + "</a>");
            }
        }

        private void PerformBrowse(string nameQuery, params string[] expectedResults)
        {
            Browse(nameQuery, null);
            Assert.AreEqual(nameQuery, _txtQuery.Text);

            if (expectedResults.Length > 0)
                AssertResults(expectedResults);
            else
                AssertNoResults();
        }

        private void PerformBrowse(string nameQuery, bool exactMatch, params string[] expectedResults)
        {
            Browse(nameQuery, exactMatch);
            Assert.AreEqual(nameQuery, _txtQuery.Text);

            if (expectedResults.Length > 0)
                AssertResults(expectedResults);
            else
                AssertNoResults();
        }

        private void SearchEmailAddressAndAssertResults(ICommunicationRecipient member)
        {
            Browse(null, null);
            AssertPage<FindFriends>();

            var query = member.EmailAddress;
            _txtEmail.Text = query;
            _btnSearch.Click();

            var expectedResults = new string[1];
            expectedResults[0] = member.FullName;
            AssertResults(expectedResults);
        }

        private Member CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(1000);
            member.VisibilitySettings.Personal.PublicVisibility |= PersonalVisibility.Name;
            _memberAccountsCommand.UpdateMember(member);
            return member;
        }

        private IList<Member> CreateFriends(IHasId<Guid> member, string firstNameFormat, string lastNameFormat, int startIndex, int count, bool isNamePublic)
        {
            var friends = new List<Member>();

            for (var index = startIndex; index < startIndex + count; ++index)
            {
                var friend = _memberAccountsCommand.CreateTestMember(string.Format(EmailFormat, index), Password, string.Format(firstNameFormat, index), string.Format(lastNameFormat, index));
                if (isNamePublic)
                    friend.VisibilitySettings.Personal.PublicVisibility |= PersonalVisibility.Name;
                _memberAccountsCommand.UpdateMember(friend);

                if (member != null)
                {
                    _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
                    _networkingCommand.CreateFirstDegreeLink(friend.Id, member.Id);
                }

                friends.Add(friend);
            }

            return friends;
        }

        private void Browse(string nameQuery, string emailQuery)
        {
            GetPage<FindFriends>(FindFriends.NameQueryParameter, nameQuery, FindFriends.EmailQueryParameter, emailQuery);
            AssertPage<FindFriends>();
        }

        private void Browse(string nameQuery, bool exactMatch)
        {
            GetPage<FindFriends>(FindFriends.NameQueryParameter, nameQuery, FindFriends.ExactMatchParameter, exactMatch.ToString());
            AssertPage<FindFriends>();
        }

        private void AssertNoResults()
        {
            AssertPage<FindFriends>();
            //            AssertPageContains("No matches were found for your search criteria.");
            AssertPageDoesNotContain(FriendsListStart);
        }

        private void AssertResults(params string[] expectedNames)
        {
            Assert.IsTrue(expectedNames.Length > 0, "expectedNames.Length > 0");

            AssertPage<FindFriends>();

            var pageText = Browser.CurrentPageText;
            var startIndex = pageText.IndexOf("Your search results");

            for (var i = 0; i < expectedNames.Length; i++)
            {
                var actualName = GetActualName(pageText, ref startIndex);
                if (actualName == null)
                {
                    Assert.Fail("Failed to find result " + i + " in the page (looks like it has " + i + " results when " + expectedNames.Length + " were expected)." + SaveCurrentPageToFile());
                }

                Assert.AreEqual(expectedNames[i], actualName, "Result " + i + " does not match.");
            }

            var unexpectedName = GetActualName(pageText, ref startIndex);
            if (unexpectedName != null)
            {
                Assert.Fail("The page contains more than the expected " + expectedNames.Length + " results." + " The first unexpected name was '" + unexpectedName + "'. " + SaveCurrentPageToFile());
            }
        }

        private static string GetActualName(string pageText, ref int startIndex)
        {
            startIndex = pageText.IndexOf(NameStart, startIndex);
            if (startIndex == -1)
                return null;

            startIndex += NameStart.Length;

            var endIndex = pageText.IndexOf(NameEnd, startIndex);
            Assert.IsTrue(endIndex != -1);

            var nameLink = pageText.Substring(startIndex, endIndex - startIndex);

            // Move past the anchor.

            var nameStartIndex = nameLink.IndexOf(">");
            if (nameStartIndex == -1)
                return null;
            ++nameStartIndex;
            var nameEndIndex = nameLink.IndexOf("<", nameStartIndex);
            return nameLink.Substring(nameStartIndex, nameEndIndex - nameStartIndex).Trim();
        }
    }
}