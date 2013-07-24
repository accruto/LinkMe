using System;
using System.Collections.Generic;
using System.Web;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Utility.Validation;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class ViewFriendsTests
        : WebFormDataTestCase
    {
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();

        private const string CurrentJobsTemplate = "Contact Tester {0}";
        private const string Suburb = "Armadale";
        private const string Postcode = "3143";
        private const string CountrySubdivision = "VIC";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestDisplay()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
            AssertPageDoesNotContain("Represented by me");
            AssertPageDoesNotContain("My nominated representative");
        }

        [TestMethod]
        public void TestNoFriends()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            var friends = new List<Member>();
            var candidates = new List<Candidate>();
            var resumes = new List<Resume>();

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestDeactivatedFriend()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;

            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            friends[0].IsActivated = false;
            _memberAccountsCommand.UpdateMember(friends[0]);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriendsNotVisible(friends, resumes);
        }

        [TestMethod]
        public void TestDisplayMultipleFriends()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;

            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 5, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestMultipleDeactivatedFriends()
        {
            // Create the member.

            var member = CreateMember(0);

            // Create activated and deactivated friends.

            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;

            IList<Member> activatedFriends;
            IList<Candidate> activatedCandidates;
            IList<Resume> activatedResumes;
            CreateFriends(member.Id, access, 1, 2, out activatedFriends, out activatedCandidates, out activatedResumes);

            IList<Member> deactivatedFriends;
            IList<Candidate> deactivatedCandidates;
            IList<Resume> deactivatedResumes;
            CreateFriends(member.Id, access, 3, 2, out deactivatedFriends, out deactivatedCandidates, out deactivatedResumes);

            deactivatedFriends[0].IsActivated = false;
            _memberAccountsCommand.UpdateMember(deactivatedFriends[0]);
            deactivatedFriends[1].IsActivated = false;
            _memberAccountsCommand.UpdateMember(deactivatedFriends[1]);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(activatedFriends, activatedCandidates, activatedResumes, false);
            AssertFriendsNotVisible(deactivatedFriends, deactivatedResumes);
        }

        [TestMethod]
        public void TestNameNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;

            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // If the name is not visible then they do not show up in the list.

            friends = new List<Member>();
            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestRepresentative()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;

            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);
            _representativesCommand.CreateRepresentative(member.Id, friends[0].Id);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
            AssertPageDoesNotContain("Represented by me");
            AssertPageContains("My nominated representative");
        }

        [TestMethod]
        public void TestRepresentativeNameNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);
            _representativesCommand.CreateRepresentative(member.Id, friends[0].Id);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // Because they are a representative then they show up in the list even though their name is not visible.

            AssertFriends(friends, candidates, resumes);
            AssertPageDoesNotContain("Represented by me");
            AssertPageContains("My nominated representative");
        }

        [TestMethod]
        public void TestRepresentee()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);
            _representativesCommand.CreateRepresentative(friends[0].Id, member.Id);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
            AssertPageContains("Represented by me");
            AssertPageDoesNotContain("My nominated representative");
        }

        [TestMethod]
        public void TestRepresenteeNameNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);
            _representativesCommand.CreateRepresentative(friends[0].Id, member.Id);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            // Because they are a representee then they show up in the list even though their name is not visible.

            AssertFriends(friends, candidates, resumes);
            AssertPageContains("Represented by me");
            AssertPageDoesNotContain("My nominated representative");
        }

        [TestMethod]
        public void TestCurrentJobsNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestCandidateStatus()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.All;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 4, out friends, out candidates, out resumes);

            candidates[0].Status = CandidateStatus.ActivelyLooking;
            _candidatesCommand.UpdateCandidate(candidates[0]);
            candidates[1].Status = CandidateStatus.NotLooking;
            _candidatesCommand.UpdateCandidate(candidates[1]);
            candidates[2].Status = CandidateStatus.OpenToOffers;
            _candidatesCommand.UpdateCandidate(candidates[2]);
            candidates[3].Status = CandidateStatus.Unspecified;
            _candidatesCommand.UpdateCandidate(candidates[3]);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestCandidateStatusNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestSuburbNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.CountrySubdivision | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestCountrySubdivisionNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.Interests;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestInterestsNotVisible()
        {
            // Create the member and friends.

            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.CurrentJobs | PersonalVisibility.CandidateStatus | PersonalVisibility.Suburb | PersonalVisibility.CountrySubdivision;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertFriends(friends, candidates, resumes);
        }

        [TestMethod]
        public void TestFriendLinks()
        {
            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.SendMessages | PersonalVisibility.FriendsList | PersonalVisibility.SendInvites;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertLinks(friends);
        }

        [TestMethod]
        public void TestSendMessagesNotVisible()
        {
            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.FriendsList | PersonalVisibility.SendInvites;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertLinks(friends);
        }

        [TestMethod]
        public void TestFriendsListNotVisible()
        {
            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.SendMessages | PersonalVisibility.SendInvites;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            AssertLinks(friends);
        }

        [TestMethod]
        public void TestFullNameLink()
        {
            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.SendMessages | PersonalVisibility.FriendsList | PersonalVisibility.SendInvites;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            FollowFullNameLink(friends, 0);
        }

        [TestMethod]
        public void TestFullProfileLink()
        {
            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.SendMessages | PersonalVisibility.FriendsList | PersonalVisibility.SendInvites;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            FollowFullProfileLink(friends, 0);
        }

        [TestMethod]
        public void TestViewFriendsLink()
        {
            var member = CreateMember(0);
            const PersonalVisibility access = PersonalVisibility.Name | PersonalVisibility.SendMessages | PersonalVisibility.FriendsList | PersonalVisibility.SendInvites;
            IList<Member> friends;
            IList<Candidate> candidates;
            IList<Resume> resumes;
            CreateFriends(member.Id, access, 1, 1, out friends, out candidates, out resumes);

            // Log in.

            LogIn(member);
            GetPage<ViewFriends>();

            FollowViewFriendsLink(friends, 0);
        }

        [TestMethod]
        public void TestViewUnrelatedNetworker()
        {
            var firstMember = CreateMember(0);
            var firstMemberId = firstMember.Id.ToString("N");

            var secondMember = CreateMember(1);
            LogIn(secondMember);

            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, firstMemberId);
            AssertPageContains(HttpUtility.HtmlEncode(ValidationErrorMessages.ATTEMPTED_TO_VIEW_NETWORKER_WITHOUT_ACCESS_TO_NAME));
        }

        private Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        private void CreateFriends(Guid memberId, PersonalVisibility firstDegree, int start, int count, out IList<Member> friends, out IList<Candidate> candidates, out IList<Resume> resumes)
        {
            friends = new List<Member>();
            candidates = new List<Candidate>();
            resumes = new List<Resume>();

            for (var index = start; index < start + count; index++)
            {
                var friend = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesCommand.GetCandidate(friend.Id);

                // Set values.

                candidate.Status = CandidateStatus.OpenToOffers;

                var resume = new Resume
                {
                    Jobs = new List<Job> { new Job {Dates = new PartialDateRange(PartialDate.Parse("01/01/1970")), Title = string.Format(CurrentJobsTemplate, index) } }
                };

                _locationQuery.ResolvePostalSuburb(friend.Address.Location, Australia, Suburb + " " + Postcode + " " + CountrySubdivision);
                friend.VisibilitySettings.Personal.FirstDegreeVisibility = firstDegree;

                // Save.

                _memberAccountsCommand.UpdateMember(friend);
                _candidatesCommand.UpdateCandidate(candidate);
                _candidateResumesCommand.CreateResume(candidate, resume);

                friends.Add(friend);
                candidates.Add(candidate);
                resumes.Add(resume);

                _networkingCommand.CreateFirstDegreeLink(memberId, friend.Id);
            }
        }

        private void AssertFriends(IList<Member> friends, IList<Candidate> candidates, IList<Resume> resumes, bool checkCount = true)
        {
            if (checkCount)
            {
                // This should really always be checked.  There is a bug at the moment
                // though where the count is not updated for deactivated users.
                // This should be fixed and this "checkCount" should be removed.

                switch (friends.Count)
                {
                    case 0:

                        // No message should appear.

                        break;

                    case 1:
                        AssertPageContains("You have one friend.");
                        break;

                    case 2:
                        AssertPageContains("You have two friends.");
                        break;

                    case 3:
                        AssertPageContains("You have three friends.");
                        break;

                    case 4:
                        AssertPageContains("You have four friends.");
                        break;

                    case 5:
                        AssertPageContains("You have five friends.");
                        break;

                    case 6:
                        AssertPageContains("You have six friends.");
                        break;

                    case 7:
                        AssertPageContains("You have seven friends.");
                        break;

                    case 8:
                        AssertPageContains("You have eight friends.");
                        break;

                    case 9:
                        AssertPageContains("You have nine friends.");
                        break;

                    default:
                        AssertPageContains("You have " + friends.Count + " friends.");
                        break;
                }
            }

            for (var index = 0; index < friends.Count; ++index)
                AssertFriend(friends[index], candidates[index], resumes[index]);
        }

        private void AssertFriend(Member friend, ICandidate candidate, IResume resume)
        {
            AssertPageContains(friend.FullName);

            var viewer = new PersonalView(friend, PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree);

            // Current Jobs.

            if (viewer.CanAccess(PersonalVisibility.CurrentJobs))
                AssertPageContains(resume.GetCurrentJobsDisplayHtml());
            else
                AssertPageDoesNotContain(resume.GetCurrentJobsDisplayHtml());

            // Candidate Status.

            if (viewer.CanAccess(PersonalVisibility.CandidateStatus))
            {
                if (candidate.Status != CandidateStatus.Unspecified)
                    AssertPageContains(NetworkerFacade.GetCandidateStatusText(candidate.Status));
            }
            else
            {
                if (candidate.Status != CandidateStatus.Unspecified)
                    AssertPageDoesNotContain(NetworkerFacade.GetCandidateStatusText(candidate.Status));
            }

            // Suburb.

            if (viewer.CanAccess(PersonalVisibility.Suburb))
            {
                AssertPageContains(friend.Address.Location.Suburb);
                AssertPageContains(" " + friend.Address.Location.Postcode);
            }
            else
            {
                AssertPageDoesNotContain(friend.Address.Location.Suburb);
                AssertPageDoesNotContain(" " + friend.Address.Location.Postcode); // GUIDs can contain "3143", too!
            }

            // CountrySubdivision.

            if (viewer.CanAccess(PersonalVisibility.CountrySubdivision))
                AssertPageContains(friend.Address.Location.CountrySubdivision.ShortName);
            else
                AssertPageDoesNotContain(friend.Address.Location.CountrySubdivision.ShortName);
        }

        private void AssertFriendsNotVisible(IList<Member> friends, IList<Resume> resumes)
        {
            for (var index = 0; index < friends.Count; ++index)
            {
                var friend = friends[index];
                var resume = resumes[index];

                AssertPageDoesNotContain(friend.FullName);

                // Current Jobs.

                AssertPageDoesNotContain(resume.GetCurrentJobsDisplayHtml());
            }
        }

        private void AssertLinks(IList<Member> friends)
        {
            for (var index = 0; index < friends.Count; ++index)
            {
                var friend = friends[index];
                var view = new PersonalView(friend, PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree);

                var lnkFullProfile = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_Content") + "_contactsListControl_rptContacts_ctl" + index.ToString("D2") + "_ucContactsListDetails_lnkFullProfile");
                Assert.IsTrue(lnkFullProfile.IsVisible);

                var lnkViewFriends = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_Content") + "_contactsListControl_rptContacts_ctl" + index.ToString("D2") + "_ucContactsListDetails_lnkViewFriends");
                Assert.AreEqual(lnkViewFriends.IsVisible, view.CanAccess(PersonalVisibility.FriendsList));
            }
        }

        private void FollowFullNameLink(IList<Member> friends, int index)
        {
            var lnkFullName = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_Content") + "_contactsListControl_rptContacts_ctl" + index.ToString("D2") + "_ucContactsListDetails_lnkFullName");
            lnkFullName.Click();

            AssertPage<ViewFriend>();
            AssertPageContains(friends[index].FirstName);
            AssertPageContains(friends[index].LastName);
        }

        private void FollowFullProfileLink(IList<Member> friends, int index)
        {
            var lnkFullName = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_Content") + "_contactsListControl_rptContacts_ctl" + index.ToString("D2") + "_ucContactsListDetails_lnkFullProfile");
            lnkFullName.Click();

            AssertPage<ViewFriend>();
            AssertPageContains(friends[index].FirstName);
            AssertPageContains(friends[index].LastName);
        }

        private void FollowViewFriendsLink(IList<Member> friends, int index)
        {
            var lnkFullName = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_Content") + "_contactsListControl_rptContacts_ctl" + index.ToString("D2") + "_ucContactsListDetails_lnkViewFriends");
            lnkFullName.Click();

            AssertPage<ViewFriendsFriends>();
            AssertPageContains(friends[index].FullName);
        }
    }
}