using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Query.Search.Test.JobAds.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobAdsRepository = LinkMe.Query.Search.JobAds.Data.JobAdsRepository;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    [TestClass]
    public class GetExistingJobAdSearchTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestGetAllSearches()
        {
            // Get all ids of all saved searches.

            var ids = GetAllJobAdSearchIds(CreateDbConnectionFactory(true));

            // Now read all of them looking for any errors.

            var jobAdSearchesQuery = CreateJobAdSearchesQuery(true);
            foreach (var id in ids)
            {
                try
                {
                    Assert.IsNotNull(jobAdSearchesQuery.GetJobAdSearch(id));
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

            var ids = GetAllJobAdSearchExecutionIds(CreateDbConnectionFactory(true));

            // Now read all of them looking for any errors.

            var jobAdSearchesQuery = CreateJobAdSearchesQuery(true);
            foreach (var id in ids)
            {
                try
                {
                    Assert.IsNotNull(jobAdSearchesQuery.GetJobAdSearchExecution(id));
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
            var id = CreateSavedJobAdSearch("LinkMe.Common.JobBoard.AdvancedJobSearchCriteria", null);
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            jobAdSearchesQuery.GetJobAdSearch(id);
        }

        [TestMethod]
        public void TestLegacySimpleName()
        {
            var id = CreateSavedJobAdSearch("LinkMe.Common.JobBoard.SimpleJobSearchCriteria", null);
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            jobAdSearchesQuery.GetJobAdSearch(id);
        }

        [TestMethod]
        public void TestAdvancedIndustries()
        {
            var id = CreateSavedJobAdSearch(
                "LinkMe.Common.JobBoard.AdvancedJobSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Industries", "administration"},
                    }
                );
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            var search = jobAdSearchesQuery.GetJobAdSearch(id);
            Assert.AreEqual(1, search.Criteria.IndustryIds.Count);
            Assert.AreEqual(_industriesQuery.GetIndustry("Administration").Id, search.Criteria.IndustryIds[0]);
        }

        [TestMethod]
        public void TestAdvancedIndustriesId()
        {
            var id = CreateSavedJobAdSearch(
                "LinkMe.Common.JobBoard.AdvancedJobSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Industries", "ef11ca42-33a5-4244-a8c8-09d6e2c2fa9b"},
                    }
                );
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            var search = jobAdSearchesQuery.GetJobAdSearch(id);
            Assert.AreEqual(1, search.Criteria.IndustryIds.Count);
            Assert.AreEqual(_industriesQuery.GetIndustry("Self-Employment").Id, search.Criteria.IndustryIds[0]);
        }

        [TestMethod]
        public void TestAdvancedIndustryId()
        {
            var id = CreateSavedJobAdSearch(
                "LinkMe.Common.JobBoard.AdvancedJobSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"IndustryId", "ef11ca42-33a5-4244-a8c8-09d6e2c2fa9b"},
                    }
                );
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            var search = jobAdSearchesQuery.GetJobAdSearch(id);
            Assert.AreEqual(1, search.Criteria.IndustryIds.Count);
            Assert.AreEqual(_industriesQuery.GetIndustry("Self-Employment").Id, search.Criteria.IndustryIds[0]);
        }

        [TestMethod]
        public void TestSimpleIndustries()
        {
            var id = CreateSavedJobAdSearch(
                "LinkMe.Common.JobBoard.SimpleJobSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Industries", "hospitality-tourism"},
                    }
                );
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            var search = jobAdSearchesQuery.GetJobAdSearch(id);
            Assert.AreEqual(_industriesQuery.GetIndustry("Hospitality & Tourism").Id, search.Criteria.IndustryIds[0]);
        }

        [TestMethod]
        public void TestNoCountryPostcode()
        {
            var id = CreateSavedJobAdSearch(
                "LinkMe.Common.JobBoard.SimpleJobSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Location", "sydney"},
                        {"Postcode", "2000"},
                    }
                );
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            var search = jobAdSearchesQuery.GetJobAdSearch(id);

            var sydney = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Sydney NSW 2000");
            Assert.AreEqual(sydney, search.Criteria.Location);
        }

        [TestMethod]
        public void TestDistance()
        {
            var id = CreateSavedJobAdSearch(
                "LinkMe.Common.JobBoard.AdvancedJobSearchCriteria",
                new Dictionary<string, string>
                    {
                        {"Distance", "30"},
                        {"Location", "Sydney NSW 2000"},
                    }
                );
            var jobAdSearchesQuery = CreateJobAdSearchesQuery(false);
            var search = jobAdSearchesQuery.GetJobAdSearch(id);
            Assert.AreEqual(30, search.Criteria.EffectiveDistance);
            Assert.AreEqual(30, search.Criteria.Distance);
        }

        private static Guid CreateSavedJobAdSearch(string type, ICollection<KeyValuePair<string, string>> criteria)
        {
            var id = Guid.NewGuid();
            using (var dc = new JobAdsDataContext(CreateDbConnectionFactory(false).CreateConnection()))
            {
                var entity = new SavedJobSearchEntity
                                 {
                                     id = id,
                                     displayText = string.Empty,
                                     JobSearchCriteriaSetEntity = new JobSearchCriteriaSetEntity
                                                                      {
                                                                          id = Guid.NewGuid(),
                                                                          type = type,
                                                                          JobSearchCriteriaEntities = CreateJobSearchCriteria(criteria),
                                                                      },
                                 };

                dc.SavedJobSearchEntities.InsertOnSubmit(entity);
                dc.SubmitChanges();
            }

            return id;
        }

        private static EntitySet<JobSearchCriteriaEntity> CreateJobSearchCriteria(ICollection<KeyValuePair<string, string>> criteria)
        {
            if (criteria == null || criteria.Count == 0)
                return null;
            var set = new EntitySet<JobSearchCriteriaEntity>();
            set.AddRange(from c in criteria
                         select new JobSearchCriteriaEntity { name = c.Key, value = c.Value });
            return set;
        }

        private static IEnumerable<Guid> GetAllJobAdSearchIds(IDbConnectionFactory connectionFactory)
        {
            using (var dc = new JobAdsDataContext(connectionFactory.CreateConnection()))
            {
                return (from s in dc.SavedJobSearchEntities
                        select s.id).ToList();
            }
        }

        private static IEnumerable<Guid> GetAllJobAdSearchExecutionIds(IDbConnectionFactory connectionFactory)
        {
            using (var dc = new JobAdsDataContext(connectionFactory.CreateConnection()))
            {
                return (from s in dc.JobSearchEntities
                        select s.id).ToList();
            }
        }

        private static IDbConnectionFactory CreateDbConnectionFactory(bool useExisting)
        {
//            return useExisting
            //              ? new SqlConnectionFactory("Initial Catalog=LinkMe;Data Source=db1.linkme.net.au;user id=sa;password=dev@LinkMe=410;")
            //: Resolve<IDbConnectionFactory>();

            return Resolve<IDbConnectionFactory>();
        }

        private IJobAdSearchesQuery CreateJobAdSearchesQuery(bool useExisting)
        {
            var repository = new JobAdsRepository(Resolve<IDataContextFactory>(), new JobAdSearchCriteriaPersister(_locationQuery , _industriesQuery));
            return new JobAdSearchesQuery(repository);
        }
    }
}