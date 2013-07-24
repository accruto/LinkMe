using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public interface IJobAdFoldersCommand
    {
        bool CanRenameFolder(IMember member, JobAdFolder folder);
        void RenameFolder(IMember member, JobAdFolder folder, string name);
    }
}