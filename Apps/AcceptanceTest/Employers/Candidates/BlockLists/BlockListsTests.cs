using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.BlockLists
{
    [TestClass]
    public abstract class BlockListsTests
        : CandidateListsTests
    {
        protected readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
        }

        protected ReadOnlyUrl GetBlockListUrl(BlockListType blockListType, int? page, int? items)
        {
            if (page == null && items == null)
                return GetBlockListUrl(blockListType);
            return Get(GetBlockListUrl(blockListType), page, items);
        }

        protected CandidateBlockList GetTemporaryBlockList(IEmployer employer)
        {
            return _candidateBlockListsQuery.GetTemporaryBlockList(employer);
        }

        protected CandidateBlockList GetPermanentBlockList(IEmployer employer)
        {
            return _candidateBlockListsQuery.GetPermanentBlockList(employer);
        }

    }
}
