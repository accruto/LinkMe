using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.solr.common;

namespace LinkMe.Query.Search.Engine.Test.Members
{
    [TestClass]
    public class SearchByNameTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private static IMemberSearchService _searchService;
        private const ProfessionalVisibility Visibility = ProfessionalVisibility.Name | ProfessionalVisibility.Resume;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _searchService = new MemberSearchService(
                new ResourceLoaderImpl(@"Apps\Config"),
                new MemberSearchBooster(), 
                Resolve<IMemberSearchEngineQuery>(),
                Resolve<IMembersQuery>(),
                Resolve<ICandidatesQuery>(),
                Resolve<IResumesQuery>(),
                new NullLocationQuery(),
                new NullIndustriesQuery(), 
                new NullMemberActivityFiltersQuery(), 
                null);
            _searchService.Clear();
        }

        [TestInitialize]
        public void SearchByNameTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestCleanup]
        public void SearchByNameTestsCleanup()
        {
            _searchService.Clear();
        }

        [TestMethod]
        public void ExactSearchTest()
        {
            var alex = Guid.NewGuid();
            var member = new Member { Id = alex, FirstName = "Alex", LastName = "Chiviliov", IsEnabled = true, EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "achiviliov@test.linkme.net.au", IsVerified = true } } };
            member.VisibilitySettings.Professional.EmploymentVisibility = Visibility;
            AddMember(new MemberContent { Member = member, Candidate = new Candidate() });

            var bob = Guid.NewGuid();
            member = new Member { Id = bob, FirstName = "Bob", LastName = "Your Uncle", IsEnabled = true, EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "bob@test.linkme.net.au", IsVerified = true } } };
            member.VisibilitySettings.Professional.EmploymentVisibility = Visibility;
            AddMember(new MemberContent { Member = member, Candidate = new Candidate() });

            // Search by first name.
            {
                var searchQuery = new MemberSearchQuery {Name = "Alex"};
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }

            // Search by last name.
            {
                var searchQuery = new MemberSearchQuery { Name = "Chiviliov" };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }

            // Search by partial last name.
            {
                var searchQuery = new MemberSearchQuery { Name = "Uncle" };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(bob, results.MemberIds[0]);
            }
        }

        [TestMethod]
        public void NicknameSearchTest()
        {
            var alex = Guid.NewGuid();
            var member = new Member { Id = alex, FirstName = "Alex", LastName = "Chiviliov", IsEnabled = true, EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "achiviliov@test.linkme.net.au", IsVerified = true } } };
            member.VisibilitySettings.Professional.EmploymentVisibility = Visibility;
            AddMember(new MemberContent { Member = member, Candidate = new Candidate() });

            var bob = Guid.NewGuid();
            member = new Member { Id = bob, FirstName = "Bob", LastName = "Your Uncle", IsEnabled = true, EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = "bob@test.linkme.net.au", IsVerified = true } } };
            member.VisibilitySettings.Professional.EmploymentVisibility = Visibility;
            AddMember(new MemberContent { Member = member, Candidate = new Candidate() });

            // Search by original nickname.
            {
                var searchQuery = new MemberSearchQuery { Name = "Alex", IncludeSimilarNames = true };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }

            // Search by full name.
            {
                var searchQuery = new MemberSearchQuery { Name = "Alexander", IncludeSimilarNames = true };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }

            // Search by another nickname.
            {
                var searchQuery = new MemberSearchQuery { Name = "Sasha", IncludeSimilarNames = true };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }

            // Search by ambiguous nickname (Sandy can be Alexander or Alexandra). 
            {
                var searchQuery = new MemberSearchQuery { Name = "Sandy", IncludeSimilarNames = true };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }

            // Phonetic search using first name.
            {
                var searchQuery = new MemberSearchQuery { Name = "Aleksandr", IncludeSimilarNames = true };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }

            // Phonetic search using first name.
            {
                var searchQuery = new MemberSearchQuery { Name = "Tchivilev", IncludeSimilarNames = true };
                var results = _searchService.Search(null, null, searchQuery);

                Assert.AreEqual(1, results.MemberIds.Count);
                Assert.AreEqual(alex, results.MemberIds[0]);
            }
        }

        private void AddMember(MemberContent content)
        {
            content.Member.Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), null) };
            _membersCommand.CreateMember(content.Member);
            content.Candidate.Id = content.Member.Id;
            _candidatesCommand.CreateCandidate(content.Candidate);
            _searchService.UpdateMember(content.Member.Id);
        }
    }
}