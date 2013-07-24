using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.store;

namespace LinkMe.Query.Search.Engine.Test.Members
{
    [TestClass]
    public class LocationFilterTests
        : TestClass
    {
        private static IndexWriter _indexWriter;
        private static Indexer _indexer;
        private static IndexSearcher _indexSearcher;

        private static readonly ILocationQuery LocationQuery = Resolve<ILocationQuery>();
        private static Country _australia;
        private static LocationReference _victoria;
        private static LocationReference _tasmania;
        private static LocationReference _queensland;
        private static LocationReference _devonport;
        private static LocationReference _hobart;
        private static LocationReference _melbourne;
        private static LocationReference _goldCoast;
        private static LocationReference _nearGoldCoast;
        private static LocationReference _armadale;
        private static LocationReference _malvern;
        private static LocationReference _nearArmadale;

        private static Guid _zero;
        private static Guid _one;
        private static Guid _three;
        private static Guid _four;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _indexWriter = new IndexWriter(new RAMDirectory(), new SimpleAnalyzer(), IndexWriter.MaxFieldLength.UNLIMITED);

            _australia = LocationQuery.GetCountry("Australia");
            _victoria = new LocationReference(LocationQuery.GetCountrySubdivision(_australia, "VIC"));
            _tasmania = new LocationReference(LocationQuery.GetCountrySubdivision(_australia, "TAS"));
            _queensland = new LocationReference(LocationQuery.GetCountrySubdivision(_australia, "QLD"));
            _devonport = new LocationReference(LocationQuery.GetPostalCode(_australia, "7310").Locality);
            _hobart = new LocationReference(LocationQuery.GetRegion(_australia, "Hobart"));
            _melbourne = new LocationReference(LocationQuery.GetRegion(_australia, "Melbourne"));
            _goldCoast = new LocationReference(LocationQuery.GetRegion(_australia, "Gold Coast"));
            _nearGoldCoast = new LocationReference(LocationQuery.GetPostalCode(_australia, "4227").Locality);
            _armadale = new LocationReference(LocationQuery.GetPostalCode(_australia, "3143"));
            _malvern = new LocationReference(LocationQuery.GetPostalSuburb(LocationQuery.GetPostalCode(_australia, "3144"), "Malvern"));
            _nearArmadale = new LocationReference(LocationQuery.GetPostalCode(_australia, "3181").Locality);

            _indexer = new Indexer(new MemberSearchBooster(), LocationQuery, Resolve<IIndustriesQuery>(), null);

            // Zero - not willing to relocate.
            _zero = Guid.NewGuid();
            IndexContent(new MemberContent
            {
                Member = new Member { Id = _zero, Address = new Address { Location = ResolvePostalSuburb("Malvern VIC") } },
                Candidate = new Candidate(),
            });

            // One - in NSW, but willing to relocate to Armadale or Malvern or TAS
            _one = Guid.NewGuid();
            IndexContent(new MemberContent
            {
                Member = new Member { Id = _one, Address = new Address { Location = ResolvePostalSuburb("Woolloomooloo NSW"), } },
                Candidate = new Candidate { RelocationPreference = RelocationPreference.Yes, RelocationLocations = new[] { _armadale, _malvern, _tasmania } },
            });

            // Three - would consider relocating anywhere in Australia
            _three = Guid.NewGuid();
            IndexContent(new MemberContent
            {
                Member = new Member { Id = _three, Address = new Address { Location = ResolvePostalSuburb("Caulfield VIC"), } },
                Candidate = new Candidate { RelocationPreference = RelocationPreference.Yes, RelocationLocations = new[] { new LocationReference(LocationQuery.GetCountrySubdivision(_australia, null)) } },
            });

            // Four - "would consider" relocating to Gold Coast (region)
            _four = Guid.NewGuid();
            IndexContent(new MemberContent
            {
                Member = new Member { Id = _four, Address = new Address { Location = ResolvePostalSuburb("VIC"), } },
                Candidate = new Candidate { RelocationPreference = RelocationPreference.Yes, RelocationLocations = new[] { _goldCoast } },
            });

            _indexSearcher = new IndexSearcher(_indexWriter.getReader());
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _indexWriter.close();
        }

        [TestMethod]
        public void FilterByStateVictoria()
        {
            var memberQuery = new MemberSearchQuery { Location = _victoria };
            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _zero, _three, _four }, results);
        }

        [TestMethod]
        public void FilterByStateTasmania()
        {
            var memberQuery = new MemberSearchQuery { Location = _tasmania };
            var results = Search(memberQuery);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void FilterByStateQueensland()
        {
            var memberQuery = new MemberSearchQuery { Location = _queensland };
            var results = Search(memberQuery);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void FilterByRegionGoldCoast()
        {
            var memberQuery = new MemberSearchQuery { Location = _goldCoast };
            var results = Search(memberQuery);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void FilterByRegionMelbourne()
        {
            var memberQuery = new MemberSearchQuery { Location = _melbourne };
            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _zero, _three }, results);
        }

        [TestMethod]
        public void FilterByRegionHobart()
        {
            var memberQuery = new MemberSearchQuery { Location = _hobart };
            var results = Search(memberQuery);
            Assert.AreEqual(0, results.Length);
        }

        /// <summary>
        /// Match candidates willing to relocate to the Locality (or nearby). 
        /// Eg. willing to relocate to Armadale, searching within 5 km of Prahran. Malvern is also included and member 1
        /// matches on TWO relocation areas, but should be returned only once.
        /// </summary>
        [TestMethod]
        public void FilterByLocalityRelocateToLocality()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _nearArmadale,
                                      Distance = 5,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _zero, _one, _three }, results);
        }

        /// <summary>
        /// Match candidates willing to relocate to a Region that includes the searched Locality.
        /// Eg. willing to relocate to Gold Coast, searching for Reedy Creek 4227.
        /// </summary>
        [TestMethod]
        public void FilterByLocalityRelocateToRegion()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _nearGoldCoast,
                                      Distance = 5,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _three, _four }, results);
        }

        /// <summary>
        /// Match candidates willing to relocate to a CountrySubdivision that includes the searched Locality.
        /// Eg. willing to relocate to TAS, searching for Devonport 7310.
        /// </summary>
        [TestMethod]
        public void FilterByLocalityRelocateToCountrySubdivision()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _devonport,
                                      Distance = 5,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _one, _three }, results);
        }

        /// <summary>
        /// Match candidates willing to relocate to the searched CountrySubdivision.
        /// Eg. willing to relocate to TAS, searching for TAS.
        /// </summary>
        [TestMethod]
        public void FilterByStateRelocateToCountrySubdivision()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _tasmania,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _one, _three }, results);
        }

        /// <summary>
        /// Match candidates willing to relocate to a Locality within the searched CountrySubdivision.
        /// Eg. willing to relocate to Armadale, searching for VIC.
        /// </summary>
        [TestMethod]
        public void FilterByStateRelocateToLocality()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _victoria,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _zero, _one, _three, _four }, results);
        }

        /// <summary>
        /// Match candidates willing to relocate to a Region if that Region has any localities in the CountrySubdivision.
        /// Eg. willing to relocate to Gold Coast, searching for QLD.
        /// </summary>
        [TestMethod]
        public void FilterByStateRelocateToRegion()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _queensland,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _three, _four }, results);
        }

        /// <summary>
        /// Match candidates willing to relocate to the searched Region.
        /// Eg. willing to relocate to Gold Coast, searching for Gold Coast.
        /// </summary>
        [TestMethod]
        public void FilterByRegionRelocateToSearchedRegion()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _goldCoast,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _three, _four }, results);
        }

        /// <summary>
        /// Match a candidate willing to relocate to a Locality within the search Region.
        /// Eg. willing to relocate to Armadale, searching for Melbourne (the Region).
        /// </summary>
        [TestMethod]
        public void FilterByRegionRelocateToLocality()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _melbourne,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _zero, _one,     _three }, results);
        }

        /// <summary>
        /// Match candidate willing to relocate to a CountrySubdivison that has any localities in the searched Region.
        /// Eg. willing to relocate to TAS, searching for Hobart (the Region).
        /// </summary>
        [TestMethod]
        public void FilterByRegionRelocateToCountrySubdivison()
        {
            var memberQuery = new MemberSearchQuery
                                  {
                                      Location = _hobart,
                                      IncludeRelocating = true
                                  };

            var results = Search(memberQuery);
            CollectionAssert.AreEquivalent(new[] { _one, _three }, results);
        }

        #region Private Methods

        private static void IndexContent(MemberContent content)
        {
            _indexer.IndexContent(_indexWriter, content, true);
        }

        private static Guid[] Search(MemberSearchQuery memberQuery)
        {
            var query = _indexer.GetQuery(memberQuery);
            var filter = _indexer.GetFilter(memberQuery, null, null);

            var hits = _indexSearcher.search(query, filter, 10);

            return Array.ConvertAll(hits.scoreDocs, scoreDoc =>
            {
                var document = _indexSearcher.doc(scoreDoc.doc);
                return new Guid(document.get(SearchFieldName.Id));
            });
        }

        private static LocationReference ResolvePostalSuburb(string location)
        {
            return LocationQuery.ResolvePostalSuburb(_australia, location);
        }

        #endregion
    }
}