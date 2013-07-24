using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Employers.Candidates.Commands
{
    public class CandidateFoldersCommand
        : ICandidateFoldersCommand
    {
        private readonly IContenderListsCommand _contenderListsCommand;
        private readonly IContenderListsQuery _contenderListsQuery;

        public CandidateFoldersCommand(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery)
        {
            _contenderListsCommand = contenderListsCommand;
            _contenderListsQuery = contenderListsQuery;
        }

        void ICandidateFoldersCommand.CreatePrivateFolder(IEmployer employer, CandidateFolder folder)
        {
            folder.RecruiterId = employer.Id;
            folder.OrganisationId = null;
            folder.FolderType = FolderType.Private;

            Validate(employer, folder, folder.Name);
            _contenderListsCommand.CreateList(folder);
        }

        void ICandidateFoldersCommand.CreateSharedFolder(IEmployer employer, CandidateFolder folder)
        {
            // Create a list for the folder.

            folder.RecruiterId = employer.Id;
            folder.OrganisationId = employer.Organisation.Id;
            folder.FolderType = FolderType.Shared;

            Validate(employer, folder, folder.Name);
            _contenderListsCommand.CreateList(folder);
        }

        bool ICandidateFoldersCommand.CanDeleteFolder(IEmployer employer, CandidateFolder folder)
        {
            return CanDeleteFolder(employer, folder);
        }

        bool ICandidateFoldersCommand.CanRenameFolder(IEmployer employer, CandidateFolder folder)
        {
            return CanRenameFolder(employer, folder);
        }

        void ICandidateFoldersCommand.DeleteFolder(IEmployer employer, Guid folderId)
        {
            var folder = _contenderListsQuery.GetList<CandidateFolder>(folderId);
            if (folder != null)
            {
                if (!CanDeleteFolder(employer, folder))
                    throw new CandidateFoldersPermissionsException(employer, folder.Id);
                _contenderListsCommand.DeleteList(folderId);
            }
        }

        void ICandidateFoldersCommand.UndeleteFolder(IEmployer employer, CandidateFolder folder)
        {
            if (!CanDeleteFolder(employer, folder))
                throw new CandidateFoldersPermissionsException(employer, folder.Id);

            folder.IsDeleted = false;
            _contenderListsCommand.UpdateList(folder);
        }

        void ICandidateFoldersCommand.RenameFolder(IEmployer employer, CandidateFolder folder, string name)
        {
            if (!CanRenameFolder(employer, folder))
                throw new CandidateFoldersPermissionsException(employer, folder.Id);

            // Validate.

            Validate(employer, folder, name);
            folder.Name = name;
            _contenderListsCommand.UpdateList(folder);
        }

        private static bool CanDeleteFolder(IEmployer employer, CandidateFolder folder)
        {
            // Must be owner.

            return CanAccessFolder(employer, folder, true)
                && CanBeDeleted(folder)
                && folder.RecruiterId == employer.Id;
        }

        private static bool CanBeDeleted(CandidateFolder folder)
        {
            return folder.FolderType == FolderType.Private || folder.FolderType == FolderType.Shared;
        }

        private static bool CanBeRenamed(CandidateFolder folder)
        {
            return folder.FolderType != FolderType.Mobile;
        }

        private static bool CanRenameFolder(IEmployer employer, CandidateFolder folder)
        {
            // Must be owner.

            return CanAccessFolder(employer, folder, false)
                && CanBeRenamed(folder)
                && folder.RecruiterId == employer.Id;
        }

        private void Validate(IEmployer employer, CandidateFolder folder, string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ValidationErrorsException(new RequiredValidationError("name"));

            // Don't need to check the name of a deleted folder.

            if (folder.IsDeleted)
                return;

            // Check no private folder with the same name and owner OR no shared folder with same name and organisation.

            if (folder.FolderType == FolderType.Shared)
            {
                if (_contenderListsQuery.GetSharedList<CandidateFolder>(employer.Organisation.Id, newName, new[]{(int)FolderType.Shared}) != null)
                    throw new ValidationErrorsException(new DuplicateValidationError("Name"));
            }
            else
            {
                if (_contenderListsQuery.GetList<CandidateFolder>(employer.Id, newName, new[]{(int)FolderType.Shortlist, (int)FolderType.Private}) != null)
                    throw new ValidationErrorsException(new DuplicateValidationError("Name"));
            }
        }

        private static bool CanAccessFolder(IEmployer employer, ContenderList folder, bool allowDeleted)
        {
            if (employer == null)
                return false;
            if (folder == null)
                return false;
            if (!allowDeleted && folder.IsDeleted)
                return false;

            return true;
        }
    }
}