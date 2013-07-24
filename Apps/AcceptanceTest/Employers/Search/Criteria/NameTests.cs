using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using LinkMe.Web.UI.Registered.Networkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class NameTests
        : CriteriaTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();

        private const string Name = "Homer";

        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlButtonTester _saveButton;

        private HtmlButtonTester _btnSave;
        private HtmlCheckBoxTester _chkAnonResume;
        private HtmlCheckBoxTester _chkName;

        private ReadOnlyUrl _settingsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _saveButton = new HtmlButtonTester(Browser, "save");

            _btnSave = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnSave");
            _chkAnonResume = new HtmlCheckBoxTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucEmployerPrivacy_chkAnonResume", false);
            _chkName = new HtmlCheckBoxTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucEmployerPrivacy_chkName", false);

            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/members/settings");
        }

        protected override void TestDisplay()
        {
            // Must have unlimited credits.

            LogIn(CreateEmployer(null));

            var criteria = new MemberSearchCriteria { Name = Name, IncludeSimilarNames = false };
            TestDisplay(criteria);

            criteria = new MemberSearchCriteria { Name = Name, IncludeSimilarNames = true };
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestAnonymousExactName()
        {
            // Create members.

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            // The name criteria is effectively ignored which makes the criteria empty which means no-one returned.

            AssertSearch(member0.FullName, false);
            AssertSearch(member1.FullName, false);
            AssertSearch(member2.FullName, false);
        }

        [TestMethod]
        public void TestLimitedExactName()
        {
            // Create members.

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            LogIn(CreateEmployer(100));

            // The name criteria is effectively ignored which makes the criteria empty which means no-one returned.

            AssertSearch(member0.FullName, false);
            AssertSearch(member1.FullName, false);
            AssertSearch(member2.FullName, false);
        }

        [TestMethod]
        public void TestUnlimitedExactName()
        {
            // Create members.

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            LogIn(CreateEmployer(null));

            // Search.

            AssertSearch(member0.FullName, false, member0);
            AssertSearch(member1.FullName, false, member1);
            AssertSearch(member2.FullName, false, member2);
        }

        [TestMethod]
        public void TestUpdateName()
        {
            // Create members.

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);

            var employer = CreateEmployer(null);
            LogIn(employer);

            // Search.

            AssertSearch(member0.FullName, false, member0);
            AssertSearch(member1.FullName, false, member1);
            AssertSearch(member2.FullName, false, member2);

            // Update the name.

            LogOut();
            LogIn(member1);

            Get(_settingsUrl);
            _firstNameTextBox.Text = member0.FirstName;
            _lastNameTextBox.Text = member0.LastName;
            _saveButton.Click();

            LogOut();

            member1 = _membersQuery.GetMember(member1.Id);

            // Search.

            LogIn(employer);
            AssertSearch(member0.FullName, false, member0, member1);
            AssertSearch(member1.FullName, false, member0, member1);
            AssertSearch(member2.FullName, false, member2);
        }

        [TestMethod]
        public void TestHiddenName()
        {
            // Create members.

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);

            // Make the name the same but hide it.

            member1.FirstName = member0.FirstName;
            member1.LastName = member0.LastName;
            member1.VisibilitySettings.Professional.EmploymentVisibility = member1.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Name);
            _memberAccountsCommand.UpdateMember(member1);
            _memberSearchService.UpdateMember(member1.Id);

            var employer = CreateEmployer(null);
            LogIn(employer);

            // Search.

            AssertSearch(member0.FullName, false, member0);
            AssertSearch(member1.FullName, false, member0);
        }

        [TestMethod]
        public void TestHideName()
        {
            // Create members.

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);

            // Make the name the same.

            member1.FirstName = member0.FirstName;
            member1.LastName = member0.LastName;
            _memberAccountsCommand.UpdateMember(member1);
            _memberSearchService.UpdateMember(member1.Id);

            // Search.

            var employer = CreateEmployer(null);
            LogIn(employer);
            AssertSearch(member0.FullName, false, member0, member1);
            AssertSearch(member1.FullName, false, member0, member1);
            LogOut();

            // Hide name.

            LogIn(member1);
            GetPage<VisibilitySettingsBasic>();
            Assert.IsTrue(_chkAnonResume.IsChecked);
            Assert.IsTrue(_chkName.IsChecked);
            _chkName.IsChecked = false;
            _btnSave.Click();
            LogOut();

            LogIn(employer);
            AssertSearch(member0.FullName, false, member0);
            AssertSearch(member1.FullName, false, member0);
            LogOut();

            // Unhide name.

            LogIn(member1);
            GetPage<VisibilitySettingsBasic>();
            Assert.IsTrue(_chkAnonResume.IsChecked);
            Assert.IsFalse(_chkName.IsChecked);
            _chkName.IsChecked = true;
            _btnSave.Click();
            LogOut();

            LogIn(employer);
            AssertSearch(member0.FullName, false, member0, member1);
            AssertSearch(member1.FullName, false, member0, member1);
            LogOut();

            // Hide resume.

            LogIn(member1);
            GetPage<VisibilitySettingsBasic>();
            Assert.IsTrue(_chkAnonResume.IsChecked);
            Assert.IsTrue(_chkName.IsChecked);
            _chkAnonResume.IsChecked = false;
            _btnSave.Click();
            LogOut();

            LogIn(employer);
            AssertSearch(member0.FullName, false, member0);
            AssertSearch(member1.FullName, false, member0);
            LogOut();

            // Unhide resume.

            LogIn(member1);
            GetPage<VisibilitySettingsBasic>();
            Assert.IsFalse(_chkAnonResume.IsChecked);
            Assert.IsFalse(_chkName.IsChecked);
            _chkAnonResume.IsChecked = true;
            _chkName.IsChecked = true;
            _btnSave.Click();
            LogOut();

            LogIn(employer);
            AssertSearch(member0.FullName, false, member0, member1);
            AssertSearch(member1.FullName, false, member0, member1);
            LogOut();
        }

        private void AssertSearch(string name, bool includeSimilarNames, params Member[] members)
        {
            var criteria = new MemberSearchCriteria { Name = name, IncludeSimilarNames = includeSimilarNames };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members);
        }

        private Employer CreateEmployer(int? quantity)
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = quantity, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            return employer;
        }
    }
}