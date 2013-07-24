using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public class JobAdFoldersCommand
        : IJobAdFoldersCommand
    {
        private static readonly int[] ListTypes = new[] { (int)FolderType.Private };
        private readonly IJobAdListsCommand _jobAdListsCommand;
        private readonly IJobAdListsQuery _jobAdListsQuery;

        public JobAdFoldersCommand(IJobAdListsCommand jobAdListsCommand, IJobAdListsQuery jobAdListsQuery)
        {
            _jobAdListsCommand = jobAdListsCommand;
            _jobAdListsQuery = jobAdListsQuery;
        }

        bool IJobAdFoldersCommand.CanRenameFolder(IMember member, JobAdFolder folder)
        {
            return CanRenameFolder(member, folder);
        }

        void IJobAdFoldersCommand.RenameFolder(IMember member, JobAdFolder folder, string name)
        {
            if (!CanRenameFolder(member, folder))
                throw new JobAdFoldersPermissionsException(member, folder.Id);

            // Validate.

            Validate(member, folder, name);
            folder.Name = name;
            _jobAdListsCommand.UpdateList(folder);
        }

        private static bool CanBeRenamed(JobAdFolder folder)
        {
            return folder.FolderType != FolderType.Mobile;
        }

        private static bool CanRenameFolder(IMember member, JobAdFolder folder)
        {
            // Must be owner.

            return CanAccessFolder(member, folder)
                && CanBeRenamed(folder)
                && folder.MemberId == member.Id;
        }

        private void Validate(IHasId<Guid> member, JobAdList folder, string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ValidationErrorsException(new RequiredValidationError("name"));

            // Don't need to check the name of a deleted folder.

            if (folder.IsDeleted)
                return;

            // Check no folder with the same name exists.

            if (_jobAdListsQuery.GetList<JobAdFolder>(member.Id, newName, ListTypes) != null)
                throw new ValidationErrorsException(new DuplicateValidationError("Name"));
        }

        private static bool CanAccessFolder(IMember member, JobAdList folder)
        {
            if (member == null)
                return false;
            if (folder == null)
                return false;
            if (folder.IsDeleted)
                return false;
            return true;
        }
    }
}