using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Representatives.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Utilities;
using LinkMe.Utility.Validation;
using LinkMe.Web.Members.Friends;
using LinkMe.Web.UI.Registered.Networkers;
using LinkMe.Apps.Asp.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class ViewFriendTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();
        private readonly IRepresentativesQuery _representativesQuery = Resolve<IRepresentativesQuery>();

        private const string Suburb = "Armadale";
        private const string Postcode = "3143";
        private const string CountrySubdivision = "VIC";
        private const string Interests = "Finding the bug";
        private const string CountryLabelText = "Country";
        private const string LocationLabelText = "Location";

        private HtmlTextAreaTester _txtEmailAddresses;
        private HtmlButtonTester _btnSendInvitations;
        private HtmlLinkButtonTester _lnkAcceptInvitation;
        private HtmlLinkButtonTester _lnkIgnoreInvitation;
        private HtmlLinkTester _lnkRemoveFriend;
        private HtmlLinkTester _lnkViewResume;
        private HtmlLinkTester _lnkRemoveRepresentative;
        private HtmlButtonTester _btnRemoveRepresentative;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _txtEmailAddresses = new HtmlTextAreaTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFriends_txtEmailAddresses");
            _btnSendInvitations = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnSendInvitations");
            _lnkAcceptInvitation = new HtmlLinkButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_lnkAcceptInvitation");
            _lnkIgnoreInvitation = new HtmlLinkButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_lnkIgnoreInvitation");
            _lnkRemoveFriend = new HtmlLinkTester(Browser, "lnkRemoveFriend");
            _lnkViewResume = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_Content") + "_ahrefViewResume");
            _lnkRemoveRepresentative = new HtmlLinkTester(Browser, "lnkRemoveRepresentative");
            _btnRemoveRepresentative = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnRemoveRepresentative");
        }

        [TestMethod]
        public void TestPageElementsDisplay()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());
            AssertPage<ViewFriend>();

            // Personal details.

            AssertPageDoesNotContain("is is");
            AssertPageContains(friend.FirstName);
            AssertPageContains(friend.LastName);
            AssertPageContains(friend.Address.Location.ToString());
            AssertPageContains(friend.Address.Location.CountrySubdivision.Country.Name);
            AssertPageContains(friend.Gender.ToString());

            // Contact details.

            AssertPageContains(friend.GetPhoneNumber(PhoneNumberType.Home).Number);
            AssertPageContains(friend.GetBestEmailAddress().Address);

            // Occupation.

            AssertPageContains(string.Join(StringUtils.HTML_LINE_BREAK, (from j in resume.CurrentJobs select j.Title).ToArray()));

            // Education.

            Assert.IsTrue(resume.HasSchools());
            var school = resume.Schools[0];
            Assert.IsNotNull(school);
            AssertPageContains(school.Degree);
            AssertPageContains(school.CompletionDate.End.Value.ToString("MMMM yyyy"));
            AssertPageContains(school.Institution);

            // Interests & Affiliations

            Assert.IsNotNull(resume.Interests);
            AssertPageContains(resume.Interests);
            Assert.IsNotNull(resume.Affiliations);
            AssertPageContains(HttpUtility.HtmlEncode(resume.Affiliations));

            // Work history.

            Assert.IsTrue(resume.HasJobs());
            var previousJob = resume.PreviousJob;
            Assert.IsNotNull(previousJob);
            AssertPageContains(previousJob.Company);
            AssertPageContains(previousJob.Title);
        }

        [TestMethod]
        public void TestRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);
            _representativesCommand.CreateRepresentative(member.Id, friend.Id);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Check text and link are visible.

            AssertPageContains(friend.FirstName + " is my nominated representative.");
            Assert.IsTrue(_lnkRemoveRepresentative.IsVisible);
            Assert.IsTrue(_btnRemoveRepresentative.IsVisible);
        }

        [TestMethod]
        public void TestRemoveRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);
            _representativesCommand.CreateRepresentative(member.Id, friend.Id);
            Assert.IsNotNull(_representativesQuery.GetRepresentativeId(member.Id));

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Check text is visible and delete.

            AssertPageContains(friend.FirstName + " is my nominated representative.");
            _btnRemoveRepresentative.Click();

            AssertPage<ViewFriend>();
            AssertPageDoesNotContain(friend.FirstName + " is my nominated representative.");
            Assert.IsFalse(_lnkRemoveRepresentative.IsVisible);
            Assert.IsFalse(_btnRemoveRepresentative.IsVisible);

            Assert.IsNull(_representativesQuery.GetRepresentativeId(member.Id));
        }

        [TestMethod]
        public void TestRepresentee()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);
            _representativesCommand.CreateRepresentative(friend.Id, member.Id);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Check text and link are visible.

            AssertPageContains(friend.FirstName + " is represented by me.");
            Assert.IsTrue(_lnkRemoveRepresentative.IsVisible);
            Assert.IsTrue(_btnRemoveRepresentative.IsVisible);
        }

        [TestMethod]
        public void TestRemoveRepresentee()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);
            _representativesCommand.CreateRepresentative(friend.Id, member.Id);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Check text and link are visible.

            AssertPageContains(friend.FirstName + " is represented by me.");
            _btnRemoveRepresentative.Click();

            AssertPageDoesNotContain(friend.FirstName + " is represented by me.");
            Assert.IsFalse(_lnkRemoveRepresentative.IsVisible);
            Assert.IsFalse(_btnRemoveRepresentative.IsVisible);

            Assert.AreEqual(0, _representativesQuery.GetRepresenteeIds(member.Id).Count);
        }

        [TestMethod]
        public void TestViewResumeLinkVisibility()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Check link is visible.

            Assert.IsTrue(string.Compare(_lnkViewResume.HRef, NavigationManager.GetUrlForPage<ResumePreview>(ResumePreview.MemberIdParam, friend.Id.ToString()).PathAndQuery, true) == 0);
            Assert.IsTrue(_lnkViewResume.IsVisible);

            // Take away visibility.

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All ^ PersonalVisibility.Resume;
            _memberAccountsCommand.UpdateMember(friend);

            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());
            Assert.IsFalse(_lnkViewResume.IsVisible);
        }

        [TestMethod]
        public void TestLocationVisibility()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Check location is visible.

            AssertPageContains(LocationLabelText);
            AssertPageContains(friend.Address.Location.ToString());

            // Take away suburb.

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All ^ PersonalVisibility.Suburb;
            _memberAccountsCommand.UpdateMember(friend);

            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());
            AssertPageContains(LocationLabelText);
            AssertPageContains(friend.Address.Location.CountrySubdivision.ShortName);
            AssertPageDoesNotContain(" " + friend.Address.Location.Postcode); // GUIDs can contain "3143", too!
            AssertPageDoesNotContain(friend.Address.Location.Suburb);

            // Take away countrysubdivision.

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All ^ PersonalVisibility.CountrySubdivision;
            _memberAccountsCommand.UpdateMember(friend);

            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());
            AssertPageContains(LocationLabelText);
            AssertPageDoesNotContain(friend.Address.Location.CountrySubdivision.ShortName);
            AssertPageContains(friend.Address.Location.Postcode);
            AssertPageContains(friend.Address.Location.Suburb);

            // Take away both.

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All ^ PersonalVisibility.Suburb ^ PersonalVisibility.CountrySubdivision;
            _memberAccountsCommand.UpdateMember(friend);

            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());
            AssertPageDoesNotContain(LocationLabelText);
            AssertPageDoesNotContain(friend.Address.Location.CountrySubdivision.ShortName);
            AssertPageDoesNotContain(" " + friend.Address.Location.Postcode);
            AssertPageDoesNotContain(friend.Address.Location.Suburb);
        }

        [TestMethod]
        public void TestCountryVisibility()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Check country is visible.

            AssertPageContains(CountryLabelText);
            AssertPageContains("<td>" + friend.Address.Location.CountrySubdivision.Country.Name + "</td>");

            // Take away access.

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All ^ PersonalVisibility.Country;
            _memberAccountsCommand.UpdateMember(friend);

            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());
            AssertPageDoesNotContain(CountryLabelText);
            AssertPageDoesNotContain("<td>" + friend.Address.Location.CountrySubdivision.Country.Name + "</td>");
        }

        [TestMethod]
        public void TestResumeVisibilityToFriend()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Member friend;
            Candidate candidate;
            Resume resume;
            CreateFriend(member.Id, out friend, out candidate, out resume);

            // Log in.

            LogIn(member);
            GetPage<ViewFriend>(ViewFriend.FriendIdParameter, friend.Id.ToString());

            // Save link to resume view

            var link = _lnkViewResume.HRef;
            
            // Check that resume is viewable.

            Get(new ApplicationUrl(link));
            AssertPageContains(friend.FullName);
            AssertPageDoesNotContain(ValidationErrorMessages.NO_ACCESS_TO_RESUME);

            // Take away my resume visibility.

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All ^ PersonalVisibility.Resume;
            _memberAccountsCommand.UpdateMember(friend);
            
            // Check that error message is displayed.

            Get(new ApplicationUrl(link));
            AssertPageContains(ValidationErrorMessages.NO_ACCESS_TO_RESUME);
            AssertPageDoesNotContain(friend.FullName);
        }

        [TestMethod]
        public void TestAcceptInvitationVisibility()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = _memberAccountsCommand.CreateTestMember(1);

            // LogIn and invite the friend.

            LogIn(member);
            GetPage<InviteFriends>();
            _txtEmailAddresses.Text = friend.GetBestEmailAddress().Address;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            LogOut();

            // LogIn as friend.

            LogIn(friend);
            GetPage<ViewFriend>("friendId", member.Id.ToString("n"));
            AssertPage<ViewFriend>();
            Assert.IsFalse(_lnkRemoveFriend.IsVisible);
            Assert.IsTrue(_lnkAcceptInvitation.IsVisible);
            Assert.IsTrue(_lnkIgnoreInvitation.IsVisible);

            // Accept.

            _lnkAcceptInvitation.Click();
            AssertPage<ViewFriend>();
            AssertPageContains("You are now linked to");

            Assert.IsTrue(_lnkRemoveFriend.IsVisible);
            Assert.IsFalse(_lnkAcceptInvitation.IsVisible);
            Assert.IsFalse(_lnkIgnoreInvitation.IsVisible);

            // Navigate back to the page.

            Get(LoggedInMemberHomeUrl);
            GetPage<ViewFriend>("friendId", member.Id.ToString("n"));
            Assert.IsTrue(_lnkRemoveFriend.IsVisible);
            Assert.IsFalse(_lnkAcceptInvitation.IsVisible);
            Assert.IsFalse(_lnkIgnoreInvitation.IsVisible);
        }

        [TestMethod]
        public void TestIgnoreInvitationVisibility()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = _memberAccountsCommand.CreateTestMember(1);

            // LogIn and invite the friend.

            LogIn(member);
            GetPage<InviteFriends>();
            _txtEmailAddresses.Text = friend.GetBestEmailAddress().Address;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            LogOut();

            // LogIn as friend.

            LogIn(friend);
            GetPage<ViewFriend>("friendId", member.Id.ToString("n"));
            AssertPage<ViewFriend>();
            Assert.IsFalse(_lnkRemoveFriend.IsVisible);
            Assert.IsTrue(_lnkAcceptInvitation.IsVisible);
            Assert.IsTrue(_lnkIgnoreInvitation.IsVisible);

            // Ignore.

            _lnkIgnoreInvitation.Click();
            AssertPage<ViewFriends>();
        }

        private void CreateFriend(Guid memberId, out Member friend, out Candidate candidate, out Resume resume)
        {
            friend = _memberAccountsCommand.CreateTestMember(1);
            candidate = _candidatesCommand.GetCandidate(friend.Id);
            
            // Set values.

            candidate.Status = CandidateStatus.OpenToOffers;
            _locationQuery.ResolvePostalSuburb(friend.Address.Location, Australia, Suburb + " " + Postcode + " " + CountrySubdivision);

            friend.PhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber {Number = "123123123", Type = PhoneNumberType.Home},
                new PhoneNumber {Number = "234234234", Type = PhoneNumberType.Mobile},
                new PhoneNumber {Number = "345345345", Type = PhoneNumberType.Work},
            };
            friend.Gender = Gender.Male;

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibility.All;

            // Create a resume.

            _memberAccountsCommand.UpdateMember(friend);
            resume = CreateResume(candidate);

            // Save.

            _memberAccountsCommand.UpdateMember(friend);
            _candidatesCommand.UpdateCandidate(candidate);

            _networkingCommand.CreateFirstDegreeLink(memberId, friend.Id);
        }

        private Resume CreateResume(Candidate candidate)
        {
            var resume = new Resume();

            // Set last two job details.

            var currentJob = new Job { Dates = new PartialDateRange(PartialDate.Parse("January 2005")), Title = "Coder", Description = "description", Company = "LinkMe" };
            var previousJob = new Job { Dates = new PartialDateRange(PartialDate.Parse("January 2004"), PartialDate.Parse("January 2005")), Title = "Developer", Description = "old description", Company = "OC" };
            resume.Jobs = new List<Job> {currentJob, previousJob};

            // Set education section.

            var school = new School { CompletionDate = new PartialCompletionDate(PartialDate.Parse("January 2004")), Institution = "LinkMe Mental Asylum", Degree = "Bachelor of Boredom", Major = "Meetings", Description = "description", City = "Melbourne", Country = "Australia" };
            resume.Schools = new List<School> {school};

            // Affiliations.

            resume.Affiliations = "What's this?";
            resume.Interests = Interests;

            _candidateResumesCommand.CreateResume(candidate, resume);
            return resume;
        }
    }
}