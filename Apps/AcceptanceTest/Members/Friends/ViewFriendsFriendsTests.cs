using System;
using System.Collections.Generic;
using System.Globalization;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class ViewFriendsFriendsTests
        : WebFormDataTestCase
    {
        private const string CurrentJobsTemplate = "Contact {0} Tester";
        private const string Suburb = "Armadale";
        private const string Postcode = "3143";
        private const string CountrySubdivision = "VIC";
        private static readonly string[] Digits = new[] { "no", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        private HtmlLinkTester _lnkViewFriends;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _lnkViewFriends = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_Content") + "_contactsListControl_rptContacts_ctl00_ucContactsListDetails_lnkViewFriends");
        }

        [TestMethod]
        public void TestDisplay()
        {
            // Create the member and friend.

            var member = CreateMember();
            var friends = CreateFriends(member.Id, 1, 1);

            // Create friends friends.

            var friendsFriends = CreateFriends(friends[0].Id, 2, 1);
            friendsFriends.Add(member);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // View friends friends.

            _lnkViewFriends.Click();
            AssertFriendsFriends(member.Id, friends[0], friendsFriends, true, true);
        }

        [TestMethod]
        public void TestDeactivatedFriendsFriend()
        {
            // Create the member and friend.

            var member = CreateMember();
            var friends = CreateFriends(member.Id, 1, 1);

            // Create friends friends.

            var friendsFriends = CreateFriends(friends[0].Id, 2, 1);
            friendsFriends[0].IsActivated = false;
            _memberAccountsCommand.UpdateMember(friendsFriends[0]);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // View friends friends.

            _lnkViewFriends.Click();
            AssertFriendsFriendsNotVisible(friendsFriends);
        }

        [TestMethod]
        public void TestMultipleDeactivatedFriendsFriends()
        {
            // Create the member and friend.

            var member = CreateMember();
            var friends = CreateFriends(member.Id, 1, 1);

            // Create friends friends.

            var friendsFriends = CreateFriends(friends[0].Id, 2, 1);
            var deactivatedFriendsFriends = CreateFriends(friends[0].Id, 3, 1);
            deactivatedFriendsFriends[0].IsActivated = false;
            _memberAccountsCommand.UpdateMember(deactivatedFriendsFriends[0]);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // View friends friends.

            _lnkViewFriends.Click();
            AssertFriendsFriends(member.Id, friends[0], friendsFriends, true, false);
            AssertFriendsFriendsNotVisible(deactivatedFriendsFriends);
        }

        [TestMethod]
        public void Test5FriendsFriends()
        {
            // Create the member and friend.

            var member = CreateMember();
            var friends = CreateFriends(member.Id, 1, 1);

            // Create friends friends.

            var friendsFriends = CreateFriends(friends[0].Id, 2, 4);
            friendsFriends.Add(member);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // View friends friends.

            _lnkViewFriends.Click();
            AssertFriendsFriends(member.Id, friends[0], friendsFriends, false, true);
        }

        [TestMethod]
        public void Test10FriendsFriends()
        {
            // Create the member and friend.

            var member = CreateMember();
            var friends = CreateFriends(member.Id, 1, 1);

            // Create friends friends.

            var friendsFriends = CreateFriends(friends[0].Id, 2, 9);
            friendsFriends.Add(member);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // View friends friends.

            _lnkViewFriends.Click();
            AssertFriendsFriends(member.Id, friends[0], friendsFriends, false, true);
        }

        [TestMethod]
        public void Test15FriendsFriends()
        {
            // Create the member and friend.

            var member = CreateMember();
            var friends = CreateFriends(member.Id, 1, 1);

            // Create friends friends.

            var friendsFriends = CreateFriends(friends[0].Id, 2, 14);
            friendsFriends.Add(member);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // View friends friends.

            _lnkViewFriends.Click();
            AssertFriendsFriends(member.Id, friends[0], friendsFriends, false, true);
        }

        [TestMethod]
        public void Test20FriendsFriends()
        {
            // Create the member and friend.

            var member = CreateMember();
            var friends = CreateFriends(member.Id, 1, 1);

            // Create friends friends.

            var friendsFriends = CreateFriends(friends[0].Id, 2, 19);
            friendsFriends.Add(member);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // View friends friends.

            _lnkViewFriends.Click();
            AssertFriendsFriends(member.Id, friends[0], friendsFriends, false, true);

            LogOut();
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(100);
        }

        private IList<Member> CreateFriends(Guid memberId, int start, int count)
        {
            var friends = new List<Member>();
            for (var index = start; index < start + count; index++)
            {
                var friend = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesCommand.GetCandidate(friend.Id);

                // Set values.

                candidate.Status = CandidateStatus.OpenToOffers;

                var resume = new Resume
                {
                    Jobs = new List<Job> { new Job { Dates = new PartialDateRange(PartialDate.Parse("01/01/1970")), Title = string.Format(CurrentJobsTemplate, index) } }
                };

                _locationQuery.ResolvePostalSuburb(friend.Address.Location, Australia, Suburb + " " + Postcode + " " + CountrySubdivision);
                friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All;

                _memberAccountsCommand.UpdateMember(friend);
                _candidatesCommand.UpdateCandidate(candidate);
                _candidateResumesCommand.CreateResume(candidate, resume);
                
                // Save.

                friends.Add(friend);

                _networkingCommand.CreateFirstDegreeLink(memberId, friend.Id);
            }

            return friends;
        }

        private void AssertFriendsFriends(Guid memberId, ICommunicationRecipient friend, ICollection<Member> friendsFriends, bool checkAddress, bool checkTotal)
        {
            // Check total first.

            if (checkTotal)
            {
                if (friendsFriends.Count == 1)
                    AssertPageContains(friend.FirstName + " has one friend");
                else if (friendsFriends.Count <= 9)
                    AssertPageContains(friend.FirstName + " has " + Digits[friendsFriends.Count] + " friends");
                else
                    AssertPageContains(friend.FirstName + " has " + friendsFriends.Count + " friends");
            }

            foreach (var friendsFriend in friendsFriends)
            {
                // Check on the main page.

                var candidate = _candidatesCommand.GetCandidate(friendsFriend.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
                if (!AssertFriendsFriend(memberId, friendsFriend, candidate, resume, checkAddress))
                {
                    // Check on the next page.

                    Move(friend.Id, 10);
                    if (!AssertFriendsFriend(memberId, friendsFriend, candidate, resume, checkAddress))
                        Assert.Fail("Cannot find friend " + friendsFriend.FullName + " in the list.");
                    else
                        Move(friend.Id, 0);
                }
            }
        }

        private void AssertFriendsFriendsNotVisible(IEnumerable<Member> friendsFriends)
        {
            foreach (var friendsFriend in friendsFriends)
            {
                var candidate = _candidatesQuery.GetCandidate(friendsFriend.Id);
                var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
                AssertFriendsFriendNotVisible(friendsFriend, resume);
            }
        }

        private bool AssertFriendsFriend(Guid memberId, Member friendsFriend, ICandidate friendsCandidate, IResume friendsResume, bool checkAddress)
        {
            try
            {
                AssertPageContains(friendsFriend.FullName);

                var contactDegree = friendsFriend.Id == memberId ? PersonalContactDegree.Self : PersonalContactDegree.SecondDegree;
                var view = new PersonalView(friendsFriend, contactDegree, contactDegree);

                // Current Jobs.

                if (view.CanAccess(PersonalVisibility.CurrentJobs))
                {
                    var currentJobs = friendsResume.GetCurrentJobsDisplayHtml();
                    if (!string.IsNullOrEmpty(currentJobs))
                        AssertPageContains(currentJobs);
                }
                else
                {
                    var currentJobs = friendsResume.GetCurrentJobsDisplayHtml();
                    if (!string.IsNullOrEmpty(currentJobs))
                        AssertPageDoesNotContain(friendsResume.GetCurrentJobsDisplayHtml());
                }

                // Candidate Status.

                if (view.CanAccess(PersonalVisibility.CandidateStatus))
                {
                    if (friendsCandidate.Status != CandidateStatus.Unspecified)
                        AssertPageContains(NetworkerFacade.GetCandidateStatusText(friendsCandidate.Status));
                }
                else
                {
                    if (friendsCandidate.Status != CandidateStatus.Unspecified)
                        AssertPageDoesNotContain(NetworkerFacade.GetCandidateStatusText(friendsCandidate.Status));
                }

                // Suburb.

                if (checkAddress)
                {
                    if (view.CanAccess(PersonalVisibility.Suburb))
                    {
                        AssertPageContains(friendsFriend.Address.Location.Suburb);
                        AssertPageContains(" " + friendsFriend.Address.Location.Postcode);
                    }
                    else
                    {
                        AssertPageDoesNotContain(friendsFriend.Address.Location.Suburb);
                        AssertPageDoesNotContain(" " + friendsFriend.Address.Location.Postcode);
                    }

                    // CountrySubdivision.

                    if (view.CanAccess(PersonalVisibility.CountrySubdivision))
                        AssertPageContains(friendsFriend.Address.Location.CountrySubdivision.ShortName);
                    else
                        AssertPageDoesNotContain(friendsFriend.Address.Location.CountrySubdivision.ShortName);
                }

                return true;
            }
            catch (AssertFailedException)
            {
                return false;
            }
        }

        private void AssertFriendsFriendNotVisible(ICommunicationRecipient friendsFriend, IResume friendsResume)
        {
            AssertPageDoesNotContain(friendsFriend.FullName);

            // Current Jobs.

            AssertPageDoesNotContain(friendsResume.GetCurrentJobsDisplayHtml());
        }

        private void Move(Guid friendId, int result)
        {
            GetPage<ViewFriendsFriends>("memberId", friendId.ToString("n"), "result", result.ToString(CultureInfo.InvariantCulture));
        }
    }
}