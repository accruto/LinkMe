using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates.Commands
{
    public interface ICandidateFoldersCommand
    {
        void CreatePrivateFolder(IEmployer employer, CandidateFolder folder);
        void CreateSharedFolder(IEmployer employer, CandidateFolder folder);

        bool CanDeleteFolder(IEmployer employer, CandidateFolder folder);
        bool CanRenameFolder(IEmployer employer, CandidateFolder folder);

        void DeleteFolder(IEmployer employer, Guid folderId);
        void UndeleteFolder(IEmployer employer, CandidateFolder folder);
        void RenameFolder(IEmployer employer, CandidateFolder folder, string name);
    }
}