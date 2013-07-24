using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Data;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Query.Search.Test.Members.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MembersDataContext = LinkMe.Query.Search.Test.Members.Data.MembersDataContext;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class GetExistingMemberSearchTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();
        private const string Name = "My saved search";

        [TestMethod]
        public void TestGetAllSearches()
        {
            // Get all ids of all saved searches.

            var ids = GetAllMemberSearchIds(_connectionFactory);

            // Now read all of them looking for any errors.

            var memberSearchesQuery = CreateMemberSearchesQuery();
            foreach (var id in ids)
            {
                try
                {
                    Assert.IsNotNull(memberSearchesQuery.GetMemberSearch(id));
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Cannot read saved search with id: '" + id + "'.", ex);
                }
            }
        }

        [TestMethod]
        public void TestGetAllSearchExecutions()
        {
            // Get all ids of all saved searches.

            var ids = GetAllMemberSearchExecutionIds(_connectionFactory);

            // Now read all of them looking for any errors.

            var memberSearchesQuery = CreateMemberSearchesQuery();
            foreach (var id in ids)
            {
                try
                {
                    Assert.IsNotNull(memberSearchesQuery.GetMemberSearchExecution(id));
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Cannot read saved search with id: '" + id + "'.", ex);
                }
            }
        }

        [TestMethod]
        public void TestLegacyAdvancedName()
        {
            var id = CreateSavedResumeSearch("LinkMe.Common.Managers.Search.AdvancedResumeSearchCriteria", null);
            var memberSearchesQuery = CreateMemberSearchesQuery();
            memberSearchesQuery.GetMemberSearch(id);
        }

        [TestMethod]
        public void TestLegacySimpleName()
        {
            var id = CreateSavedResumeSearch("LinkMe.Common.Managers.Search.SimpleResumeSearchCriteria", null);
            var memberSearchesQuery = CreateMemberSearchesQuery();
            memberSearchesQuery.GetMemberSearch(id);
        }

        [TestMethod]
        public void TestSimpleJobsToSearch()
        {
            var id = CreateSavedResumeSearch(
                "LinkMe.Common.Managers.Search.SimpleResumeSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"JobsToSearch", "3"},
                    }
                );
            var memberSearchesQuery = CreateMemberSearchesQuery();
            memberSearchesQuery.GetMemberSearch(id);
        }

        [TestMethod]
        public void TestAdvancedIndustriesName()
        {
            var id = CreateSavedResumeSearch(
                "LinkMe.Common.Managers.Search.AdvancedResumeSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Industries", "administration"},
                    }
                );
            var memberSearchesQuery = CreateMemberSearchesQuery();
            var search = memberSearchesQuery.GetMemberSearch(id);
            Assert.AreEqual(1, search.Criteria.IndustryIds.Count);
            Assert.AreEqual(_industriesQuery.GetIndustry("Administration").Id, search.Criteria.IndustryIds[0]);
        }

        [TestMethod]
        public void TestNoCountryPostcode()
        {
            var id = CreateSavedResumeSearch(
                "LinkMe.Common.Managers.Search.SimpleResumeSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Location", "sydney"},
                        {"Postcode", "2000"},
                    }
                );
            var memberSearchesQuery = CreateMemberSearchesQuery();
            var search = memberSearchesQuery.GetMemberSearch(id);

            var sydney = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Sydney NSW 2000");
            Assert.AreEqual(sydney, search.Criteria.Location);
        }

        [TestMethod]
        public void TestDistance()
        {
            var id = CreateSavedResumeSearch(
                "LinkMe.Common.Managers.Search.AdvancedResumeSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Distance", "30"},
                        {"Location", "Sydney NSW 2000"},
                    }
                );
            var memberSearchesQuery = CreateMemberSearchesQuery();
            var search = memberSearchesQuery.GetMemberSearch(id);
            Assert.AreEqual(30, search.Criteria.EffectiveDistance);
            Assert.AreEqual(30, search.Criteria.Distance);
        }

        [TestMethod]
        public void TestExecutionLegacyAdvancedName()
        {
            var id = CreateResumeSearch(
                "LinkMe.Common.Managers.Search.AdvancedResumeSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Distance", "30"},
                        {"Location", "Sydney NSW 2000"},
                    }
                );
            var memberSearchesQuery = CreateMemberSearchesQuery();
            var execution = memberSearchesQuery.GetMemberSearchExecution(id);
            Assert.AreEqual(30, execution.Criteria.EffectiveDistance);
            Assert.AreEqual(30, execution.Criteria.Distance);
        }

        [TestMethod]
        public void TestExecutionLegacySimpleName()
        {
            var id = CreateResumeSearch("LinkMe.Common.Managers.Search.SimpleResumeSearchCriteria", null);
            var memberSearchesQuery = CreateMemberSearchesQuery();
            memberSearchesQuery.GetMemberSearchExecution(id);
        }

        [TestMethod]
        public void TestExecutionLegacyLikeName()
        {
            var id = CreateResumeSearch("LinkMe.Common.Managers.Search.LikeResumeSearchCriteria", null);
            var memberSearchesQuery = CreateMemberSearchesQuery();
            Assert.IsNull(memberSearchesQuery.GetMemberSearchExecution(id));
        }

        [TestMethod]
        public void TestIndustry()
        {
            var industry = _industriesQuery.GetIndustries()[0];
            var industryIds = new[] { industry.Id };
            var id = CreateResumeSearch("AdvancedMemberSearchCriteria", new Dictionary<string, string> { { "IndustryIds", string.Join(",", (from i in industryIds select i.ToString()).ToArray()) } });

            var memberSearchesQuery = CreateMemberSearchesQuery();
            Assert.IsTrue(industryIds.CollectionEqual(memberSearchesQuery.GetMemberSearchExecution(id).Criteria.IndustryIds));
        }

        [TestMethod]
        public void TestUnknownIndustry()
        {
            var industryIds = new[] { Guid.NewGuid() };
            var id = CreateResumeSearch("AdvancedMemberSearchCriteria", new Dictionary<string, string> { { "IndustryIds", string.Join(",", (from i in industryIds select i.ToString()).ToArray()) } });

            // Unknown industries should be filtered out.

            var memberSearchesQuery = CreateMemberSearchesQuery();
            Assert.IsNull(memberSearchesQuery.GetMemberSearchExecution(id).Criteria.IndustryIds);
        }

        [TestMethod]
        public void TestKnownAndUnknownIndustry()
        {
            var industry = _industriesQuery.GetIndustries()[0];
            var industryIds = new[] { industry.Id, Guid.NewGuid() };
            var id = CreateResumeSearch("AdvancedMemberSearchCriteria", new Dictionary<string, string> { { "IndustryIds", string.Join(",", (from i in industryIds select i.ToString()).ToArray()) } });

            var memberSearchesQuery = CreateMemberSearchesQuery();
            Assert.IsTrue(new[] { industry.Id }.CollectionEqual(memberSearchesQuery.GetMemberSearchExecution(id).Criteria.IndustryIds));
        }

        private Guid CreateSavedResumeSearch(string type, ICollection<KeyValuePair<string, string>> criteria)
        {
            var id = Guid.NewGuid();
            using (var dc = new MembersDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = new SavedResumeSearchEntity
                {
                    id = id,
                    createdTime = DateTime.Now,
                    name = Name,
                    ResumeSearchCriteriaSetEntity = new ResumeSearchCriteriaSetEntity
                    {
                        id = Guid.NewGuid(),
                        type = type,
                        ResumeSearchCriteriaEntities = CreateResumeSearchCriteria(criteria),
                    },
                };

                dc.SavedResumeSearchEntities.InsertOnSubmit(entity);
                dc.SubmitChanges();
            }

            return id;
        }

        private Guid CreateResumeSearch(string type, ICollection<KeyValuePair<string, string>> criteria)
        {
            var id = Guid.NewGuid();
            using (var dc = new MembersDataContext(_connectionFactory.CreateConnection()))
            {
                var entity = new ResumeSearchEntity
                {
                    id = id,
                    startTime = DateTime.Now,
                    ResumeSearchCriteriaSetEntity = new ResumeSearchCriteriaSetEntity
                    {
                        id = Guid.NewGuid(),
                        type = type,
                        ResumeSearchCriteriaEntities = CreateResumeSearchCriteria(criteria),
                    },
                    ResumeSearchResultSetEntity = new ResumeSearchResultSetEntity
                    {
                        id = Guid.NewGuid(),
                        count = 0
                    },
                };

                dc.ResumeSearchEntities.InsertOnSubmit(entity);
                dc.SubmitChanges();
            }

            return id;
        }

        private static EntitySet<ResumeSearchCriteriaEntity> CreateResumeSearchCriteria(ICollection<KeyValuePair<string, string>> criteria)
        {
            if (criteria == null || criteria.Count == 0)
                return null;
            var set = new EntitySet<ResumeSearchCriteriaEntity>();
            set.AddRange(from c in criteria
                         select new ResumeSearchCriteriaEntity {name = c.Key, value = c.Value});
            return set;
        }

        private static IEnumerable<Guid> GetAllMemberSearchIds(IDbConnectionFactory connectionFactory)
        {
            using (var dc = new MembersDataContext(connectionFactory.CreateConnection()))
            {
                var ids = (from s in dc.SavedResumeSearchEntities select s.id).ToArray();
                return ids.Except(new [] {new Guid("5248c710-bc14-4e66-b0e8-3fe55c34fce6")}).ToList();
            }
        }

        private static IEnumerable<Guid> GetAllMemberSearchExecutionIds(IDbConnectionFactory connectionFactory)
        {
            using (var dc = new MembersDataContext(connectionFactory.CreateConnection()))
            {
                return (from s in dc.ResumeSearchEntities
                        where s.ResumeSearchCriteriaSetEntity.type != "LinkMe.Common.Managers.Search.LikeResumeSearchCriteria"
                        select s.id).ToList();
            }
        }

        private IMemberSearchesQuery CreateMemberSearchesQuery()
        {
            var repository = new MembersRepository(Resolve<IDataContextFactory>(), new MemberSearchCriteriaPersister(_locationQuery, _industriesQuery));
            return new MemberSearchesQuery(repository);
        }
    }
}
