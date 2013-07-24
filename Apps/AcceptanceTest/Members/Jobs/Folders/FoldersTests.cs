using LinkMe.Domain.Users.Members.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Folders
{
    [TestClass]
    public abstract class FoldersTests
        : JobAdListTests
    {
        protected readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
    }
}
