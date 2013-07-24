using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Web.UI.Registered.Networkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestClass]
    public class VisibilitySettingsBasicTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();

        private HtmlButtonTester _saveButton;
        private HtmlRadioButtonTester _moderatelyVisibleButton;
        private HtmlRadioButtonTester _activeButton;
        private HtmlCheckBoxTester _anonResumeCheckBox;
        private HtmlCheckBoxTester _nameCheckBox;
        private HtmlCheckBoxTester _phoneCheckBox;
        private HtmlCheckBoxTester _photoCheckBox;
        private HtmlCheckBoxTester _recentEmployersCheckBox;
        private HtmlRadioButtonTester _highlyVisibleButton;
        private HtmlRadioButtonTester _notLookingButton;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _saveButton = new HtmlButtonTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_btnSave");
            _moderatelyVisibleButton = new HtmlRadioButtonTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucVisibilityBasicSettings_rdoModeratelyVisible");
            _activeButton = new HtmlRadioButtonTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucWorkStatusSettings_rdoActive");
            _anonResumeCheckBox = new HtmlCheckBoxTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucEmployerPrivacy_chkAnonResume", false);
            _nameCheckBox = new HtmlCheckBoxTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucEmployerPrivacy_chkName", false);
            _phoneCheckBox = new HtmlCheckBoxTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucEmployerPrivacy_chkPhone", false);
            _photoCheckBox = new HtmlCheckBoxTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucEmployerPrivacy_chkPhoto", false);
            _recentEmployersCheckBox = new HtmlCheckBoxTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucEmployerPrivacy_chkRecentEmployers", false);
            _highlyVisibleButton = new HtmlRadioButtonTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucVisibilityBasicSettings_rdoHighlyVisible");
            _notLookingButton = new HtmlRadioButtonTester(Browser, "ctl00_ctl00_ctl00_Body_FormContent_Content_ucWorkStatusSettings_rdoNotLooking");
        }

        [TestMethod]
        public void TestDefaults()
        {
            var member = CreateMember(0, false);
            LogIn(member);
            GetPage<VisibilitySettingsBasic>();

            Assert.IsTrue(_moderatelyVisibleButton.IsChecked);

            Assert.IsTrue(_anonResumeCheckBox.IsChecked);
            Assert.IsTrue(_nameCheckBox.IsChecked);
            Assert.IsTrue(_phoneCheckBox.IsChecked);
            Assert.IsTrue(!_photoCheckBox.IsChecked);
            Assert.IsTrue(_recentEmployersCheckBox.IsChecked);

            Assert.IsTrue(_activeButton.IsChecked);
        }

        [TestMethod]
        public void TestEditPersonalVisibility()
        {
            TestEditPersonalVisibility(CreateMember(0, false));
            TestEditPersonalVisibility(CreateMember(1, true));
        }

        [TestMethod]
        public void TestEditStatus()
        {
            TestEditStatus(CreateMember(0, false));
            TestEditStatus(CreateMember(1, true));
        }

        [TestMethod]
        public void TestEditResumeVisibility()
        {
            TestEditResumeVisibility(CreateMember(0, false));
            TestEditResumeVisibility(CreateMember(1, true));
        }

        [TestMethod]
        public void TestChangeEmployerPrivacyFullResume()
        {
            TestEditProfessionalVisibility(CreateMember(0, false));
            TestEditProfessionalVisibility(CreateMember(1, true));
        }

        private void TestEditPersonalVisibility(Member member)
        {
            LogIn(member);
            GetPage<VisibilitySettingsBasic>();

            Assert.AreEqual(PersonalVisibilitySettings.ModerateFirstDegree, member.VisibilitySettings.Personal.FirstDegreeVisibility);
            Assert.AreEqual(PersonalVisibilitySettings.ModerateSecondDegree, member.VisibilitySettings.Personal.SecondDegreeVisibility);
            Assert.AreEqual(PersonalVisibilitySettings.ModeratePublic, member.VisibilitySettings.Personal.PublicVisibility);

            Assert.IsTrue(_moderatelyVisibleButton.IsChecked);
            Assert.IsFalse(_highlyVisibleButton.IsChecked);

            _highlyVisibleButton.IsChecked = true;
            _saveButton.Click();

            GetPage<VisibilitySettingsBasic>();
            AssertPage<VisibilitySettingsBasic>();

            member = _membersQuery.GetMember(member.Id);
            Assert.AreEqual(PersonalVisibilitySettings.HighFirstDegree, member.VisibilitySettings.Personal.FirstDegreeVisibility);
            Assert.AreEqual(PersonalVisibilitySettings.HighSecondDegree, member.VisibilitySettings.Personal.SecondDegreeVisibility);
            Assert.AreEqual(PersonalVisibilitySettings.HighPublic, member.VisibilitySettings.Personal.PublicVisibility);

            Assert.IsFalse(_moderatelyVisibleButton.IsChecked);
            Assert.IsTrue(_highlyVisibleButton.IsChecked);
        }

        private void TestEditStatus(IUser member)
        {
            LogIn(member);
            GetPage<VisibilitySettingsBasic>();

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.AreEqual(CandidateStatus.ActivelyLooking, candidate.Status);

            Assert.IsTrue(_activeButton.IsChecked);
            Assert.IsFalse(_notLookingButton.IsChecked);

            _notLookingButton.IsChecked = true;
            _saveButton.Click();

            GetPage<VisibilitySettingsBasic>();
            AssertPage<VisibilitySettingsBasic>();

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.AreEqual(CandidateStatus.NotLooking, candidate.Status);

            Assert.IsFalse(_activeButton.IsChecked);
            Assert.IsTrue(_notLookingButton.IsChecked);
        }

        private void TestEditResumeVisibility(Member member)
        {
            LogIn(member);
            GetPage<VisibilitySettingsBasic>();

            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.ProfilePhoto));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.RecentEmployers));

            Assert.IsTrue(_anonResumeCheckBox.IsChecked);
            Assert.IsTrue(_nameCheckBox.IsChecked);
            Assert.IsTrue(_phoneCheckBox.IsChecked);
            Assert.IsFalse(_photoCheckBox.IsChecked);
            Assert.IsTrue(_recentEmployersCheckBox.IsChecked);

            _anonResumeCheckBox.IsChecked = false;
            _saveButton.Click();

            GetPage<VisibilitySettingsBasic>();
            AssertPage<VisibilitySettingsBasic>();

            member = _membersQuery.GetMember(member.Id);
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.ProfilePhoto));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.RecentEmployers));

            Assert.IsFalse(_anonResumeCheckBox.IsChecked);
            Assert.IsFalse(_nameCheckBox.IsChecked);
            Assert.IsFalse(_phoneCheckBox.IsChecked);
            Assert.IsFalse(_photoCheckBox.IsChecked);
            Assert.IsFalse(_recentEmployersCheckBox.IsChecked);
        }

        private void TestEditProfessionalVisibility(Member member)
        {
            LogIn(member);
            GetPage<VisibilitySettingsBasic>();

            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.ProfilePhoto));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.RecentEmployers));

            Assert.IsTrue(_anonResumeCheckBox.IsChecked);
            Assert.IsTrue(_nameCheckBox.IsChecked);
            Assert.IsTrue(_phoneCheckBox.IsChecked);
            Assert.IsFalse(_photoCheckBox.IsChecked);
            Assert.IsTrue(_recentEmployersCheckBox.IsChecked);

            _recentEmployersCheckBox.IsChecked = false;
            _saveButton.Click();

            GetPage<VisibilitySettingsBasic>();
            AssertPage<VisibilitySettingsBasic>();

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name));
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.ProfilePhoto));
            Assert.IsFalse(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.RecentEmployers));

            Assert.IsTrue(_anonResumeCheckBox.IsChecked);
            Assert.IsTrue(_nameCheckBox.IsChecked);
            Assert.IsTrue(_phoneCheckBox.IsChecked);
            Assert.IsFalse(_photoCheckBox.IsChecked);
            Assert.IsFalse(_recentEmployersCheckBox.IsChecked);
        }

        private Member CreateMember(int index, bool withInvalidPostalSuburb)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            member.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibilitySettings.ModerateFirstDegree;
            member.VisibilitySettings.Personal.SecondDegreeVisibility = PersonalVisibilitySettings.ModerateSecondDegree;
            member.VisibilitySettings.Personal.PublicVisibility = PersonalVisibilitySettings.ModeratePublic;

            member.Address.Location = withInvalidPostalSuburb
                ? _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "xyz")
                : _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Melbourne VIC 3000");

            _memberAccountsCommand.UpdateMember(member);

            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.Status = CandidateStatus.ActivelyLooking;
            _candidatesCommand.UpdateCandidate(candidate);

            return member;
        }
    }
}