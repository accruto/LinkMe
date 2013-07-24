using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
//using org.apache.lucene.search;
using org.apache.lucene.store;

namespace LinkMe.Query.Search.Engine.Test.Members
{
    [TestClass]
    public class SortTests
        : TestClass
    {
        private IndexWriter _indexWriter;
        private static Indexer _indexer;
        private static readonly ILocationQuery LocationQuery = Resolve<ILocationQuery>();
        private static MockCandidateFlagListsQuery _mockFoldersQuery;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _mockFoldersQuery = new MockCandidateFlagListsQuery();

            _indexer = new Indexer(
                new MemberSearchBooster(), 
                LocationQuery,
                Resolve<IIndustriesQuery>(),
                _mockFoldersQuery);
        }

        [TestInitialize]
        public void SortTestsInitialize()
        {
            _indexWriter = new IndexWriter(new RAMDirectory(), new SimpleAnalyzer(), IndexWriter.MaxFieldLength.UNLIMITED);
        }

        [TestCleanup]
        public void SortTestsCleanup()
        {
            _indexWriter.close();
        }

        [TestMethod]
        public void SortByDateTest()
        {
            var dt = new DateTime(2011, 1, 1);
            var index = 0;

            // Create content.

            var never = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = never }, Candidate = new Candidate() });

            // Created time.

            var created1 = Guid.NewGuid();
            var time = dt.AddDays(++index);
            IndexContent(new MemberContent { Member = new Member { Id = created1, CreatedTime = time, LastUpdatedTime = time }, Candidate = new Candidate() });

            var created2 = Guid.NewGuid();
            time = dt.AddDays(++index);
            IndexContent(new MemberContent { Member = new Member { Id = created2, CreatedTime = time, LastUpdatedTime = time }, Candidate = new Candidate() });

            // Last updated time.

            var lastUpdated1 = Guid.NewGuid();
            time = dt.AddDays(++index);
            IndexContent(new MemberContent { Member = new Member { Id = lastUpdated1, CreatedTime = time, LastUpdatedTime = dt.AddDays(++index) }, Candidate = new Candidate() });

            var lastUpdated2 = Guid.NewGuid();
            time = dt.AddDays(++index);
            IndexContent(new MemberContent { Member = new Member { Id = lastUpdated2, CreatedTime = time, LastUpdatedTime = dt.AddDays(++index) }, Candidate = new Candidate() });

            // Candidate last updated time.

            var candidate1 = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = candidate1 }, Candidate = new Candidate { LastUpdatedTime = dt.AddDays(++index) } });

            var candidate2 = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = candidate2 }, Candidate = new Candidate { LastUpdatedTime = dt.AddDays(++index) } });

            // Resume last updated time.

            var resume1 = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = resume1 }, Candidate = new Candidate(), Resume = new Resume { LastUpdatedTime = dt.AddDays(++index) } });

            var resume2 = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = resume2 }, Candidate = new Candidate(), Resume = new Resume { LastUpdatedTime = dt.AddDays(++index) } });

            // Resume last updated time.

            var all1 = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = all1, CreatedTime = dt.AddDays(++index) }, Candidate = new Candidate { LastUpdatedTime = dt.AddDays(++index) }, Resume = new Resume { LastUpdatedTime = dt.AddDays(++index) } });

            var all2 = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = all2, CreatedTime = dt.AddDays(++index) }, Candidate = new Candidate { LastUpdatedTime = dt.AddDays(++index) }, Resume = new Resume { LastUpdatedTime = dt.AddDays(++index) } });

            // Default order.

            var searchQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.DateUpdated };
            var searchResults = Search(searchQuery, 0, 100);
            Assert.IsTrue(new[] { all2, all1, resume2, resume1, candidate2, candidate1, lastUpdated2, lastUpdated1, created2, created1, never }.SequenceEqual(searchResults.MemberIds));

            // Reverse order.

            searchQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.DateUpdated, ReverseSortOrder = true };
            searchResults = Search(searchQuery, 0, 100);
            Assert.IsTrue(new[] { never, created1, created2, lastUpdated1, lastUpdated2, candidate1, candidate2, resume1, resume2, all1, all2 }.SequenceEqual(searchResults.MemberIds));
        }

        [TestMethod]
        public void SortBySalaryTest()
        {
            // Create content.

            var noSalary = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = noSalary }, Candidate = new Candidate() });

            var cheap = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = cheap }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 40000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year } } });

            var expensive = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = expensive }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 200000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year } } });

            // Default order.

            var searchQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.Salary };
            var searchResults = Search(searchQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { expensive, cheap, noSalary }, searchResults.MemberIds.ToArray());

            // Reverse order.

            searchQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.Salary, ReverseSortOrder = true };
            searchResults = Search(searchQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { cheap, expensive, noSalary }, searchResults.MemberIds.ToArray());
        }

        [TestMethod]
        public void SortByCandidateStatusTest()
        {
            // Create content.

            var activelyLooking = Guid.NewGuid();
            var content = new MemberContent { Member = new Member { Id = activelyLooking }, Candidate = new Candidate { Status = CandidateStatus.ActivelyLooking } };
            IndexContent(content);

            var availableNow = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = availableNow }, Candidate = new Candidate { Status = CandidateStatus.AvailableNow } };
            IndexContent(content);

            var unspecified = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = unspecified }, Candidate = new Candidate { Status = CandidateStatus.Unspecified } };
            IndexContent(content);

            // Default order.

            var memberQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.Availability };
            var searchResults = Search(memberQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { availableNow, activelyLooking, unspecified }, searchResults.MemberIds.ToArray());

            // Reverse order.

            memberQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.Availability, ReverseSortOrder = true };
            searchResults = Search(memberQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { unspecified, activelyLooking, availableNow }, searchResults.MemberIds.ToArray());
        }

        [TestMethod]
        public void SortByFlaggedStatusTest()
        {
            // Create content.

            var flagged = new[] { Guid.NewGuid(), Guid.NewGuid() };
            var notFlagged = new[] { Guid.NewGuid(), Guid.NewGuid() };
            _mockFoldersQuery.SetFlagged(flagged);

            IndexContent(new MemberContent { Member = new Member { Id = flagged[0] }, Candidate = new Candidate() });
            IndexContent(new MemberContent { Member = new Member { Id = notFlagged[0] }, Candidate = new Candidate() });
            IndexContent(new MemberContent { Member = new Member { Id = flagged[1] }, Candidate = new Candidate() });
            IndexContent(new MemberContent { Member = new Member { Id = notFlagged[1] }, Candidate = new Candidate() });

            // Default order.

            var memberQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.Flagged };
            var searchResults = Search(memberQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { flagged[0], flagged[1], notFlagged[0], notFlagged[1] }, searchResults.MemberIds.ToArray());

            // Reverse order.

            memberQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.Flagged, ReverseSortOrder = true };
            searchResults = Search(memberQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { notFlagged[0], notFlagged[1], flagged[0], flagged[1] }, searchResults.MemberIds.ToArray());
        }

        [TestMethod]
        public void SortByNameTest()
        {
            // Create content.

            //Visibilty needs to be name and resume to allow indexing
            const ProfessionalVisibility visibility = ProfessionalVisibility.Name | ProfessionalVisibility.Resume;

            var bx = Guid.NewGuid();
            var member = new Member { Id = bx, FirstName = "b", LastName = "x" };
            member.VisibilitySettings.Professional.EmploymentVisibility = visibility;
            IndexContent(new MemberContent { Member = member, Candidate = new Candidate() });

            var ax = Guid.NewGuid();
            member = new Member { Id = ax, FirstName = "A", LastName = "x" };
            member.VisibilitySettings.Professional.EmploymentVisibility = visibility;
            IndexContent(new MemberContent { Member = member, Candidate = new Candidate() });

            var ay = Guid.NewGuid();
            member = new Member { Id = ay, FirstName = "a", LastName = "Y" };
            member.VisibilitySettings.Professional.EmploymentVisibility = visibility;
            IndexContent(new MemberContent { Member = member, Candidate = new Candidate() });


            // Ascending order.

            var searchQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.FirstName, ReverseSortOrder = false };
            var searchResults = Search(searchQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { ax, ay, bx }, searchResults.MemberIds.ToArray());

            // Descending order.

            searchQuery = new MemberSearchQuery { SortOrder = MemberSortOrder.FirstName, ReverseSortOrder = true };
            searchResults = Search(searchQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { bx, ay, ax }, searchResults.MemberIds.ToArray());
        }

        [TestMethod]
        public void SortByDistanceTest()
        {
            // Create content.

            var australia = LocationQuery.GetCountry("Australia");
            var stKilda = LocationQuery.ResolveLocation(australia, "St Kilda VIC");
            var armadale = LocationQuery.ResolveLocation(australia, "Armadale VIC");
            var bentleigh = LocationQuery.ResolveLocation(australia, "Bentleigh VIC");

            var armadaleMember = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = armadaleMember, Address = new Address { Location = armadale } }, Candidate = new Candidate() });

            var stKildaMember = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = stKildaMember, Address = new Address { Location = stKilda } }, Candidate = new Candidate() });

            var bentleighMember = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = bentleighMember, Address = new Address { Location = bentleigh } }, Candidate = new Candidate() });

            // Ascending order.

            var searchQuery = new MemberSearchQuery
                                  {
                                      Location = stKilda, Distance = 50,
                                      SortOrder = MemberSortOrder.Distance,
                                      ReverseSortOrder = false
                                  };
            var searchResults = Search(searchQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { stKildaMember, armadaleMember, bentleighMember }, searchResults.MemberIds.ToArray());

            // Descending order.

            searchQuery = new MemberSearchQuery
                              {
                                  Location = stKilda, Distance = 50,
                                  SortOrder = MemberSortOrder.Distance,
                                  ReverseSortOrder = true
                              };
            searchResults = Search(searchQuery, 0, 10);
            CollectionAssert.AreEqual(new[] { bentleighMember, armadaleMember, stKildaMember }, searchResults.MemberIds.ToArray());
        }

        private void IndexContent(MemberContent content)
        {
            _indexer.IndexContent(_indexWriter, content, true);
        }

        private MemberSearchResults Search(MemberSearchQuery memberQuery, int skip, int take)
        {
            var searcher = new Searcher(_indexWriter.getReader());
            var query = _indexer.GetQuery(memberQuery);
            var sort = _indexer.GetSort(null, memberQuery);
            var searchResults = searcher.Search(query, null, null, sort.getSort(), skip, take, false);
            return searchResults;
        }

        private class MockCandidateFlagListsQuery
            : ICandidateFlagListsQuery
        {
            private IList<Guid> _flagged;

            public void SetFlagged(IList<Guid> flagged)
            {
                _flagged = flagged;
            }

            CandidateFlagList ICandidateFlagListsQuery.GetFlagList(IEmployer employer)
            {
                throw new NotImplementedException();
            }

            bool ICandidateFlagListsQuery.IsFlagged(IEmployer employer, Guid candidateId)
            {
                throw new NotImplementedException();
            }

            int ICandidateFlagListsQuery.GetFlaggedCount(IEmployer employer)
            {
                throw new NotImplementedException();
            }

            IList<Guid> ICandidateFlagListsQuery.GetFlaggedCandidateIds(IEmployer employer)
            {
                return _flagged.ToList();
            }

            IList<Guid> ICandidateFlagListsQuery.GetFlaggedCandidateIds(IEmployer employer, IEnumerable<Guid> candidateIds)
            {
                throw new NotImplementedException();
            }
        }
    }
}