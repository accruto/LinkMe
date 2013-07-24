using System.Linq;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.FlagLists
{
    [TestClass]
    public abstract class ApiFlagListsTests
        : ApiTests
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        protected ReadOnlyUrl _baseFlagListsUrl;

        protected const string FlagListNameFormat = "My flagList{0}";

        [TestInitialize]
        public void ApiFlagListsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _baseFlagListsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/flaglists/api/");
        }

        protected CandidateFlagList GetFlagList(IEmployer employer)
        {
            return _candidateFlagListsQuery.GetFlagList(employer);
        }

        protected void AssertModel(int expectedCount, JsonListCountModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedCount, model.Count);
        }

        protected static void AssertFlagList(IEmployer employer, string name, bool isShared, CandidateFlagList flagList)
        {
            Assert.AreEqual(name, flagList.Name);
            Assert.AreEqual(false, flagList.IsDeleted);
            Assert.AreEqual(employer.Id, flagList.RecruiterId);
        }

        protected void AssertCandidates(IEmployer employer, params Member[] expectedMembers)
        {
            var candidateIds = _candidateFlagListsQuery.GetFlaggedCandidateIds(employer);
            Assert.AreEqual(expectedMembers.Length, candidateIds.Count);
            for (var index = 0; index < expectedMembers.Length; ++index)
            {
                var expectedMemberId = expectedMembers[index].Id;
                Assert.AreEqual(true, (from i in candidateIds where i == expectedMemberId select i).Any());
            }
        }
    }
}
