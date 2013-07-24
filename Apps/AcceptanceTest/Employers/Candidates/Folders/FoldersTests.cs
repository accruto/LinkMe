using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public abstract class FoldersTests
        : CandidateListsTests
    {
        protected readonly ICandidateFoldersCommand _candidateFoldersCommand = Resolve<ICandidateFoldersCommand>();
        protected readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();

        private ReadOnlyUrl _baseFoldersUrl;
        private ReadOnlyUrl _foldersUrl;

        [TestInitialize]
        public void FoldersTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _baseFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/");
            _foldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders");
        }

        protected ReadOnlyUrl GetFoldersUrl()
        {
            return _foldersUrl;
        }

        protected ReadOnlyUrl GetFolderUrl(Guid folderId)
        {
            return GetFolderUrl(folderId, null, null);
        }

        protected ReadOnlyUrl GetFolderUrl(Guid folderId, int? page, int? items)
        {
            if (page == null && items == null)
                return new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId.ToString());

            var queryString = new QueryString();
            if (page != null)
                queryString.Add("page", page.Value.ToString());
            if (items != null)
                queryString.Add("items", items.Value.ToString());
            return new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId.ToString(), queryString);
        }
        
        protected CandidateFolder GetMobileFolder(Employer employer)
        {
            var mobileFolder = _candidateFoldersQuery.GetMobileFolder(employer);
            mobileFolder.Name = "My mobile favourites";

            return mobileFolder;
        }

    }
}
