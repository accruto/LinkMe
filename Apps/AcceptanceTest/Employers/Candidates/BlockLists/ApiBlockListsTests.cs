using System;
using System.Linq;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.BlockLists
{
    [TestClass]
    public abstract class ApiBlockListsTests
        : ApiTests
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        protected ReadOnlyUrl _baseBlockListsUrl;

        protected const string BlockListNameFormat = "My blockList{0}";

        [TestInitialize]
        public void ApiBlockListsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _baseBlockListsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/blockLists/api/");
        }

        protected CandidateBlockList GetTemporaryBlockList(IEmployer employer)
        {
            return _candidateBlockListsQuery.GetTemporaryBlockList(employer);
        }

        protected CandidateBlockList GetPermanentBlockList(IEmployer employer)
        {
            return _candidateBlockListsQuery.GetPermanentBlockList(employer);
        }

        protected void AssertModel(int expectedCount, JsonListCountModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedCount, model.Count);
        }

        protected static void AssertBlockList(IEmployer employer, string name, CandidateBlockList blockList)
        {
            Assert.AreEqual(name, blockList.Name);
            Assert.AreEqual(false, blockList.IsDeleted);
            Assert.AreEqual(employer.Id, blockList.RecruiterId);
        }

        protected void AssertCandidates(IEmployer employer, Guid blockListId, params Member[] expectedMembers)
        {
            var candidateIds = _candidateBlockListsQuery.GetBlockedCandidateIds(employer, blockListId);
            Assert.AreEqual(expectedMembers.Length, candidateIds.Count);
            for (var index = 0; index < expectedMembers.Length; ++index)
            {
                var expectedMemberId = expectedMembers[index].Id;
                Assert.AreEqual(true, (from e in candidateIds where e == expectedMemberId select e).Any());
            }
        }
    }
}
