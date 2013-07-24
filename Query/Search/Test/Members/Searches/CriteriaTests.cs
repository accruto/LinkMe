using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class CriteriaTests
        : MemberSearchTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        [TestMethod]
        public void TestNonDefaultAdvancedCriteria()
        {
            var criteria = new MemberSearchCriteria
            {
                CommunityId = Guid.NewGuid(),
                IndustryIds = new List<Guid> { _industriesQuery.GetIndustries()[0].Id },
                JobTitle = "Architect",
                SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated },
                CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking,
                CompanyKeywords = "LinkMe",
                DesiredJobTitle = "Landscape gardener",
                Distance = 32,
                EthnicStatus = EthnicStatus.Aboriginal,
                JobTypes = JobTypes.Contract,
                IncludeRelocating = true,
                JobTitlesToSearch = JobsToSearch.LastJob,
            };

            TestCriteria(criteria);
        }

        [TestMethod]
        public void TestLocationAdvancedCriteria()
        {
            TestLocationCriteria();
        }

        [TestMethod]
        public void TestKeywordsCriteria()
        {
            // Keywords

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(".NET");
            TestCriteria(criteria, ".NET", ".NET", ".NET", null, null, null);

            // All

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(".NET", null, null, null);
            TestCriteria(criteria, ".NET", ".NET", ".NET", null, null, null);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(".NET Java", null, null, null);
            TestCriteria(criteria, ".NET Java", ".NET Java", ".NET Java", null, null, null);

            // Exact

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, ".NET", null, null);
            TestCriteria(criteria, ".NET", ".NET", null, ".NET", null, null);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, ".NET Java", null, null);
            TestCriteria(criteria, "\".NET Java\"", "\".NET Java\"", null, ".NET Java", null, null);

            // Any

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, ".NET", null);
            TestCriteria(criteria, ".NET", ".NET", null, null, ".NET", null);

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, ".NET Java", null);
            TestCriteria(criteria, ".NET OR Java", ".NET OR Java", null, null, ".NET Java", null);

            // Without

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords("SOME", null, null, ".NET");
            TestCriteria(criteria, "SOME AND NOT .NET", "SOME AND NOT .NET", "SOME", null, null, ".NET");

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords("SOME", null, null, ".NET Java");
            TestCriteria(criteria, "SOME AND NOT (.NET OR Java)", "SOME AND NOT (.NET OR Java)", "SOME", null, null, ".NET Java");

            // Combined

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(".NET1", ".NET2", ".NET3", ".NET4");
            TestCriteria(criteria, ".NET1 .NET2 .NET3 AND NOT .NET4", ".NET1 .NET2 .NET3 AND NOT .NET4", ".NET1", ".NET2", ".NET3", ".NET4");

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(".NET1", ".NET2 Java2", ".NET3 Java3", ".NET4 Java4");
            TestCriteria(criteria, ".NET1 \".NET2 Java2\" (.NET3 OR Java3) AND NOT (.NET4 OR Java4)", ".NET1 \".NET2 Java2\" (.NET3 OR Java3) AND NOT (.NET4 OR Java4)", ".NET1", ".NET2 Java2", ".NET3 Java3", ".NET4 Java4");
        }

        [TestMethod]
        public void TestChangeToOred()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("word and \"quoted text\"");

            var oredSimple = criteria.ChangeKeywordsToOred();
            Assert.AreEqual("word OR \"quoted text\"", oredSimple.KeywordsExpression.GetUserExpression());
            Assert.AreEqual("word OR \"quoted text\"", oredSimple.GetKeywords());

            var otherCriteria = new MemberSearchCriteria();
            otherCriteria.SetKeywords(criteria.KeywordsExpression.GetUserExpression());
            var oredAdvanced = otherCriteria.ChangeKeywordsToOred();
            Assert.AreEqual("word OR \"quoted text\"", oredAdvanced.KeywordsExpression.GetUserExpression());
            Assert.AreEqual("word \"quoted text\"", oredAdvanced.AnyKeywords);
        }

        private void TestCriteria(MemberSearchCriteria criteria, string keywords, string keywordsExpression, string allKeywords, string exactPhrase, string anyKeywords, string withoutKeywords)
        {
            Assert.AreEqual(keywords, criteria.GetKeywords());
            Assert.AreEqual(keywordsExpression, criteria.KeywordsExpression.GetUserExpression());
            Assert.AreEqual(allKeywords, criteria.AllKeywords);
            Assert.AreEqual(exactPhrase, criteria.ExactPhrase);
            Assert.AreEqual(anyKeywords, criteria.AnyKeywords);
            Assert.AreEqual(withoutKeywords, criteria.WithoutKeywords);

            TestCriteria(criteria);
        }

        private void TestLocationCriteria()
        {
            var country = _locationQuery.GetCountry("Australia");
            var location = _locationQuery.ResolveLocation(country, string.Empty);
            TestCriteria(new MemberSearchCriteria { Location = location });

            location = _locationQuery.ResolveLocation(country, "Melbourne VIC 3000");
            TestCriteria(new MemberSearchCriteria { Location = location });

            location = _locationQuery.ResolveLocation(country, "VIC");
            TestCriteria(new MemberSearchCriteria { Location = location });

            location = _locationQuery.ResolveLocation(country, "Melbourne");
            TestCriteria(new MemberSearchCriteria { Location = location });
        }

        private void TestCriteria(MemberSearchCriteria criteria)
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch { Name = SearchName, Criteria = criteria };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch);

            // Get it.

            var gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
            Assert.IsNotNull(gotSearch);
            Assert.IsNotNull(gotSearch.Criteria);
            Assert.AreEqual(criteria, gotSearch.Criteria);

            var gotSearches = _memberSearchesQuery.GetMemberSearches(savedSearch.OwnerId);
            Assert.AreEqual(1, gotSearches.Count);
            Assert.IsNotNull(gotSearches[0].Criteria);
            Assert.AreEqual(criteria, gotSearches[0].Criteria);
        }
    }
}