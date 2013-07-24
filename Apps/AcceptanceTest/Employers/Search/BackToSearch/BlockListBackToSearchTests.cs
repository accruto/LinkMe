using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.BackToSearch
{
    [TestClass]
    public class BlockListBackToSearchTests
        : BackToSearchTests
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();

        protected override ReadOnlyUrl GetBackToSearchUrl(IEmployer employer)
        {
            return GetBlockListUrl(_candidateBlockListsQuery.GetPermanentBlockList(employer).BlockListType);
        }
    }
}