using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestClass]
    public class CommunicationSettingsTests
        : SettingsTests
    {
        private HtmlButtonTester _communicationsSaveButton;
        private HtmlButtonTester _communicationsCancelButton;

        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _communicationsSaveButton = new HtmlButtonTester(Browser, "CommunicationsSave");
            _communicationsCancelButton = new HtmlButtonTester(Browser, "CommunicationsCancel");
        }

        [TestMethod]
        public void TestDefaults()
        {
            // By default some settings may be set up for the user so remove those first.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            _settingsCommand.DeleteSettings(member.Id);

            LogIn(member);
            Get(_settingsUrl);

            // The user's settings should reflect the defaults for the categories.

            var categories = _settingsQuery.GetCategories(UserType.Member);
            AssertCategories(categories);
            AssertSettings(categories, c => c.Timing, c => c.DefaultFrequency, c => c.Name);

            // The user's settings should also reflect the settings for the user.

            var settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);

            // Create a member with a changed regular category.

            member = _memberAccountsCommand.CreateTestMember(++index);
            settings = ChangeCategorySettings((from c in categories where c.Timing == Timing.Periodic select c).First().Id, Frequency.Monthly, member.Id);

            LogOut();
            LogIn(member);
            Get(_settingsUrl);
            AssertCategories(categories);
            AssertCategorySettings(categories, settings);

            // Create a member with a changed notification category.

            member = _memberAccountsCommand.CreateTestMember(++index);
            settings = ChangeCategorySettings((from c in categories where c.Timing == Timing.Notification select c).First().Id, Frequency.Never, member.Id);

            LogOut();
            LogIn(member);
            Get(_settingsUrl);
            AssertCategories(categories);
            AssertCategorySettings(categories, settings);

            // Create a member with changed categories.
            
            member = _memberAccountsCommand.CreateTestMember(++index);
            ChangeCategorySettings(categories.First(c => c.Timing == Timing.Periodic).Id, Frequency.Daily, member.Id);
            settings = ChangeCategorySettings((from c in categories where c.Timing == Timing.Notification select c).Last().Id, Frequency.Never, member.Id);

            LogOut();
            LogIn(member);
            Get(_settingsUrl);
            AssertCategories(categories);
            AssertCategorySettings(categories, settings);

            // Change it back.

            settings = ChangeCategorySettings((from c in categories where c.Timing == Timing.Notification select c).Last().Id, Frequency.Immediately, member.Id);

            LogOut();
            LogIn(member);
            Get(_settingsUrl);
            AssertCategories(categories);
            AssertCategorySettings(categories, settings);
        }

        [TestMethod]
        public void TestCancel()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_settingsUrl);

            Assert.IsTrue(_communicationsCancelButton.IsVisible);
            Assert.IsTrue(_communicationsSaveButton.IsVisible);

            // The user's settings should be the default.

            var categories = _settingsQuery.GetCategories(UserType.Member);
            var settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);

            // Change a regular value on the screen.

            var category = (from c in categories where c.Timing == Timing.Periodic select c).First();
            UpdatePeriodicCategory(ref settings, category, Frequency.Never, false);

            // Cancel and confirm that the values are back to what they were.

            _communicationsCancelButton.Click();
            AssertUrl(LoggedInMemberHomeUrl);

            Get(_settingsUrl);
            AssertCategorySettings(categories, settings);

            // Change some other settings.

            category = (from c in categories where c.Timing == Timing.Periodic select c).First();
            UpdatePeriodicCategory(ref settings, category, Frequency.Monthly, false);

            category = (from c in categories where c.Timing == Timing.Notification select c).First();
            UpdateNotificationCategory(ref settings, category, false, false);

            _communicationsCancelButton.Click();
            AssertUrl(LoggedInMemberHomeUrl);

            Get(_settingsUrl);
            AssertCategorySettings(categories, settings);
        }

        [TestMethod]
        public void TestSaveRegularChange()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_settingsUrl);

            Assert.IsTrue(_communicationsCancelButton.IsVisible);
            Assert.IsTrue(_communicationsSaveButton.IsVisible);

            // The user's settings should be the default.

            var categories = _settingsQuery.GetCategories(UserType.Member);
            var settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);

            // Change a regular value on the screen.

            var category = (from c in categories where c.Timing == Timing.Periodic select c).First();
            UpdatePeriodicCategory(ref settings, category, Frequency.Never, true);

            // Save.

            _communicationsSaveButton.Click();
            AssertConfirmationMessage(ValidationInfoMessages.CHANGES_SAVED);

            // Assert that the screen still shows the changes.

            Get(_settingsUrl);
            AssertCategorySettings(categories, settings);

            // Assert that the database has been udpated.

            settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);
        }

        [TestMethod]
        public void TestSaveNotificationChange()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_settingsUrl);

            Assert.IsTrue(_communicationsCancelButton.IsVisible);
            Assert.IsTrue(_communicationsSaveButton.IsVisible);

            // The user's settings should be the default.

            var categories = _settingsQuery.GetCategories(UserType.Member);
            var settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);

            // Change a notification value on the screen.

            var category = (from c in categories where c.Timing == Timing.Notification select c).First();
            UpdateNotificationCategory(ref settings, category, false, true);

            // Save.

            _communicationsSaveButton.Click();
            AssertConfirmationMessage(ValidationInfoMessages.CHANGES_SAVED);

            // Assert that the screen still shows the changes.

            Get(_settingsUrl);
            AssertCategorySettings(categories, settings);

            // Assert that the database has been udpated.

            settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);

            // Change it back.

            UpdateNotificationCategory(ref settings, category, true, true);

            // Save.

            _communicationsSaveButton.Click();
            AssertConfirmationMessage(ValidationInfoMessages.CHANGES_SAVED);

            // Assert that the screen still shows the changes.

            Get(_settingsUrl);
            AssertCategorySettings(categories, settings);

            // Assert that the database has been udpated.

            settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);
        }

        [TestMethod]
        public void TestSaveMultipleChanges()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_settingsUrl);

            Assert.IsTrue(_communicationsCancelButton.IsVisible);
            Assert.IsTrue(_communicationsSaveButton.IsVisible);

            // The user's settings should be the default.

            var categories = _settingsQuery.GetCategories(UserType.Member);
            var settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);

            // Change some regular values on the screen.

            var category = (from c in categories where c.Timing == Timing.Periodic select c).First();
            UpdatePeriodicCategory(ref settings, category, Frequency.Monthly, true);

            category = (from c in categories where c.Timing == Timing.Periodic select c).Skip(2).First();
            UpdatePeriodicCategory(ref settings, category, Frequency.Monthly, true);

            // Change some notification values on the screen.

            category = (from c in categories where c.Timing == Timing.Notification select c).First();
            UpdateNotificationCategory(ref settings, category, false, true);

            category = (from c in categories where c.Timing == Timing.Notification select c).Skip(1).First();
            UpdateNotificationCategory(ref settings, category, false, true);

            // Save.

            _communicationsSaveButton.Click();
            AssertConfirmationMessage(ValidationInfoMessages.CHANGES_SAVED);

            // Assert that the screen still shows the changes.

            Get(_settingsUrl);
            AssertCategorySettings(categories, settings);

            // Assert that the database has been udpated.

            settings = _settingsQuery.GetSettings(member.Id);
            AssertCategorySettings(categories, settings);
        }

        private void UpdatePeriodicCategory(ref RecipientSettings settings, Category category, Frequency newFrequency, bool updateSettings)
        {
            var frequency = GetFrequency(category, settings);

            // Check the correct frequency is being shown and that it will be changed.

            var tester = new HtmlDropDownListTester(Browser, category.Name);
            Assert.AreEqual(frequency.ToString(), tester.SelectedItem.Text);
            Assert.IsTrue(frequency != newFrequency);

            // Update the selected item.

            tester.SelectedValue = newFrequency.ToString();

            if (updateSettings)
                UpdateSettings(ref settings, category, newFrequency);
        }

        private static void UpdateSettings(ref RecipientSettings settings, Category category, Frequency newFrequency)
        {
            if (settings == null)
                settings = new RecipientSettings { CategorySettings = new List<CategorySettings>(), DefinitionSettings = new List<DefinitionSettings>() };

            var categorySettings = (from s in settings.CategorySettings where s.CategoryId == category.Id select s).SingleOrDefault();
            if (categorySettings == null)
            {
                categorySettings = new CategorySettings { CategoryId = category.Id, Frequency = newFrequency };
                settings.CategorySettings.Add(categorySettings);
            }
            else
            {
                categorySettings.Frequency = newFrequency;
            }
        }

        private void UpdateNotificationCategory(ref RecipientSettings settings, Category category, bool immediate, bool updateSettings)
        {
            var notificationTester = new HtmlCheckBoxTester(Browser, category.Name);
            Assert.IsTrue(notificationTester.IsChecked == !immediate);
            notificationTester.IsChecked = immediate;

            if (updateSettings)
                UpdateSettings(ref settings, category, immediate ? Frequency.Immediately : Frequency.Never);
        }

        private void AssertCategories(IEnumerable<Category> categories)
        {
            // Ensure all testers are visible for all regular type categories.

            var periodicCategories = from c in categories
                                      where c.Timing == Timing.Periodic
                                      select c;

            foreach (var periodicCategory in periodicCategories)
            {
                var tester = new HtmlDropDownListTester(Browser, periodicCategory.Name);
                Assert.IsTrue(tester.IsVisible);
                Assert.IsTrue(periodicCategory.AvailableFrequencies.SequenceEqual((from i in tester.Items select (Frequency)Enum.Parse(typeof(Frequency), i.Value))));
            }

            // Ensure all testers are visible for all notification type categories.

            var notificationTesters = from c in categories
                                      where c.Timing == Timing.Notification
                                      select new HtmlCheckBoxTester(Browser, c.Name);

            foreach (var tester in notificationTesters)
                Assert.IsTrue(tester.IsVisible);
        }

        private RecipientSettings ChangeCategorySettings(Guid categoryId, Frequency frequency, Guid memberId)
        {
            // Update the database.

            _settingsCommand.SetFrequency(memberId, categoryId, frequency);
            return _settingsQuery.GetSettings(memberId);
        }

        private void AssertCategorySettings(IEnumerable<Category> categories, RecipientSettings settings)
        {
            AssertSettings(
                from c in categories where c.UserTypes.IsFlagSet(UserType.Member) select c,
                c => c.Timing,
                c => GetFrequency(c, settings),
                c => c.Name);
        }

        private static Frequency GetFrequency(Category category, RecipientSettings settings)
        {
            if (settings == null)
                return category.DefaultFrequency;

            // Look for a saved setting.

            var categorySettings = (from s in settings.CategorySettings where s.CategoryId == category.Id select s).SingleOrDefault();
            if (categorySettings == null || categorySettings.Frequency == null)
                return category.DefaultFrequency;

            return categorySettings.Frequency.Value;
        }

        private void AssertSettings<T>(IEnumerable<T> source, Func<T, Timing> getTiming, Func<T, Frequency> getFrequency, Func<T, string> getName)
        {
            // Assert the regular categories.

            var periodicRows = from s in source
                               where getTiming(s) == Timing.Periodic
                               select new { Frequency = getFrequency(s), Tester = new HtmlDropDownListTester(Browser, getName(s))};

            foreach (var row in periodicRows)
                AssertTester(row.Frequency, row.Tester);

            // Assert the notification categories.

            var notificationRows = from s in source
                                   where getTiming(s) == Timing.Notification
                                   select new { Frequency = getFrequency(s), Tester = new HtmlCheckBoxTester(Browser, getName(s))};

            foreach (var row in notificationRows)
                AssertTester(row.Frequency, row.Tester);
        }

        private static void AssertTester(Frequency frequency, HtmlDropDownListTester tester)
        {
            // Assert that the tester is visible.

            Assert.IsTrue(tester.IsVisible);

            // The tester value should correspond to the frequency.

            Assert.AreEqual(frequency.ToString(), tester.SelectedItem.Text);
        }

        private static void AssertTester(Frequency frequency, HtmlCheckBoxTester tester)
        {
            // Assert that the tester is visible.

            Assert.IsTrue(tester.IsVisible);

            // The tester value should correspond to the frequency.

            Assert.AreEqual(tester.IsChecked, frequency == Frequency.Immediately);
        }
    }
}