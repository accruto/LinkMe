using LinkMe.Domain.Users.Members.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.BlockLists
{
    [TestClass]
    public abstract class BlockListsTests
        : JobAdListTests
    {
        protected readonly IJobAdBlockListsQuery _jobAdBlockListsQuery = Resolve<IJobAdBlockListsQuery>();
    }
}
