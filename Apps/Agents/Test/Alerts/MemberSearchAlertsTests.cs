using System;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Alerts
{
    [TestClass]
    public class MemberSearchAlertsTests
        : TestClass
    {
        private const string KeywordsFormat = "Business analyst {0}";
        private const string NameFormat = "My search {0}";
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand = Resolve<IMemberSearchAlertsCommand>();
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery = Resolve<IMemberSearchAlertsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateMemberSearch()
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearch(owner, search);

            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.Id));
            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.Email));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.AppleDevice));
        }

        [TestMethod]
        public void TestCreateMemberAlert()
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearchAlert(owner, search, AlertType.Email);

            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.Id));
            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.IsNotNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.Email));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.AppleDevice));
        }

        [TestMethod]
        public void TestUpdateMemberSearch()
        {
            // Create it.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearch(owner, search);

            // Update it.

            search.Criteria = CreateCriteria(2);
            _memberSearchAlertsCommand.UpdateMemberSearch(owner, search);

            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.Id));
            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.AreEqual(0, _memberSearchAlertsQuery.GetMemberSearchAlerts(search.Id, null).Count);
        }

        [TestMethod]
        public void TestUpdateMemberAlert()
        {
            // Create it.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearchAlert(owner, search, AlertType.AppleDevice);

            // Update it.

            search.Criteria = CreateCriteria(2);
            _memberSearchAlertsCommand.UpdateMemberSearch(owner, search, new Tuple<AlertType, bool>(AlertType.Email, true));

            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.Id));
            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.IsNotNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.AppleDevice));
        }

        [TestMethod]
        public void TestUpdateMemberSearchToAlert()
        {
            // Create it.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearch(owner, search);

            // Update it.

            search.Criteria = CreateCriteria(2);
            _memberSearchAlertsCommand.UpdateMemberSearch(owner, search, new Tuple<AlertType, bool>(AlertType.Email, true));

            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.Id));
            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.IsNotNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.Email));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.AppleDevice));
        }

        [TestMethod]
        public void TestUpdateMemberAlertToSearch()
        {
            // Create it.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearchAlert(owner, search, AlertType.Email, AlertType.AppleDevice);

            // Update it.

            search.Criteria = CreateCriteria(2);
            _memberSearchAlertsCommand.UpdateMemberSearch(owner, search, new Tuple<AlertType, bool>(AlertType.AppleDevice, false));

            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.Id));
            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.AppleDevice));
            Assert.IsNotNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.Email));
        }

        [TestMethod]
        public void TestDeleteMemberSearch()
        {
            // Create it.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearch(owner, search);

            // Delete it.

            _memberSearchAlertsCommand.DeleteMemberSearch(owner, search);

            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearch(search.Id));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.AreEqual(0, _memberSearchAlertsQuery.GetMemberSearchAlerts(search.Id, null).Count);
        }

        [TestMethod]
        public void TestDeleteMemberAlert()
        {
            // Create it.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearchAlert(owner, search, AlertType.Email, AlertType.AppleDevice);

            // Delete it.

            _memberSearchAlertsCommand.DeleteMemberSearch(owner, search);

            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearch(search.Id));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.AreEqual(0, _memberSearchAlertsQuery.GetMemberSearchAlerts(search.Id, null).Count);
        }

        [TestMethod]
        public void TestDeleteJustMemberAlert()
        {
            // Create it.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = CreateMemberSearch(1);
            _memberSearchAlertsCommand.CreateMemberSearchAlert(owner, search, AlertType.Email, AlertType.AppleDevice);

            // Delete it.

            _memberSearchAlertsCommand.DeleteMemberSearchAlert(owner, search, AlertType.Email);

            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.Id));
            AssertSearch(search, _memberSearchAlertsQuery.GetMemberSearch(search.OwnerId, search.Name));
            Assert.IsNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.Email));
            Assert.IsNotNull(_memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, AlertType.AppleDevice));
        }

        private static void AssertSearch(MemberSearch expectedSearch, MemberSearch search)
        {
            Assert.AreEqual(expectedSearch.Criteria, search.Criteria);
            Assert.AreEqual(expectedSearch.Name, search.Name);
            Assert.AreEqual(expectedSearch.Id, search.Id);
            Assert.AreEqual(expectedSearch.OwnerId, search.OwnerId);
        }

        private static MemberSearch CreateMemberSearch(int index)
        {
            return new MemberSearch
            {
                Criteria = CreateCriteria(index),
                Name = string.Format(NameFormat, index),
            };
        }

        private static MemberSearchCriteria CreateCriteria(int index)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(string.Format(KeywordsFormat, index));
            return criteria;
        }
    }
}
