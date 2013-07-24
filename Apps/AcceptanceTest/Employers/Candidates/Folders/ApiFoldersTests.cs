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

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public abstract class ApiFoldersTests
        : ApiTests
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly ICandidateFoldersCommand _candidateFoldersCommand = Resolve<ICandidateFoldersCommand>();
        protected readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        protected readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        protected ReadOnlyUrl _baseFoldersUrl;

        protected const string FolderNameFormat = "My folder{0}";

        [TestInitialize]
        public void ApiFoldersTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _baseFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/api/");
        }

        protected CandidateFolder CreatePrivateFolder(IEmployer employer)
        {
            return CreatePrivateFolder(employer, 0);
        }

        protected CandidateFolder CreatePrivateFolder(IEmployer employer, int index)
        {
            var folder = new CandidateFolder { Name = string.Format(FolderNameFormat, index), RecruiterId = employer.Id };
            _candidateFoldersCommand.CreatePrivateFolder(employer, folder);
            return folder;
        }

        protected CandidateFolder CreateSharedFolder(IEmployer employer)
        {
            return CreateSharedFolder(employer, 0);
        }

        protected CandidateFolder CreateSharedFolder(IEmployer employer, int index)
        {
            var folder = new CandidateFolder { Name = string.Format(FolderNameFormat, index), RecruiterId = employer.Id };
            _candidateFoldersCommand.CreateSharedFolder(employer, folder);
            return folder;
        }

        protected CandidateFolder GetShortlistFolder(IEmployer employer)
        {
            return _candidateFoldersQuery.GetShortlistFolder(employer);
        }

        protected CandidateFolder GetMobileFolder(IEmployer employer)
        {
            return _candidateFoldersQuery.GetMobileFolder(employer);
        }

        protected void AssertModel(int expectedCount, JsonCountModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedCount, model.Count);
        }

        protected static void AssertFolder(IEmployer employer, string name, bool isShared, CandidateFolder folder)
        {
            Assert.AreEqual(name, folder.Name);
            Assert.AreEqual(false, folder.IsDeleted);
            if (isShared)
                Assert.AreEqual(true, folder.FolderType == FolderType.Shared);
            else
                Assert.AreEqual(true, folder.FolderType == FolderType.Private || folder.FolderType == FolderType.Shortlist || folder.FolderType == FolderType.Mobile);
            Assert.AreEqual(isShared ? employer.Organisation.Id : (Guid?)null, folder.OrganisationId);
            Assert.AreEqual(employer.Id, folder.RecruiterId);
        }

        protected void AssertCandidates(IEmployer employer, Guid folderId, params Member[] expectedMembers)
        {
            var candidateIds = _candidateFoldersQuery.GetInFolderCandidateIds(employer, folderId);
            Assert.AreEqual(expectedMembers.Length, candidateIds.Count);
            for (var index = 0; index < expectedMembers.Length; ++index)
            {
                var expectedMemberId = expectedMembers[index].Id;
                Assert.AreEqual(true, (from e in candidateIds where e == expectedMemberId select e).Any());
            }
        }
    }
}
