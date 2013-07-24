using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Api.Areas.Employers.Models.Search;
using LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Devices.Apple;
using LinkMe.Domain.Devices.Apple.Commands;
using LinkMe.Domain.Devices.Apple.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Searches
{
    [TestClass]
    public abstract class SearchesTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAppleDevicesCommand _appleDevicesCommand = Resolve<IAppleDevicesCommand>();
        private readonly IMemberSearchesQuery _memberSearchesQuery = Resolve<IMemberSearchesQuery>();
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery = Resolve<IMemberSearchAlertsQuery>();
        private readonly IAppleDevicesQuery _appleDevicesQuery = Resolve<IAppleDevicesQuery>();
        protected readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        protected ReadOnlyUrl _searchesUrl;
        private ReadOnlyUrl _baseSearchUrl;

        protected const string NameFormat = "My search {0}";
        protected const string Keywords = "Analyst";
        protected const string NewKeywords = "Archeologist";

        [TestInitialize]
        public void SearchesTestsInitialize()
        {
            _searchesUrl = new ReadOnlyApplicationUrl("~/v1/employers/searches");
            _baseSearchUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/searches/");
        }

        protected ReadOnlyUrl GetSearchUrl(Guid searchId)
        {
            return new ReadOnlyApplicationUrl(_baseSearchUrl, searchId.ToString());
        }

        protected ReadOnlyUrl GetSearchesUrl(string deviceToken)
        {
            return new ReadOnlyApplicationUrl(true, _searchesUrl.ToString(), new ReadOnlyQueryString("deviceToken", deviceToken));
        }

        protected Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        protected AppleDevice CreateDevice(Guid ownerId, string deviceToken)
        {
            var device = new AppleDevice {Active = true, DeviceToken = "BogusDeviceToken", OwnerId = ownerId};
            _appleDevicesCommand.CreateDevice(device);

            return device;
        }

        protected MemberSearchesResponseModel Searches()
        {
            return Deserialize<MemberSearchesResponseModel>(Get(_searchesUrl), new MemberSearchCriteriaJavaScriptConverter(_locationQuery, _industriesQuery));
        }

        protected static void AssertSearches(MemberSearchesResponseModel model, params Tuple<MemberSearch, Tuple<AlertType, bool>>[] expectedSearches)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedSearches.Length, model.MemberSearches.Count);
            foreach (var expectedSearch in expectedSearches)
            {
                var expectedSearchName = expectedSearch.Item1.Name;
                var searchModel = (from s in model.MemberSearches where s.Name == expectedSearchName select s).Single();
                AssertSearch(expectedSearch.Item1, expectedSearch.Item2.Item2, searchModel);
            }
        }

        private static void AssertSearch(MemberSearch expectedSearch, bool expectedIsAlert, MemberSearchModel model)
        {
            Assert.AreEqual(expectedSearch.Name, model.Name);
            Assert.AreEqual(expectedIsAlert, model.IsAlert);
            AssertAreEqual(expectedSearch.Criteria, model.Criteria);
        }

        protected void AssertSearches(Employer employer, string expectedDeviceToken, params Tuple<MemberSearch, Tuple<AlertType, bool>>[] expectedSearches)
        {
            var searches = _memberSearchesQuery.GetMemberSearches(employer.Id);
            Assert.AreEqual(expectedSearches.Length, searches.Count);
            foreach (var expectedSearch in expectedSearches)
            {
                var expectedSearchName = expectedSearch.Item1.Name;
                var search = (from s in searches where s.Name == expectedSearchName select s).Single();
                var isAlert = _memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, expectedSearch.Item2.Item1) != null;
                AssertSearch(expectedSearch.Item1, expectedSearch.Item2.Item2, search, isAlert);
            }

            AssertDeviceToken(_appleDevicesQuery.GetDevices(employer.Id), expectedDeviceToken);
        }

        private static void AssertSearch(MemberSearch expectedSearch, bool expectedIsAlert, MemberSearch search, bool isAlert)
        {
            Assert.AreEqual(expectedSearch.Name, search.Name);
            Assert.AreEqual(expectedIsAlert, isAlert);
            AssertAreEqual(expectedSearch.Criteria, search.Criteria);
        }

        private static void AssertAreEqual(MemberSearchCriteria expectedCriteria, MemberSearchCriteria criteria)
        {
            if (expectedCriteria == null || expectedCriteria.IsEmpty)
            {
                Assert.IsTrue(criteria == null || criteria.IsEmpty);
            }
            else
            {
                Assert.IsFalse(criteria.IsEmpty);
                Assert.AreEqual(expectedCriteria.EducationKeywords, criteria.EducationKeywords);
                Assert.IsTrue(expectedCriteria.IndustryIds.NullableCollectionEqual(criteria.IndustryIds));
                Assert.AreEqual(expectedCriteria.JobTitle, criteria.JobTitle);
                Assert.AreEqual(expectedCriteria.DesiredJobTitle, criteria.DesiredJobTitle);
                Assert.AreEqual(expectedCriteria.JobTitlesToSearch, criteria.JobTitlesToSearch);
                Assert.AreEqual(expectedCriteria.CompanyKeywords, criteria.CompanyKeywords);
                Assert.AreEqual(expectedCriteria.CompaniesToSearch, criteria.CompaniesToSearch);
                Assert.AreEqual(expectedCriteria.Location, criteria.Location);
                Assert.AreEqual(expectedCriteria.Distance, criteria.Distance);
                Assert.AreEqual(expectedCriteria.IncludeRelocating, criteria.IncludeRelocating);
                Assert.AreEqual(expectedCriteria.IncludeInternational, criteria.IncludeInternational);
                Assert.AreEqual(expectedCriteria.InFolder, criteria.InFolder);
                Assert.AreEqual(expectedCriteria.IsFlagged, criteria.IsFlagged);
                Assert.AreEqual(expectedCriteria.HasNotes, criteria.HasNotes);
                Assert.AreEqual(expectedCriteria.HasViewed, criteria.HasViewed);
                Assert.AreEqual(expectedCriteria.IsUnlocked, criteria.IsUnlocked);
                Assert.AreEqual(expectedCriteria.CommunityId, criteria.CommunityId);
                Assert.AreEqual(expectedCriteria.AnyKeywords, criteria.AnyKeywords);
                Assert.AreEqual(expectedCriteria.AllKeywords, criteria.AllKeywords);
                Assert.AreEqual(expectedCriteria.ExactPhrase, criteria.ExactPhrase);
                Assert.AreEqual(expectedCriteria.WithoutKeywords, criteria.WithoutKeywords);
                Assert.AreEqual(expectedCriteria.GetKeywords(), criteria.GetKeywords());
                Assert.AreEqual(expectedCriteria.Salary, criteria.Salary);
                Assert.AreEqual(expectedCriteria.Recency, criteria.Recency);
                Assert.AreEqual(expectedCriteria.IncludeSynonyms, criteria.IncludeSynonyms);
                Assert.AreEqual(expectedCriteria.SortCriteria.SortOrder, criteria.SortCriteria.SortOrder);
                Assert.AreEqual(expectedCriteria.SortCriteria.ReverseSortOrder, criteria.SortCriteria.ReverseSortOrder);
            }
        }

        private static void AssertDeviceToken(IEnumerable<AppleDevice> devices, string expectedDeviceToken)
        {
            if (!string.IsNullOrEmpty(expectedDeviceToken))
                Assert.IsTrue(devices.Select(d => d.DeviceToken).ToArray().Contains(expectedDeviceToken));
        }

        protected Tuple<MemberSearch, Tuple<AlertType, bool>> MakeTupleTuple(MemberSearch search, AlertType alertType, bool isAlert)
        {
            return new Tuple<MemberSearch, Tuple<AlertType, bool>>(search, new Tuple<AlertType, bool>(alertType, isAlert));
        }

    }
}