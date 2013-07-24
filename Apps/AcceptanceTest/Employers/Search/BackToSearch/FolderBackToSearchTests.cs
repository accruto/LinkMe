using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.BackToSearch
{
    [TestClass]
    public class FolderBackToSearchTests
        : BackToSearchTests
    {
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        private ReadOnlyUrl _baseFoldersUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _baseFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/");
        }

        protected override ReadOnlyUrl GetBackToSearchUrl(IEmployer employer)
        {
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            return new ReadOnlyApplicationUrl(_baseFoldersUrl, folder.Id.ToString());
        }
    }
}